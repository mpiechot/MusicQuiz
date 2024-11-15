#nullable enable

using UnityEngine;

namespace Musicmania.Settings
{
    [CreateAssetMenu(fileName = "MusicmaniaSettings", menuName = "Musicmania/Settings")]
    public class MusicmaniaSettings : ScriptableObject
    {
        /// <summary>
        ///    The resource settings, containing information about where resources should be loaded/saved from/to.
        /// </summary>
        [field: SerializeField]
        public ResourceSettings ResourceSettings { get; private set; } = new();

        /// <summary>
        ///    The prefab settings, containing information about which prefabs should be used.
        /// </summary>
        [field: SerializeField]
        public PrefabSettings PrefabSettings { get; private set; } = new();
    }
}