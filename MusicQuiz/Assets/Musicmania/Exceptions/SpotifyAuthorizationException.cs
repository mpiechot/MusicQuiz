#nullable enable

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace Musicmania.Exceptions
{
    /// <summary>
    ///     Represents errors that occur when the Spotify Web API rejects an authenticated request.
    ///     This exception is thrown when Spotify responds with an authorization failure, indicating
    ///     that the configured credentials are invalid or have expired.
    /// </summary>
    [Serializable]
    public class SpotifyAuthorizationException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SpotifyAuthorizationException"/> class.
        /// </summary>
        /// <param name="filePath">The source file that attempted the Spotify request.</param>
        /// <param name="callerMemberName">The member that triggered the authorization failure.</param>
        public SpotifyAuthorizationException([CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "")
            : base($"Spotify denied authorization for '{callerMemberName}' in '{Path.GetFileName(filePath)}'.")
        {
        }
    }
}
