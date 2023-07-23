#nullable enable

using MusicQuiz.Disks;
using MusicQuiz.Exceptions;
using System.Collections.Generic;
using UnityEngine;

namespace MusicQuiz.Management
{
    [CreateAssetMenu(fileName = "AppData", menuName = "MusicQuiz/App Data")]
    public class AppData : ScriptableObject
    {
        [SerializeField]
        private MusicQuizScreens? musicQuizScreens;

        [SerializeField]
        private CategoryDisk? categoryDiskPrefab;

        [SerializeField]
        private QuestionDisk? questionDiskPrefab;

        /// <summary>
        ///    The available quiz-categories the user can select in the app.
        /// </summary>
        [field: SerializeField]
        public List<CategoryDiskData> QuizCategories { get; private set; } = new();

        /// <summary>
        ///     The CategoryDisk prefab that is used to display the available quiz-categories.
        /// </summary>
        public CategoryDisk CategoryDiskPrefab => categoryDiskPrefab != null ? categoryDiskPrefab : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///    The QuestionDisk prefab that is used to display the available questions of a quiz-category.
        /// </summary>
        public QuestionDisk QuestionDiskPrefab => questionDiskPrefab != null ? questionDiskPrefab : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///    The MusicQuizScreens that are used to display the different screens of the app.
        /// </summary>
        public MusicQuizScreens MusicQuizScreens => musicQuizScreens != null ? musicQuizScreens : throw new SerializeFieldNotAssignedException();
    }
}