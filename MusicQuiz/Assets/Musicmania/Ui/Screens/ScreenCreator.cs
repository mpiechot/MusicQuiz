#nullable enable

using Musicmania.Settings;
using Musicmania.Ui.Screens;
using Musicmania.Ui.Screens.MainMenu;
using UnityEngine;

namespace Musicmania.Screens
{
    public class ScreenCreator
    {
        private QuizScreen quizScreenPrefab;
        private MainScreen mainScreenPrefab;
        private readonly MusicmaniaContext context;

        public ScreenCreator(MusicmaniaContext context)
        {
            this.context = context;
            quizScreenPrefab = context.Settings.ScreenPrefabCollection.QuizScreenPrefab;
            mainScreenPrefab = context.Settings.ScreenPrefabCollection.MainScreenPrefab;
        }

        public QuizScreen CreateQuizScreen()
        {
            var quizScreen = GameObject.Instantiate(quizScreenPrefab);
            quizScreen.Initialize(context);
            return quizScreen;
        }

        public MainScreen CreateMainScreen()
        {
            var mainScreen = GameObject.Instantiate(mainScreenPrefab);
            mainScreen.Initialize(context);
            return mainScreen;
        }
    }
}
