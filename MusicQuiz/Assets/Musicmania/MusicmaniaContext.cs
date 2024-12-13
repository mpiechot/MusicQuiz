using Musicmania.Data;
using Musicmania.SaveManagement;
using Musicmania.Settings;
using Musicmania.Util;
using System.Collections.Generic;

namespace Musicmania
{
    /// <summary>
    ///     Stores and provides access to general application components.
    /// </summary>
    public class MusicmaniaContext
    {
        /// <summary>
        ///     Initializes a new instance of a <see cref="MusicmaniaContext"/>.
        /// </summary>
        /// <param name="colorProfile">The color profile to be used.</param>
        /// <param name="settings">The application data to be used.</param>
        public MusicmaniaContext(ColorProfile colorProfile, MusicmaniaSettings settings, List<QuestionData> allQuestions, List<CategoryData> quizCategories, AudioPlayer audioPlayer)
        {
            ColorProfile = colorProfile;
            Settings = settings;
            QuestionSaveContainerManager = new QuestionSaveContainerManager(this);
            ScreenManager = new ScreenManager(this);
            QuestionStorage = new QuestionStorage(allQuestions, this);
            AudioPlayer = audioPlayer;
            CategoryStorage = new CategoryStorage(quizCategories);
        }


        /// <summary>
        ///    Gets the AppData.
        /// </summary>
        public MusicmaniaSettings Settings { get; }

        /// <summary>
        ///     Gets the SaveManager.
        /// </summary>
        public QuestionSaveContainerManager QuestionSaveContainerManager { get; }

        /// <summary>
        ///     Gets the SceneManager.
        /// </summary>
        public ScreenManager ScreenManager { get; }

        /// <summary>
        ///     Gets the ColorProfile.
        /// </summary>
        public ColorProfile ColorProfile { get; }

        public QuestionStorage QuestionStorage { get; }

        public CategoryStorage CategoryStorage { get; }

        /// <summary>
        ///     Gets the RessourceManager.
        /// </summary>
        public RessourceManager RessourceManager { get; }

        /// <summary>
        ///     Gets the AudioPlayer.
        /// </summary>
        public AudioPlayer AudioPlayer { get; }
    }
}
