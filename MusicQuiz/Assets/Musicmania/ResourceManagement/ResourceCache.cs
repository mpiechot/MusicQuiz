#nullable enable

using Musicmania.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Musicmania.ResourceManagement
{
    /// <summary>
    /// Represents a cache for resource handles, allowing efficient retrieval and storage of resources.
    /// </summary>
    public class ResourceCache
    {
        private readonly Dictionary<string, IResourceHandle> cache = new();

        private readonly ResourceHandleFactory handleFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResourceCache"/> class.
        /// </summary>
        /// <param name="resourceSettings">The settings that determine how resource providers are configured.</param>
        public ResourceCache(ResourceSettings resourceSettings)
        {
            if (resourceSettings == null)
            {
                throw new ArgumentNullException(nameof(resourceSettings));
            }

            handleFactory = new ResourceHandleFactory(resourceSettings);
        }

        /// <summary>
        /// Gets a resource handle for the specified prefix and path.
        /// </summary>
        /// <typeparam name="T">The type of the resource to load.</typeparam>
        /// <param name="prefix">The prefix used to identify the resource provider.</param>
        /// <param name="path">The path to the resource.</param>
        /// <returns>The resource handle for the specified prefix and path.</returns>
        public ResourceHandle<T> GetResourceHandle<T>(string prefix, string path) where T : UnityEngine.Object
        {
            var cacheKey = $"{prefix}:{path}";

            if (cache.TryGetValue(cacheKey, out var existing))
            {
                return (ResourceHandle<T>)existing;
            }

            var provider = handleFactory.GetProvider(prefix);
            var handler = new ResourceHandle<T>(path, provider);
            cache[cacheKey] = handler;

            return handler;
        }
    }
}
