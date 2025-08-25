using Musicmania.ResourceManagement;
using Musicmania.Settings;
using Musicmania.Ui.Theming;

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
        /// <param name="settings">The application data to be used.</param>
        public MusicmaniaContext(MusicmaniaSettings settings, AudioPlayer audioPlayer)
        {
            Settings = settings;
            ScreenManager = new ScreenManager(this);
            AudioPlayer = audioPlayer;
            ResourceManager = new ResourceManager(settings.ResourceSettings);
            ThemeProvider = new ThemeProvider(settings.UiTheme, settings.ColorProfile);
        }

        /// <summary>
        ///    Gets the AppData.
        /// </summary>
        public MusicmaniaSettings Settings { get; }

        /// <summary>
        ///     Gets the theme provider.
        /// </summary>
        public IThemeProvider ThemeProvider { get; }

        /// <summary>
        ///     Gets the SceneManager.
        /// </summary>
        public ScreenManager ScreenManager { get; }


        /// <summary>
        ///     Gets the RessourceManager.
        /// </summary>
        public ResourceManager ResourceManager { get; }

        /// <summary>
        ///     Gets the AudioPlayer.
        /// </summary>
        public AudioPlayer AudioPlayer { get; }
    }
}
