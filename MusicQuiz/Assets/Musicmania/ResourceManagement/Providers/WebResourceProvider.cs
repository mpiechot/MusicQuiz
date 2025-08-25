#nullable enable

using Cysharp.Threading.Tasks;
using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Musicmania.ResourceManagement.Providers
{
    public class WebResourceProvider : IResourceProvider
    {
        public static string Prefix => "http";

        public async UniTask<T> LoadAsync<T>(string key, CancellationToken cancellationToken) where T : UnityEngine.Object
        {
            if (typeof(T) != typeof(AudioClip))
            {
                throw new InvalidOperationException($"WebAudioProvider only supports AudioClip, not {typeof(T).Name}");
            }

            var localPath = GetCachePathForUrl(key);
            AudioClip? clip = null;

            if (File.Exists(localPath))
            {
                using var request = UnityWebRequestMultimedia.GetAudioClip($"file://{localPath}", AudioType.UNKNOWN);

                await request.SendWebRequest().WithCancellation(cancellationToken);

                if (request.result != UnityWebRequest.Result.Success)
                {
                    throw new Exception($"Failed to load local audio file: {request.error}");
                }

                clip = DownloadHandlerAudioClip.GetContent(request);
            }
            else
            {
                using var request = UnityWebRequestMultimedia.GetAudioClip(key, AudioType.UNKNOWN);

                await request.SendWebRequest().WithCancellation(cancellationToken);

                if (request.result != UnityWebRequest.Result.Success)
                {
                    throw new Exception($"Failed to download audio: {request.error}");
                }

                clip = DownloadHandlerAudioClip.GetContent(request);

                // Speichern
                var data = request.downloadHandler.data;
                File.WriteAllBytes(localPath, data);
            }

            clip.name = Path.GetFileNameWithoutExtension(localPath);
            return (T)(UnityEngine.Object)clip;
        }

        public void Unload()
        {
            // Kein globales Unload notwendig
        }

        private static string GetCachePathForUrl(string url)
        {
            using var sha = SHA256.Create();
            var hash = sha.ComputeHash(Encoding.UTF8.GetBytes(url));
            var fileName = BitConverter.ToString(hash).Replace("-", "") + ".audio";

            return Path.Combine(Application.temporaryCachePath, fileName);
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}