#nullable enable

using UnityEngine;

namespace Musicmania.Extensions
{
    /// <summary>
    /// Provides extension methods for Transform type.
    /// </summary>
    public static class TransformExtensions
    {
        /// <summary>
        /// Destroys all child GameObjects of the Transform's GameObject.
        /// </summary>
        /// <param name="transform">The Transform instance whose children will be destroyed.</param>
        public static void DestroyAllChildren(this Transform transform)
        {
            foreach (Transform child in transform.transform)
            {
                if (child != null)
                {
                    GameObject.Destroy(child.gameObject);
                }
            }
        }
    }
}