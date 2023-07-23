#nullable enable

using MusicQuiz.Exceptions;
using MusicQuiz.Screens;
using UnityEngine;

namespace MusicQuiz.Management
{
    /// <summary>
    /// 
    /// </summary>
    [CreateAssetMenu(fileName = "MusicQuizScreens", menuName = "MusicQuiz/Music Quiz Screens")]
    public class MusicQuizScreens : ScriptableObject
    {
        [SerializeField]
        private CategoryScreen? categoryScreenPrefab;

        [SerializeField]
        private QuizScreen? quizScreenPrefab;

        public CategoryScreen CategoryScreenPrefab => categoryScreenPrefab != null ? categoryScreenPrefab : throw new SerializeFieldNotAssignedException();

        public QuizScreen QuizScreenPrefab => quizScreenPrefab != null ? quizScreenPrefab : throw new SerializeFieldNotAssignedException();
    }
}