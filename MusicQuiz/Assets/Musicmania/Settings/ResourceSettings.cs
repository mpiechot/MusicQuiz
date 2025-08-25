#nullable enable

using UnityEngine;

namespace Musicmania.Settings
{
    [CreateAssetMenu(fileName = "ResourceSettings", menuName = "Musicmania/Resource Settings")]
    public class ResourceSettings : ScriptableObject
    {
        [field: SerializeField]
        public string SaveDataLocation { get; set; } = string.Empty;

        [field: SerializeField]
        public string DownloadedFilesLocation { get; set; } = string.Empty;

        [field: SerializeField]
        public string CategoriesFileLocation { get; set; } = string.Empty;
    }
}