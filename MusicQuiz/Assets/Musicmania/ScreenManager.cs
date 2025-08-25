#nullable enable

using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.Screens;
using Musicmania.Ui.Screens;
using Musicmania.Ui.Screens.MainMenu;
using System.Collections.Generic;

namespace Musicmania
{
    /// <summary>
    ///     The ScreenManager class is responsible for managing the different screens in the application.
    ///     It provides methods to show and hide these screens.
    /// </summary>
    public class ScreenManager
    {
        private readonly ScreenCreator screenCreator;
        private MainScreen? mainScreen;
        private QuizScreen? quizScreen;

        private MainScreen MainScreen => mainScreen != null ? mainScreen : throw new NotInitializedException();
        private QuizScreen QuizScreen => quizScreen != null ? quizScreen : throw new NotInitializedException();

        private ScreenBase? currentScreen;

        /// <summary>
        ///     Initializes a new instance of the ScreenManager class.
        /// </summary>
        /// <param name="context">The MusicQuizContext instance.</param>
        public ScreenManager(MusicmaniaContext context)
        {
            screenCreator = new ScreenCreator(context);
        }

        /// <summary>
        ///     Shows the QuizScreen. 
        ///     If it's not initialized, it creates a new one.
        /// </summary>
        /// <param name="category">The CategoryDiskData instance.</param>
        public void ShowQuizScreen(CategoryData category)
        {
            if (quizScreen == null)
            {
                quizScreen = screenCreator.CreateQuizScreen();
            }

            if (currentScreen != quizScreen && currentScreen != null)
            {
                currentScreen.Hide();
            }

            QuizScreen.Show(category);
        }

        /// <summary>
        ///     Shows the CategoryScreen. 
        ///     If it's not initialized, it creates a new one.
        /// </summary>
        public void ShowMainScreen()
        {
            if (mainScreen == null)
            {
                mainScreen = screenCreator.CreateMainScreen();
            }

            MainScreen.Show();
        }
    }
}
