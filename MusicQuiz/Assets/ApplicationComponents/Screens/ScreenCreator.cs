#nullable enable

using MusicQuiz.Management;
using UnityEngine;

namespace MusicQuiz.Screens
{
    public class ScreenCreator
    {
        private QuizScreen quizScreenPrefab;
        private CategoryScreen categoryScreenPrefab;
        private ScreenManager screenManager;
        private AppData appData;

        public ScreenCreator(ScreenManager screenManager, AppData appData)
        {
            this.screenManager = screenManager;
            this.appData = appData;
            quizScreenPrefab = appData.MusicQuizScreens.QuizScreenPrefab;
            categoryScreenPrefab = appData.MusicQuizScreens.CategoryScreenPrefab;
        }

        public QuizScreen CreateQuizScreen()
        {
            var quizScreen = GameObject.Instantiate(quizScreenPrefab);
            quizScreen.Initialize(appData);
            return quizScreen;
        }

        public CategoryScreen CreateCategoryScreen()
        {
            var categoryScreen = GameObject.Instantiate(categoryScreenPrefab);
            categoryScreen.Initialize(appData, screenManager);
            return categoryScreen;
        }
    }
}
