#nullable enable

using Cysharp.Threading.Tasks;
using Musicmania.Exceptions;
using Musicmania.Settings;
using Musicmania.Utils;
using System;
using System.Globalization;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

namespace Musicmania.ResourceManagement.Providers
{
    /// <summary>
    ///     Resource provider for loading audio previews from Spotify.
    /// </summary>
    public sealed class SpotifyResourceProvider : IResourceProvider
    {
        public static string Prefix => "spotify";

        private const string TokenEndpoint = "https://accounts.spotify.com/api/token";
        private const string TrackEndpointFormat = "https://api.spotify.com/v1/tracks/{0}";
        private const int TokenRefreshBufferSeconds = 60;

        private readonly string clientId;
        private readonly string clientSecret;
        private readonly CancellableTaskCollection tokenTaskCollection = new();

        private SpotifyToken? currentToken;
        private UniTaskCompletionSource<string>? tokenExchangeCompletion;

        public SpotifyResourceProvider(ResourceSettings resourceSettings)
        {
            if (resourceSettings == null)
            {
                throw new ArgumentNullException(nameof(resourceSettings));
            }

            clientId = resourceSettings.SpotifyClientId;
            clientSecret = resourceSettings.SpotifyClientSecret;
        }

        /// <inheritdoc />
        public async UniTask<T> LoadAsync<T>(string key, CancellationToken cancellationToken) where T : Object
        {
            if (typeof(T) != typeof(AudioClip))
            {
                throw new InvalidOperationException($"{nameof(SpotifyResourceProvider)} only supports loading {nameof(AudioClip)} resources.");
            }

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                throw new InvalidOperationException("Spotify client credentials are missing. Configure them in the ResourceSettings asset.");
            }

            var trackId = ParseTrackId(key);
            var track = await GetTrackInfoWithRefreshAsync(trackId, cancellationToken);
            var clip = await DownloadAudioClipAsync(trackId, track, cancellationToken);

            return (T)(Object)clip;
        }

        /// <inheritdoc />
        public void Unload()
        {
            // Spotify previews are streamed and released by the garbage collector when no longer referenced.
        }

        /// <inheritdoc />
        public void Save()
        {
            throw new NotImplementedException();
        }

        private async UniTask<SpotifyTrackResponse> GetTrackInfoWithRefreshAsync(string trackId, CancellationToken cancellationToken)
        {
            var accessToken = await EnsureAccessTokenAsync(cancellationToken);
            try
            {
                return await GetTrackInfoAsync(trackId, accessToken, cancellationToken);
            }
            catch (SpotifyAuthorizationException)
            {
                currentToken = null;
                accessToken = await EnsureAccessTokenAsync(cancellationToken);
                return await GetTrackInfoAsync(trackId, accessToken, cancellationToken);
            }
        }

        private async UniTask<string> EnsureAccessTokenAsync(CancellationToken cancellationToken)
        {
            if (currentToken != null && !currentToken.IsExpired)
            {
                return currentToken.AccessToken;
            }

            var existingExchange = tokenExchangeCompletion;
            if (existingExchange != null)
            {
                return await existingExchange.Task.AttachExternalCancellation(cancellationToken);
            }

            var completionSource = new UniTaskCompletionSource<string>();
            tokenExchangeCompletion = completionSource;

            tokenTaskCollection.StartExecution(async internalCancellationToken =>
            {
                try
                {
                    currentToken = await RequestAccessTokenAsync(internalCancellationToken);
                    completionSource.TrySetResult(currentToken.AccessToken);
                }
                catch (Exception exception)
                {
                    completionSource.TrySetException(exception);
                }
                finally
                {
                    tokenExchangeCompletion = null;
                }
            });

            return await completionSource.Task.AttachExternalCancellation(cancellationToken);
        }

        private async UniTask<SpotifyToken> RequestAccessTokenAsync(CancellationToken cancellationToken)
        {
            var form = new WWWForm();
            form.AddField("grant_type", "client_credentials");

            using var request = UnityWebRequest.Post(TokenEndpoint, form);
            var credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            request.SetRequestHeader("Authorization", $"Basic {credentials}");

            await request.SendWebRequest().WithCancellation(cancellationToken);

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new InvalidOperationException($"Failed to exchange Spotify token: {request.error}");
            }

            var responseJson = request.downloadHandler.text;
            var response = JsonUtility.FromJson<SpotifyTokenResponse>(responseJson);
            if (response == null || string.IsNullOrEmpty(response.access_token))
            {
                throw new InvalidOperationException("Spotify token response did not contain a valid access token.");
            }

            var expiresInSeconds = Math.Max(response.expires_in - TokenRefreshBufferSeconds, 1);
            var expiresAt = DateTime.UtcNow.AddSeconds(expiresInSeconds);

            return new SpotifyToken(response.access_token, expiresAt);
        }

        private async UniTask<SpotifyTrackResponse> GetTrackInfoAsync(string trackId, string accessToken, CancellationToken cancellationToken)
        {
            var url = string.Format(CultureInfo.InvariantCulture, TrackEndpointFormat, trackId);
            using var request = UnityWebRequest.Get(url);
            request.SetRequestHeader("Authorization", $"Bearer {accessToken}");

            await request.SendWebRequest().WithCancellation(cancellationToken);

            if (request.result != UnityWebRequest.Result.Success)
            {
                if (request.responseCode == 401)
                {
                    throw new SpotifyAuthorizationException("Spotify access token expired or is invalid.");
                }

                throw new InvalidOperationException($"Failed to retrieve Spotify track '{trackId}': {request.error}");
            }

            var trackJson = request.downloadHandler.text;
            var track = JsonUtility.FromJson<SpotifyTrackResponse>(trackJson);
            if (track == null || string.IsNullOrEmpty(track.preview_url))
            {
                throw new InvalidOperationException($"Spotify track '{trackId}' does not provide a preview url.");
            }

            if (string.IsNullOrEmpty(track.name))
            {
                track.name = trackId;
            }

            return track;
        }

        private static async UniTask<AudioClip> DownloadAudioClipAsync(string trackId, SpotifyTrackResponse track, CancellationToken cancellationToken)
        {
            using var request = UnityWebRequestMultimedia.GetAudioClip(track.preview_url, AudioType.MPEG);

            await request.SendWebRequest().WithCancellation(cancellationToken);

            if (request.result != UnityWebRequest.Result.Success)
            {
                throw new InvalidOperationException($"Failed to download Spotify preview for track '{trackId}': {request.error}");
            }

            var clip = DownloadHandlerAudioClip.GetContent(request);
            clip.name = string.IsNullOrEmpty(track.name) ? trackId : track.name;
            return clip;
        }

        /// <summary>
        ///     Extracts the Spotify track identifier from a resource key that may contain prefixes or query parameters.
        /// </summary>
        /// <example>
        ///     <para>Example keys: <c>spotify:track:3n3Ppam7vgaVa1iaRUc9Lp</c> or <c>https://open.spotify.com/track/3n3Ppam7vgaVa1iaRUc9Lp?si=1234567890abcdef</c>.</para>
        /// </example>
        private static string ParseTrackId(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Spotify resource key cannot be null or empty.", nameof(key));
            }

            var trimmed = key.Trim();

            const string openSpotifySegment = "open.spotify.com/track/";
            var openSpotifyIndex = trimmed.IndexOf(openSpotifySegment, StringComparison.OrdinalIgnoreCase);
            if (openSpotifyIndex >= 0)
            {
                // Remove the share URL portion (e.g. https://open.spotify.com/track/...).
                trimmed = trimmed[(openSpotifyIndex + openSpotifySegment.Length)..];
            }

            const string trackPrefix = "track/";
            if (trimmed.StartsWith(trackPrefix, StringComparison.OrdinalIgnoreCase))
            {
                // Handle keys that still include the track segment after trimming the domain.
                trimmed = trimmed[trackPrefix.Length..];
            }

            const string spotifyTrackPrefix = "spotify:track:";
            if (trimmed.StartsWith(spotifyTrackPrefix, StringComparison.OrdinalIgnoreCase))
            {
                // Support Spotify URI format (spotify:track:{id}).
                trimmed = trimmed[spotifyTrackPrefix.Length..];
            }

            var questionMarkIndex = trimmed.IndexOf('?');
            if (questionMarkIndex >= 0)
            {
                // Drop any query parameters that follow the identifier.
                trimmed = trimmed[..questionMarkIndex];
            }

            var ampersandIndex = trimmed.IndexOf('&');
            if (ampersandIndex >= 0)
            {
                // Guard against additional query parameters appended without a question mark.
                trimmed = trimmed[..ampersandIndex];
            }

            if (trimmed.Contains('/'))
            {
                // Fallback for unexpected path segments by taking the last token.
                trimmed = trimmed[(trimmed.LastIndexOf('/') + 1)..];
            }

            if (string.IsNullOrEmpty(trimmed))
            {
                throw new InvalidOperationException("Spotify resource key did not contain a valid track identifier.");
            }

            return trimmed;
        }

        [Serializable]
        private sealed class SpotifyTokenResponse
        {
            public string access_token = string.Empty;
            public int expires_in;
        }

        [Serializable]
        private sealed class SpotifyTrackResponse
        {
            public string id = string.Empty;
            public string name = string.Empty;
            public string preview_url = string.Empty;
        }

        private sealed class SpotifyToken
        {
            public SpotifyToken(string accessToken, DateTime expiresAt)
            {
                AccessToken = accessToken;
                ExpiresAt = expiresAt;
            }

            public string AccessToken { get; }

            public DateTime ExpiresAt { get; }

            public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        }

    }
}
