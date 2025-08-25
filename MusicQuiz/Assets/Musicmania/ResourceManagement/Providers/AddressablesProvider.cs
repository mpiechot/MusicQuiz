#nullable enable

using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Musicmania.ResourceManagement.Providers
{
    /// <summary>
    /// Provider for loading resources using Unity's Addressables system.
    /// </summary>
    public class AddressablesProvider : IResourceProvider
    {
        public static string Prefix { get; } = "addressables";

        private AsyncOperationHandle? handle;

        /// <inheritdoc />
        public async UniTask<T> LoadAsync<T>(string key, CancellationToken cancellationToken) where T : Object
        {
            var operation = Addressables.LoadAssetAsync<T>(key);
            handle = operation;

            await operation.WithCancellation(cancellationToken);

            if (operation.Status != AsyncOperationStatus.Succeeded || operation.Result == null)
                throw new System.Exception($"Failed to load addressable asset with key '{key}'.");

            return operation.Result;
        }

        /// <inheritdoc />
        public void Unload()
        {
            if (handle.HasValue)
            {
                Addressables.Release(handle.Value);
                handle = null;
            }
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }
    }
}
