using Musicmania.Settings;
using Musicmania.Util;

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
        public MusicmaniaContext(ColorProfile colorProfile, MusicmaniaSettings settings, QuestionStorage storage)
        {
            ColorProfile = colorProfile;
            Settings = settings;
            SaveManager = new SaveManager(this);
            ScreenManager = new ScreenManager(this);
            QuestionStorage = storage;
        }

        /// <summary>
        ///    Gets the AppData.
        /// </summary>
        public MusicmaniaSettings Settings { get; }

        /// <summary>
        ///     Gets the SaveManager.
        /// </summary>
        public SaveManager SaveManager { get; }

        /// <summary>
        ///     Gets the SceneManager.
        /// </summary>
        public ScreenManager ScreenManager { get; }

        /// <summary>
        ///     Gets the ColorProfile.
        /// </summary>
        public ColorProfile ColorProfile { get; }

        public QuestionStorage QuestionStorage { get; }

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
