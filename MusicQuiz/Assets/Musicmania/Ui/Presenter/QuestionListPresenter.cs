#nullable enable

using System;
using System.Collections.Generic;
using Musicmania;
using Musicmania.Questions;
using Musicmania.Settings.Ui;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Presenter
{
    /// <summary>
    ///     Presents a list of questions using UI Toolkit.
    /// </summary>
    public sealed class QuestionListPresenter : VisualElement, IDisposable
    {
        private readonly MusicmaniaContext context;
        private readonly List<QuestionPresenter> presenters = new();
        private readonly string category;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuestionListPresenter"/> class.
        /// </summary>
        /// <param name="contextToUse">The application context.</param>
        /// <param name="parent">Parent element to attach to.</param>
        /// <param name="questions">Questions to present.</param>
        /// <param name="categoryName">Current quiz category.</param>
        public QuestionListPresenter(MusicmaniaContext contextToUse, VisualElement parent, IReadOnlyList<QuestionData> questions, string categoryName)
        {
            context = contextToUse ?? throw new ArgumentNullException(nameof(contextToUse));
            _ = parent ?? throw new ArgumentNullException(nameof(parent));
            _ = questions ?? throw new ArgumentNullException(nameof(questions));
            category = categoryName ?? throw new ArgumentNullException(nameof(categoryName));

            style.flexDirection = FlexDirection.Column;
            parent.Add(this);

            context.ThemeProvider.ThemeChanged += OnThemeChanged;

            foreach (var question in questions)
            {
                var presenter = new QuestionPresenter(question, category, Theme.ButtonStyle, context);
                presenter.OnClicked += OnPresenterClicked;
                Add(presenter);
                presenters.Add(presenter);
            }
        }

        /// <summary>
        ///     Raised when one of the questions is clicked.
        /// </summary>
        public event EventHandler<QuestionPresenter>? QuestionClicked;

        /// <inheritdoc />
        public void Dispose()
        {
            context.ThemeProvider.ThemeChanged -= OnThemeChanged;

            foreach (var presenter in presenters)
            {
                presenter.OnClicked -= OnPresenterClicked;
                presenter.Dispose();
            }
        }

        private UITheme Theme => context.ThemeProvider.CurrentTheme;

        private void OnThemeChanged(object? sender, UITheme e)
        {
            foreach (var presenter in presenters)
            {
                presenter.SetTheme(e.ButtonStyle);
            }
        }

        private void OnPresenterClicked(object? sender, EventArgs e)
        {
            if (sender is QuestionPresenter presenter)
            {
                QuestionClicked?.Invoke(this, presenter);
            }
        }
    }
}
