#nullable enable

using Cysharp.Threading.Tasks;
using Musicmania.ResourceManagement.Providers;
using Musicmania.Utils;
using System.Threading;


namespace Musicmania.ResourceManagement
{
    /// <summary>
    /// Represents a handle to a resource that can be loaded and unloaded.
    /// </summary>
    /// <typeparam name="T">The type of the resource to handle.</typeparam>
    public class ResourceHandle<T> : IResourceHandle where T : UnityEngine.Object
    {
        private readonly string key;
        private readonly IResourceProvider provider;
        private T? resource;

        /// <summary>
        /// Initializes a new instance of a <see cref="ResourceHandle{T}"/>.
        /// </summary>
        /// <param name="key">The key to identify the resource.</param>
        /// <param name="provider">The provider to use for loading and unloading the resource.</param>
        public ResourceHandle(string key, IResourceProvider provider)
        {
            this.key = key;
            this.provider = provider;
        }

        /// <inheritdoc />
        public int RefCount { get; private set; }

        /// <inheritdoc />
        [MemberNotNullWhen(true, nameof(resource))]
        public bool IsLoaded { get; private set; }

        /// <inheritdoc />
        public async UniTask<T> LoadAsync(CancellationToken cancellationToken)
        {
            if (IsLoaded && resource != null)
            {
                RefCount++;
                return resource;
            }

            resource = await provider.LoadAsync<T>(key, cancellationToken);
            IsLoaded = true;
            RefCount = 1;
            return resource;
        }

        /// <inheritdoc />
        public void Unload()
        {
            if (!IsLoaded || --RefCount > 0)
            {
                return;
            }

            provider.Unload();
            resource = default;
            IsLoaded = false;
        }
    }
}
