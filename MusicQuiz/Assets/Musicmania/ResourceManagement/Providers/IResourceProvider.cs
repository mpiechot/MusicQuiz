#nullable enable

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Musicmania.ResourceManagement.Providers
{
    /// <summary>
    /// Interface for a resource provider that handles loading and unloading of resources.
    /// </summary>
    public interface IResourceProvider
    {
        /// <summary>
        /// Gets the prefix used to identify the resource provider.
        /// </summary>
        static string Prefix { get; } = string.Empty;

        /// <summary>
        /// Loads the resource asynchronously on the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the resource to load.</typeparam>
        /// <param name="key">The key to identify the resource.</param>
        /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
        /// <returns>The loaded resource.</returns>
        UniTask<T> LoadAsync<T>(string key, CancellationToken cancellationToken) where T : Object;

        /// <summary>
        /// Unloads the resource asynchronously.
        /// </summary>
        void Unload();

        /// <summary>
        /// Saves the current state of the resource, if applicable.
        /// </summary>
        void Save();
    }
}
