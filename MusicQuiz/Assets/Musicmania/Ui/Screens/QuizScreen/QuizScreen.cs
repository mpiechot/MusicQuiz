#nullable enable

using Musicmania.Data;
using Musicmania.Disks;
using Musicmania.Exceptions;
using Musicmania.Extensions;
using Musicmania.SaveManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Musicmania.Ui.Screens
{
    public class QuizScreen : ScreenBase
    {
        [SerializeField]
        private RectTransform? questionParent;

        [SerializeField]
        private TMP_InputField? answerInputField;

        [SerializeField]
        private TMP_Text? askForExactNameLabel;

        private QuestionDisk? diskPrefab;

        private QuestionDisk? selectedQuestionDisk;

        private CategoryData? selectedCategory;

        private List<QuestionDisk> currentQuestionDisks = new();

        private TMP_InputField AnswerInputField => answerInputField != null ? answerInputField : throw new SerializeFieldNotAssignedException();

        private TMP_Text AskForExactNameLabel => SerializeFieldNotAssignedException.ThrowIfNull(askForExactNameLabel, nameof(askForExactNameLabel));

        private int[] diskYOffsets = new int[] { 40, 0, -40, -100, -140, -190 };

        public override void Initialize(MusicmaniaContext context)
        {
            base.Initialize(context);
            diskPrefab = Context.Settings.PrefabSettings.QuestionDiskPrefab;

            AskForExactNameLabel.text = string.Empty;
        }

        public void AssignCategoryData(CategoryData categoryToload)
        {
            selectedCategory = categoryToload;
        }

        /// <summary>
        ///     Shows the QuizScreen.
        /// </summary>
        public override void Show()
        {
            CreateQuestionDisks();

            gameObject.SetActive(true);
        }

        private void CreateQuestionDisks()
        {
            if (selectedCategory == null)
            {
                throw new InvalidOperationException("No category selected.");
            }

            var questions = Context.QuestionStorage.GetAllQuestionsWithTags(selectedCategory.Tags);

            if (currentQuestionDisks.Count > 0)
            {
                foreach (var questionDisk in currentQuestionDisks)
                {
                    questionDisk.DiskClicked -= OnDiskClicked;
                    Destroy(questionDisk.gameObject);
                }

                currentQuestionDisks.Clear();
            }

            var questionsToUse = OrderQuestions(questions);

            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

            for (var questionPosition = 0; questionPosition < questionsToUse.Count(); questionPosition++)
            {
                var questionData = questionsToUse[questionPosition];

                var disk = Instantiate(diskPrefab, questionParent);
                disk.Initialize((questionPosition + 1) + "", Context);
                disk.DiskClicked += OnDiskClicked;
                disk.AssignData(questionData);
                disk.Data.Position = questionPosition;
                var randomDiskOffset = UnityEngine.Random.Range(0, diskYOffsets.Length);
                disk.SetDiskYOffset(diskYOffsets[randomDiskOffset]);

                if (!string.IsNullOrEmpty(questionData.LastAnswer))
                {
                    disk.CheckAnswer(questionData.LastAnswer);
                }

                currentQuestionDisks.Add(disk);
                Debug.Log("Created Question Disk for: " + questionData.Name);
            }
        }

        private static IReadOnlyList<QuestionData> OrderQuestions(IReadOnlyList<QuestionData>? questions)
        {
            var orderedQuestions = new List<QuestionData>();
            var questionsToInsert = questions.Where(question => question.Position == -1).ToList();

            var groupedQuestions = questions
                .Where(question => question.Position != -1)
                .GroupBy(question => question.Difficulty)
                .OrderBy(group => group.Key)
                .ToList();

            foreach (var group in groupedQuestions)
            {
                group.OrderBy(question => question.Position);

                foreach (var question in group)
                {
                    orderedQuestions.Add(question);
                }

                var questionsToInsertInGroup = questionsToInsert.Where(question => question.Difficulty == group.Key).Shuffle().ToList();
                if (questionsToInsertInGroup.Count > 0)
                {
                    foreach (var newQuestion in questionsToInsertInGroup)
                    {
                        var position = group.Max(question => question.Position) + 1;
                        orderedQuestions.Add(newQuestion);
                    }
                }
            }


            return orderedQuestions;
        }

        /// <summary>
        ///    Called by Unity when the CheckAnswer button is pressed.
        /// </summary>
        public void OnCheckAnswerPressed()
        {
            selectedQuestionDisk?.CheckAnswer(AnswerInputField.text);
        }

        /// <summary>
        ///    Called by Unity when the back button is clicked.
        /// </summary>
        public void OnBackToCategoryScreenClicked()
        {
            foreach (var question in currentQuestionDisks)
            {
                Context.QuestionSaveContainerManager.SaveQuestionSaves(question.Data.QuestionSaves, question.Data.Name);
            }

            Context.AudioPlayer.Stop();

            Context.ScreenManager.ShowCategoryScreen();
        }

        private void OnDiskClicked(object sender, EventArgs args)
        {
            if (sender is not QuestionDisk questionDisk)
            {
                throw new InvalidOperationException($"Expected a QuestionDisk, but got {sender?.GetType().Name}.");
            }

            AnswerInputField.text = questionDisk.Data.LastAnswer;
            AskForExactNameLabel.text = questionDisk.Data.AskForExactName ? "Bitte nenne den exakten Namen" : "Bitte nenne nur die Serie und nicht den ggf. exakten Teil des Spiels";

            selectedQuestionDisk = questionDisk;
        }
    }
}