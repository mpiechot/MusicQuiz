#nullable enable

using MusicQuiz.Disks;
using MusicQuiz.Exceptions;
using MusicQuiz.Extensions;
using MusicQuiz.Management;
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

        private QuestionSelectionManager? quizManager;
        private QuestionDisk? diskPrefab;
        private DiskPlayer? diskPlayer;

        private TMP_InputField AnswerInputField => answerInputField != null ? answerInputField : throw new SerializeFieldNotAssignedException();

        private DiskPlayer DiskPlayer => diskPlayer != null ? diskPlayer : throw new SerializeFieldNotAssignedException();

        private QuestionSelectionManager QuestionSelectionManager => quizManager != null ? quizManager : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///     Initializes the QuizScreen.
        /// </summary>
        /// <param name="appData">The appData to use.</param>
        /// <param name="diskPlayer">The diskPlayer to use.</param>
        public void Initialize(AppData appData)
        {
            diskPlayer = GameObject.FindObjectOfType<DiskPlayer>();
            quizManager = new QuestionSelectionManager(diskPlayer);
            diskPrefab = appData.QuestionDiskPrefab;
        }

        /// <summary>
        ///     Shows the QuizScreen.
        /// </summary>
        /// <param name="categoryToload">The question category to show.</param>
        public void Show(CategoryDiskData categoryToload)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

            gameObject.SetActive(true);

            var shuffledQuestions = categoryToload.Questions
                .GroupBy(question => question.Difficulty)
                .OrderBy(group => group.Key)
                .Select(group => group.Shuffle())
                .SelectMany(group => group)
                .ToList();

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

        private void SelectQuestion(QuestionDisk questionDisk)
        {
            Debug.Log("Question selected: " + questionDisk.Data.DiskName + " - " + questionDisk.Data.Difficulty);
            quizManager?.SelectDisk(questionDisk);
            DiskPlayer.LoadAndPlay(questionDisk.Data.Audio);

            AnswerInputField.text = questionDisk.Data.LastAnswer;
        }
    }
}