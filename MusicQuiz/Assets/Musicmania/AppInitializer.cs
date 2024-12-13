#nullable enable

using Musicmania.Data;
using Musicmania.Exceptions;
using Musicmania.Settings;
using Musicmania.Util;
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

        /// <summary>
        ///    The available quiz-categories the user can select in the app.
        /// </summary>
        [field: SerializeField]
        public List<CategoryData> QuizCategories { get; private set; } = new();

        /// <summary>
        ///    The available questions which can be asked in the quiz.
        /// </summary>
        [field: SerializeField]
        public List<QuestionData> AllQuestions { get; private set; } = new();

        /// <summary>
        ///     Serialized field for the color profile.
        /// </summary>
        [SerializeField]
        private ColorProfile? colorProfile;

        void Awake()
        {
            SerializeFieldNotAssignedException.ThrowIfNull(settings, nameof(settings));
            SerializeFieldNotAssignedException.ThrowIfNull(audioPlayer, nameof(audioPlayer));

            MusicmaniaContext context = new MusicmaniaContext(colorProfile, settings, AllQuestions, QuizCategories, audioPlayer);

            context.ScreenManager.ShowCategoryScreen();
        }
    }
}
