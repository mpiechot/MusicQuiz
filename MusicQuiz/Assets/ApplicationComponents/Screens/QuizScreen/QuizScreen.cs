#nullable enable

using MusicQuiz.Disks;
using MusicQuiz.Exceptions;
using MusicQuiz.Extensions;
using MusicQuiz.Management;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace MusicQuiz.Screens
{
    public class QuizScreen : MonoBehaviour
    {
        [SerializeField]
        private RectTransform? questionParent;

        [SerializeField]
        private TMP_InputField? answerInputField;

        [SerializeField]
        private TMP_Dropdown? difficultyDropdown;

        private QuizManager? quizManager;
        private QuestionDisk? diskPrefab;
        private DiskPlayer? diskPlayer;

        private TMP_InputField AnswerInputField => answerInputField != null ? answerInputField : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///     Initializes the QuizScreen.
        /// </summary>
        /// <param name="appData">The appData to use.</param>
        /// <param name="diskPlayer">The diskPlayer to use.</param>
        public void Initialize(AppData appData)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(difficultyDropdown, nameof(difficultyDropdown));

            diskPlayer = GameObject.FindObjectOfType<DiskPlayer>();
            quizManager = new QuizManager(diskPlayer);
            diskPrefab = appData.QuestionDiskPrefab;

            difficultyDropdown.ClearOptions();
            var difficulties = Enum.GetValues(typeof(QuestionDifficulty)).Cast<QuestionDifficulty>();
            difficultyDropdown.AddOptions(difficulties.Select(x => x.ToString()).ToList());
        }

        /// <summary>
        ///     Shows the QuizScreen.
        /// </summary>
        /// <param name="categoryToload">The question category to show.</param>
        public void Show(CategoryDiskData categoryToload)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

            gameObject.SetActive(true);

            var shuffledQuestions = categoryToload.Questions.Shuffle().ToList();

            for (var i = 0; i < shuffledQuestions.Count; i++)
            {
                var question = shuffledQuestions[i];
                var disk = Instantiate(diskPrefab, questionParent);
                disk.Initialize((i + 1) + "", () => SelectQuestion(disk));
                disk.AssignQuestionData(question);
                Debug.Log("Created Question Disk for: " + question.DiskName);
            }
        }

        /// <summary>
        ///    Hides the QuizScreen.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        ///    Called when the CheckAnswer button is pressed.
        /// </summary>
        public void OnCheckAnswerPressed()
        {
            quizManager?.CheckAnswer(AnswerInputField.text);
        }

        public void OnDifficultyChange()
        {
            if (quizManager == null || quizManager.CurrentDisk == null || difficultyDropdown == null)
            {
                return;
            }

            quizManager.CurrentDisk.Data.Difficulty = (QuestionDifficulty)difficultyDropdown.value;
        }

        private void SelectQuestion(QuestionDisk questionDisk)
        {
            quizManager?.SelectDisk(questionDisk);


            AnswerInputField.text = questionDisk.Data.LastAnswer;
            difficultyDropdown?.SetValueWithoutNotify((int)questionDisk.Data.Difficulty);
        }
    }
}