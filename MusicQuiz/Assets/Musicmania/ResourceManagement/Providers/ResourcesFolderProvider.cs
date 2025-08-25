#nullable enable

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Musicmania.ResourceManagement.Providers
{
    /// <summary>
    /// Provider for loading resources from the Unity Resources folder.
    /// </summary>
    public class ResourcesFolderProvider : IResourceProvider
    {
        public static string Prefix => "resources";

        private Object? loadedResource;

        /// <inheritdoc />
        public async UniTask<T> LoadAsync<T>(string key, CancellationToken cancellationToken) where T : Object
        {
            loadedResource = await Resources.LoadAsync<T>(key);

            if (loadedResource is T result)
            {
                return result;
            }

            throw new System.Exception($"Failed to load resource at '{key}' of type {typeof(T).Name}");
        }

        /// <inheritdoc />
        public void Unload()
        {
            if (loadedResource != null)
            {
                Resources.UnloadAsset(loadedResource);
                loadedResource = null;
            }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}