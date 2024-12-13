#nullable enable

using Musicmania.Disks;
using Musicmania.Exceptions;
using Musicmania.Screens;
using Musicmania.Ui.Screens;
using UnityEngine;

namespace Musicmania.Settings
{
    [CreateAssetMenu(fileName = "PrefabSettings", menuName = "Musicmania/Prefab Settings")]
    public class PrefabSettings : ScriptableObject
    {
        [SerializeField]
        private CategoryScreen? categoryScreenPrefab;

        [SerializeField]
        private QuizScreen? quizScreenPrefab;

        [SerializeField]
        private CategoryDisk? categoryDiskPrefab;

        [SerializeField]
        private QuestionDisk? questionDiskPrefab;

        /// <summary>
        ///     The CategoryDisk prefab that is used to display the available quiz-categories.
        /// </summary>
        public CategoryDisk CategoryDiskPrefab => categoryDiskPrefab != null ? categoryDiskPrefab : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///    The QuestionDisk prefab that is used to display the available questions of a quiz-category.
        /// </summary>
        public QuestionDisk QuestionDiskPrefab => questionDiskPrefab != null ? questionDiskPrefab : throw new SerializeFieldNotAssignedException();

        public CategoryScreen CategoryScreenPrefab => categoryScreenPrefab != null ? categoryScreenPrefab : throw new SerializeFieldNotAssignedException();

        public QuizScreen QuizScreenPrefab => quizScreenPrefab != null ? quizScreenPrefab : throw new SerializeFieldNotAssignedException();
    }
}