#nullable enable

using Cysharp.Threading.Tasks;
using System.IO;
using System.Threading;
using UnityEngine;

namespace Musicmania.ResourceManagement.Providers
{
    /// <summary>
    /// Provider for loading resources from JSON files.
    /// </summary>
    public class JsonResourceProvider : IResourceProvider
    {
        public static string Prefix => "json";

        private Object? loadedResource;

        private string jsonPath = string.Empty;

        /// <inheritdoc />
        public async UniTask<T> LoadAsync<T>(string key, CancellationToken cancellationToken) where T : Object
        {
            jsonPath = key;
            if (!File.Exists(jsonPath))
            {
                throw new System.Exception($"JSON file not found at '{jsonPath}'");
            }

            var json = await File.ReadAllTextAsync(jsonPath, cancellationToken);
            loadedResource = JsonUtility.FromJson<T>(json);

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
            if (loadedResource != null)
            {
                var json = JsonUtility.ToJson(loadedResource, true);
                File.WriteAllText(Path.Combine($"{Prefix}://", jsonPath), json);
            }
        }
    }
}