#nullable enable

using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.Settings;
using Musicmania.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Musicmania
{
    /// <summary>
    ///     The AppInitializer class is responsible for initializing the application.
    ///     It sets up the necessary data and shows the category screen at the start.
    /// </summary>
    public class AppInitializer : MonoBehaviour
    {
        /// <summary>
        ///     Serialized field for the application data.
        /// </summary>
        [SerializeField]
        private MusicmaniaSettings? settings;

        /// <summary>
        ///     Serialized field for the audio player.
        /// </summary>
        [SerializeField]
        private AudioPlayer? audioPlayer;

        void Awake()
        {
            SerializeFieldNotAssignedException.ThrowIfNull(settings, nameof(settings));
            SerializeFieldNotAssignedException.ThrowIfNull(audioPlayer, nameof(audioPlayer));

            var context = new MusicmaniaContext(settings, audioPlayer);

            context.ScreenManager.ShowMainScreen();
        }
    }
}
