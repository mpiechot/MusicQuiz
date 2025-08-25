#nullable enable

using Musicmania.ResourceManagement.Providers;
using Musicmania.Settings;
using System;
using System.Collections.Generic;

namespace Musicmania.ResourceManagement
{
    /// <summary>
    /// Manages resources by providing access to resources via <see cref="IResourceHandle"/>.
    /// </summary>
    public class ResourceManager : IDisposable
    {
        private readonly ResourceCache cache = new();
        private ResourceSettings resourceSettings;

        public ResourceManager(ResourceSettings resourceSettings)
        {
            this.resourceSettings = resourceSettings;
        }

        /// <summary>
        ///  Gets a resource handle for the specified key.
        /// </summary>
        /// <typeparam name="T">The type of the resource to load.</typeparam>
        /// <param name="key">The key to identify the resource, in the format 'prefix://path'.</param>
        /// <returns>The resource handle for the specified key.</returns>
        /// <exception cref="InvalidOperationException">Thrown when no provider is registered for the specified prefix.</exception>
        public ResourceHandle<T> GetResource<T>(string key) where T : UnityEngine.Object
        {
            var (prefix, path) = ParseKey(key);

            return cache.GetResourceHandle<T>(prefix, path);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        private static (string prefix, string path) ParseKey(string key)
        {
            var parts = key.Split(new[] { "://" }, 2, StringSplitOptions.None);
            if (parts.Length != 2)
                throw new ArgumentException("Key must be in format 'prefix://path'");

            return (parts[0], parts[1]);
        }
    }
}