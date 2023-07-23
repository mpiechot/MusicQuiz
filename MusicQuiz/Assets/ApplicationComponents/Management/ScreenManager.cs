#nullable enable

using MusicQuiz.Disks;
using MusicQuiz.Exceptions;
using MusicQuiz.Screens;

namespace MusicQuiz.Management
{
    public class ScreenManager
    {
        private CategoryScreen? categoryScreen;
        private QuizScreen? quizScreen;
        private ScreenCreator? screenCreator;

        private ScreenCreator ScreenCreator => screenCreator ?? throw new NotInitializedException();

        public ScreenManager(AppData appData)
        {
            screenCreator = new ScreenCreator(this, appData);
        }

        public void ShowQuizScreen(CategoryDiskData category)
        {
            if (quizScreen == null)
            {
                quizScreen = ScreenCreator.CreateQuizScreen();
            }

            HideAllScreens();
            quizScreen.Show(category);
        }

        public void ShowCategoryScreen()
        {
            if (categoryScreen == null)
            {
                categoryScreen = ScreenCreator.CreateCategoryScreen();
            }

            HideAllScreens();
            categoryScreen.Show();
        }

        private void HideAllScreens()
        {
            quizScreen?.Hide();
            categoryScreen?.Hide();
        }
    }
}
