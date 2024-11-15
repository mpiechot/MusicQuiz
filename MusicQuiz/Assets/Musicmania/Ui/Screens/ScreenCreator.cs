#nullable enable

using Musicmania.Settings;
using UnityEngine;

namespace Musicmania.Screens
{
    public class ScreenCreator
    {
        private QuizScreen quizScreenPrefab;
        private CategoryScreen categoryScreenPrefab;
        private ScreenManager screenManager;
        private MusicmaniaSettings settings;

        public ScreenCreator(ScreenManager screenManager, MusicmaniaContext context)
        {
            this.screenManager = screenManager;
            this.settings = context.Settings;
            quizScreenPrefab = settings.PrefabSettings.QuizScreenPrefab;
            categoryScreenPrefab = settings.PrefabSettings.CategoryScreenPrefab;
        }

        public QuizScreen CreateQuizScreen()
        {
            var quizScreen = GameObject.Instantiate(quizScreenPrefab);
            quizScreen.Initialize(settings);
            return quizScreen;
        }

        public CategoryScreen CreateCategoryScreen()
        {
            var categoryScreen = GameObject.Instantiate(categoryScreenPrefab);
            categoryScreen.Initialize(settings, screenManager);
            return categoryScreen;
        }
    }
}
