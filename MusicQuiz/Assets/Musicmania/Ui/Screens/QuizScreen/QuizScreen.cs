#nullable enable

using Musicmania.Data;
using Musicmania.Disks;
using Musicmania.Exceptions;
using Musicmania.Extensions;
using Musicmania.Settings;
using NUnit.Framework;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Musicmania.Screens
{
    public class QuizScreen : MonoBehaviour
    {
        [SerializeField]
        private RectTransform? questionParent;

        [SerializeField]
        private TMP_InputField? answerInputField;

        private MusicmaniaContext? context;

        private MusicmaniaContext Context => NotInitializedException.ThrowIfNull(context, nameof(context));

        private QuestionSelectionManager? quizManager;
        private QuestionDisk? diskPrefab;
        private DiskPlayer? diskPlayer;

        private TMP_InputField AnswerInputField => answerInputField != null ? answerInputField : throw new SerializeFieldNotAssignedException();

        private DiskPlayer DiskPlayer => diskPlayer != null ? diskPlayer : throw new SerializeFieldNotAssignedException();

        private QuestionSelectionManager QuestionSelectionManager => quizManager != null ? quizManager : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///     Initializes the QuizScreen.
        /// </summary>
        /// <param name="contextToUse">The appData to use.</param>
        /// <param name="diskPlayer">The diskPlayer to use.</param>
        public void Initialize(MusicmaniaContext contextToUse)
        {
            context = contextToUse;
            diskPlayer = GameObject.FindObjectOfType<DiskPlayer>();
            quizManager = new QuestionSelectionManager(diskPlayer);
            diskPrefab = contextToUse.Settings.PrefabSettings.QuestionDiskPrefab;
        }

        /// <summary>
        ///     Shows the QuizScreen.
        /// </summary>
        /// <param name="categoryToload">The question category to show.</param>
        public void Show(CategoryData categoryToload)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

            gameObject.SetActive(true);

            var questions = Context.QuestionStorage.GetAllQuestionsWithTags(categoryToload.Tags);

            var shuffledQuestions = questions
                .GroupBy(question => question.Difficulty)
                .OrderBy(group => group.Key)
                .Select(group => group.Shuffle())
                .SelectMany(group => group)
                .ToList();

            for (var i = 0; i < shuffledQuestions.Count; i++)
            {
                var question = shuffledQuestions[i];
                var disk = Instantiate(diskPrefab, questionParent);
                disk.Initialize((i + 1) + "", Context);
                disk.DiskClicked += OnDiskClicked;
                disk.AssignData(question);
                Debug.Log("Created Question Disk for: " + question.Name);
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

        private void OnDiskClicked(object sender, EventArgs args)
        {
            if (sender is not QuestionDisk questionDisk)
            {
                throw new InvalidOperationException($"Expected a QuestionDisk, but got {sender?.GetType().Name}.");
            }

            AnswerInputField.text = questionDisk.Data.LastAnswer;
        }
    }
}