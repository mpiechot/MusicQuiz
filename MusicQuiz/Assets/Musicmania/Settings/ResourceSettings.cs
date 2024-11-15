#nullable enable

using UnityEngine;

namespace Musicmania.Settings
{
    [CreateAssetMenu(fileName = "ResourceSettings", menuName = "Musicmania/Resource Settings")]
    public class ResourceSettings : ScriptableObject
    {
        [field: SerializeField]
        public string SaveDataLocation { get; set; } = string.Empty;
    }
}