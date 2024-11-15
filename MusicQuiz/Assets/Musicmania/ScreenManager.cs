#nullable enable

using Musicmania.Data;
using Musicmania.Disks;
using Musicmania.Exceptions;
using Musicmania.Screens;
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
        private CategoryScreen? categoryScreen;
        private QuizScreen? quizScreen;

        private CategoryScreen CategoryScreen => categoryScreen != null ? categoryScreen : throw new NotInitializedException();
        private QuizScreen QuizScreen => quizScreen != null ? quizScreen : throw new NotInitializedException();

        /// <summary>
        ///     Initializes a new instance of the ScreenManager class.
        /// </summary>
        /// <param name="context">The MusicQuizContext instance.</param>
        public ScreenManager(MusicmaniaContext context)
        {
            screenCreator = new ScreenCreator(this, context);
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

            HideAllScreens();
            QuizScreen.Show(category);
        }

        /// <summary>
        ///     Shows the CategoryScreen. 
        ///     If it's not initialized, it creates a new one.
        /// </summary>
        public void ShowCategoryScreen(List<CategoryData> categories)
        {
            if (categoryScreen == null)
            {
                categoryScreen = screenCreator.CreateCategoryScreen();
            }

            HideAllScreens();
            CategoryScreen.Show(categories);
        }

        private void HideAllScreens()
        {
            if (quizScreen != null)
            {
                quizScreen.Hide();
            }

            if (categoryScreen != null)
            {
                categoryScreen.Hide();
            }
        }
    }
}
