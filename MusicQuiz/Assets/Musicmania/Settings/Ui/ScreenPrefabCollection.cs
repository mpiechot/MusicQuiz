#nullable enable

using Musicmania.Exceptions;
using Musicmania.Ui.Screens;
using Musicmania.Ui.Screens.MainMenu;
using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "ScreenPrefabCollection", menuName = "Musicmania/Screen Prefab Collection")]
    public class ScreenPrefabCollection : ScriptableObject
    {
        [SerializeField]
        private MainScreen? mainScreenPrefab;

        [SerializeField]
        private QuizScreen? quizScreenPrefab;

        public MainScreen MainScreenPrefab => SerializeFieldNotAssignedException.ThrowIfNull(mainScreenPrefab);

        public QuizScreen QuizScreenPrefab => SerializeFieldNotAssignedException.ThrowIfNull(quizScreenPrefab);
    }
}
