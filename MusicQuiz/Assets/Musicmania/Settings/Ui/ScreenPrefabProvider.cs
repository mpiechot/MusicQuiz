#nullable enable

using Musicmania.Exceptions;
using Musicmania.Ui.Screens;
using Musicmania.Ui.Screens.MainMenu;
using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "ScreenPrefabProvider", menuName = "Musicmania/Screen Prefab Provider")]
    public class ScreenPrefabProvider : ScriptableObject
    {
        [SerializeField]
        private MainScreen? mainScreenPrefab;

        [SerializeField]
        private QuizScreen? quizScreenPrefab;

        public MainScreen MainScreenPrefab => mainScreenPrefab != null ? mainScreenPrefab : throw new SerializeFieldNotAssignedException();

        public QuizScreen QuizScreenPrefab => quizScreenPrefab != null ? quizScreenPrefab : throw new SerializeFieldNotAssignedException();
    }
}