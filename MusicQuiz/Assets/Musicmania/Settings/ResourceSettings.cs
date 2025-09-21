#nullable enable

using UnityEngine;

namespace Musicmania.Settings
{
    /// <summary>
    ///     Provides configuration options for resource storage paths and external providers.
    /// </summary>
    [CreateAssetMenu(fileName = "ResourceSettings", menuName = "Musicmania/Resource Settings")]
    public class ResourceSettings : ScriptableObject
    {
        /// <summary>
        ///     Gets or sets the location on disk where save data is stored.
        /// </summary>
        [field: SerializeField]
        public string SaveDataLocation { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the folder used to persist downloaded files.
        /// </summary>
        [field: SerializeField]
        public string DownloadedFilesLocation { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the path to the categories configuration file.
        /// </summary>
        [field: SerializeField]
        public string CategoriesFileLocation { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Spotify client identifier used when requesting preview data.
        /// </summary>
        [field: SerializeField]
        public string SpotifyClientId { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the Spotify client secret used when requesting preview data.
        /// </summary>
        [field: SerializeField]
        public string SpotifyClientSecret { get; set; } = string.Empty;
    }
}
