#nullable enable

using Musicmania.Exceptions;
using Musicmania.Settings.Ui;
using UnityEngine;

namespace Musicmania.Settings
{
    [CreateAssetMenu(fileName = "MusicmaniaSettings", menuName = "Musicmania/Settings")]
    public class MusicmaniaSettings : ScriptableObject
    {
        [SerializeField]
        private ResourceSettings? resourceSettings;

        [SerializeField]
        private ScreenPrefabCollection? screenPrefabCollection;

        [SerializeField]
        private DataPrefabCollection? dataPrefabCollection;

        [SerializeField]
        private ColorProfile? colorProfile;

        [SerializeField]
        private UITheme? uiTheme;

        /// <summary>
        ///    Gets the resource settings, containing information about where resources should be loaded/saved from/to.
        /// </summary>
        public ResourceSettings ResourceSettings =>
            SerializeFieldNotAssignedException.ThrowIfNull(resourceSettings);

        /// <summary>
        ///   Gets the screen prefab collection, containing references to the prefabs used for different screens in the app.
        /// </summary>
        public ScreenPrefabCollection ScreenPrefabCollection =>
            SerializeFieldNotAssignedException.ThrowIfNull(screenPrefabCollection);

        /// <summary>
        ///   Gets the data prefab collection, containing references to the prefabs used for different data from <see cref="Data"/>.
        /// </summary>
        public DataPrefabCollection DataPrefabCollection =>
            SerializeFieldNotAssignedException.ThrowIfNull(dataPrefabCollection);

        /// <summary>
        ///   Gets the color profile, containing information about the colors used in the app.
        /// </summary>
        public ColorProfile ColorProfile =>
            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile);

        /// <summary>
        ///   Gets the UI theme, containing information about the theme used in the app.
        /// </summary>
        public UITheme UiTheme =>
            SerializeFieldNotAssignedException.ThrowIfNull(uiTheme);
    }
}
