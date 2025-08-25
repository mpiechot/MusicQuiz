#nullable enable

using Cysharp.Threading.Tasks;
namespace Musicmania.ResourceManagement
{
    /// <summary>
    /// Interface for a resource provider that handles loading and unloading of resources.
    /// </summary>
    public interface IResourceHandle
    {
        /// <summary>
        /// Gets the count of references to the resource.
        /// </summary>
        int RefCount { get; }

        /// <summary>
        /// Gets a value indicating whether the resource is already loaded or not.
        /// </summary>
        bool IsLoaded { get; }
    }

}
