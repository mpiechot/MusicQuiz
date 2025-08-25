#nullable enable

using Musicmania.Settings.Ui;
using Musicmania.Utils;
using UnityEngine;

namespace Musicmania.Settings
{
    [CreateAssetMenu(fileName = "MusicmaniaSettings", menuName = "Musicmania/Settings")]
    public class MusicmaniaSettings : ScriptableObject
    {
        /// <summary>
        ///    Gets the resource settings, containing information about where resources should be loaded/saved from/to.
        /// </summary>
        [field: SerializeField]
        public ResourceSettings ResourceSettings { get; private set; } = null!;

        /// <summary>
        ///   Gets the screen prefab provider, containing references to the prefabs used for different screens in the app.
        /// </summary>
        [field: SerializeField]
        public ScreenPrefabProvider ScreenPrefabProvider { get; private set; } = null!;

        /// <summary>
        ///   Gets the data prefab provider, containing references to the prefabs used for different data from <see cref="Data"/>.
        /// </summary>
        [field: SerializeField]
        public DataPrefabProvider DataPrefabProvider { get; private set; } = null!;

        /// <summary>
        ///   Gets the color profile, containing information about the colors used in the app.
        /// </summary>
        [field: SerializeField]
        public ColorProfile ColorProfile { get; private set; } = null!;

        /// <summary>
        ///   Gets the UI theme, containing information about the theme used in the app.
        /// </summary>
        [field: SerializeField]
        public UITheme UiTheme { get; private set; } = null!;
    }
}