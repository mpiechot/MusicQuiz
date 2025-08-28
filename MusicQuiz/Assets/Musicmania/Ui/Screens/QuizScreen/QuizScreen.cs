#nullable enable

using System;
using System.Collections.Generic;
using Musicmania;
using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.Questions;
using Musicmania.Ui.Controls;
using Musicmania.Ui.Presenter;
using UnityEngine;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Screens
{
    /// <summary>
    ///     Screen for running the music quiz.
    /// </summary>
    public class QuizScreen : ScreenBase
    {
        [SerializeField]
        private UIDocument? document;

        private QuizScreenNavigationComposite? navigation;
        private QuestionListPresenter? questionListPresenter;
        private TextInputControl? answerInput;
        private QuestionPresenter? selectedQuestion;
        private CategoryData? selectedCategory;

        private UIDocument Document => SerializeFieldNotAssignedException.ThrowIfNull(document);

        private QuizScreenNavigationComposite Navigation => NotInitializedException.ThrowIfNull(navigation);

        private TextInputControl AnswerInput => NotInitializedException.ThrowIfNull(answerInput);

        private QuestionListPresenter QuestionListPresenter =>
            ShowNotCalledException.ThrowIfNull(questionListPresenter);

        /// <inheritdoc />
        public override void Initialize(MusicmaniaContext context)
        {
            base.Initialize(context);

            var root = Document.rootVisualElement;
            var navigationRoot = root.Q("quizScreenNavigation") ?? root;
            navigation = new QuizScreenNavigationComposite(context, navigationRoot);

            var inputRoot = root.Q("answerInputRoot") ?? root;
            answerInput = new TextInputControl();
            inputRoot.Add(answerInput);
            answerInput.OnTextChanged += OnAnswerChanged;
        }

        /// <summary>
        ///     Shows the quiz screen for the given category with the provided questions.
        /// </summary>
        public void Show(CategoryData categoryToLoad, IReadOnlyList<QuestionData> questions)
        {
            selectedCategory = categoryToLoad ?? throw new ArgumentNullException(nameof(categoryToLoad));

            var root = Document.rootVisualElement;
            var listRoot = root.Q("questionList") ?? root;

            questionListPresenter?.Dispose();
            questionListPresenter = new QuestionListPresenter(Context, listRoot, questions, selectedCategory.Name);
            questionListPresenter.QuestionClicked += OnQuestionClicked;

            AnswerInput.Text = string.Empty;

            base.Show();
            Navigation.Show();
        }

        /// <summary>
        ///     Shows the quiz screen with no questions.
        /// </summary>
        public void Show(CategoryData categoryToLoad) => Show(categoryToLoad, Array.Empty<QuestionData>());

        /// <inheritdoc />
        public override void Hide()
        {
            Navigation.Hide();

            QuestionListPresenter.QuestionClicked -= OnQuestionClicked;
            QuestionListPresenter.Dispose();
            questionListPresenter = null;

            base.Hide();
        }

        private void OnQuestionClicked(object? sender, QuestionPresenter presenter)
        {
            selectedQuestion = presenter;
            var save = presenter.GetSaveData();
            AnswerInput.Text = save?.LastAnswer ?? string.Empty;
        }

        private void OnAnswerChanged(object? sender, string text)
        {
            if (selectedQuestion == null)
            {
                return;
            }

            selectedQuestion.UpdateSaveData(text);

            if (string.Equals(selectedQuestion.Question.Name, text, StringComparison.OrdinalIgnoreCase))
            {
                OnCorrectAnswer();
            }
        }

        private void OnCorrectAnswer()
        {
            // Future implementation for correct answers.
        }

        private void OnDestroy()
        {
            navigation?.Dispose();
            questionListPresenter?.Dispose();

            if (answerInput != null)
            {
                AnswerInput.OnTextChanged -= OnAnswerChanged;
                AnswerInput.Dispose();
            }
        }
    }
}
