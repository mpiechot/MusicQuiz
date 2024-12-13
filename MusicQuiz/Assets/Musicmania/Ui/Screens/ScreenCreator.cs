#nullable enable

using Musicmania.Settings;
using Musicmania.Ui.Screens;
using UnityEngine;

namespace Musicmania.Screens
{
    public class ScreenCreator
    {
        private QuizScreen quizScreenPrefab;
        private CategoryScreen categoryScreenPrefab;
        private readonly MusicmaniaContext context;

        public ScreenCreator(MusicmaniaContext context)
        {
            this.context = context;
            quizScreenPrefab = context.Settings.PrefabSettings.QuizScreenPrefab;
            categoryScreenPrefab = context.Settings.PrefabSettings.CategoryScreenPrefab;
        }

        public QuizScreen CreateQuizScreen()
        {
            var quizScreen = GameObject.Instantiate(quizScreenPrefab);
            quizScreen.Initialize(context);
            return quizScreen;
        }

        public CategoryScreen CreateCategoryScreen()
        {
            var categoryScreen = GameObject.Instantiate(categoryScreenPrefab);
            categoryScreen.Initialize(context);
            return categoryScreen;
        }
    }
}
