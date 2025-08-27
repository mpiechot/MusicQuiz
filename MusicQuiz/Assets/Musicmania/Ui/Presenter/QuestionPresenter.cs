#nullable enable

using System;
using System.Linq;
using Musicmania;
using Musicmania.Questions;
using Musicmania.Settings.Ui;
using Musicmania.Ui.Controls;
using Musicmania.ResourceManagement;
using UnityEngine;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Presenter
{
    /// <summary>
    ///     Presents a question and plays its audio when clicked.
    /// </summary>
    public sealed class QuestionPresenter : VisualElement, IDisposable
    {
        private readonly ButtonControl button;
        private readonly MusicmaniaContext context;
        private readonly string category;
        private readonly ResourceHandle<AudioClip> audioResourceHandle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuestionPresenter"/> class.
        /// </summary>
        /// <param name="question">Question to display.</param>
        /// <param name="categoryName">Current quiz category.</param>
        /// <param name="buttonStyle">Style to apply to the button.</param>
        /// <param name="contextToUse">The application context.</param>
        public QuestionPresenter(QuestionData question, string categoryName, ButtonStyle buttonStyle, MusicmaniaContext contextToUse)
        {
            Question = question ?? throw new ArgumentNullException(nameof(question));
            category = categoryName ?? throw new ArgumentNullException(nameof(categoryName));
            context = contextToUse ?? throw new ArgumentNullException(nameof(contextToUse));

            button = new ButtonControl(buttonStyle ?? throw new ArgumentNullException(nameof(buttonStyle)))
            {
                Text = question.Name,
            };
            button.OnClick += OnButtonClicked;
            Add(button);

            audioResourceHandle = context.ResourceManager.GetResource<AudioClip>(question.AudioResourceKey);
        }

        /// <summary>
        ///     Gets the question represented by this presenter.
        /// </summary>
        public QuestionData Question { get; }

        /// <summary>
        ///     Gets the current category name.
        /// </summary>
        public string Category => category;

        /// <summary>
        ///     Raised when the presenter is clicked.
        /// </summary>
        public event EventHandler? OnClicked;

        /// <summary>
        ///     Applies a new theme to the underlying button.
        /// </summary>
        public void SetTheme(ButtonStyle style)
        {
            button.SetTheme(style ?? throw new ArgumentNullException(nameof(style)));
        }

        /// <summary>
        ///     Returns the save data for the current category, if any.
        /// </summary>
        public QuestionSaveData? GetSaveData() => Question.QuestionSaves.FirstOrDefault(s => s.Category == category);

        /// <summary>
        ///     Updates or creates the save data for the current category.
        /// </summary>
        public void UpdateSaveData(string answer)
        {
            var save = GetSaveData();
            if (save == null)
            {
                save = new QuestionSaveData { Category = category };
                Question.QuestionSaves.Add(save);
            }

            save.LastAnswer = answer;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            audioResourceHandle.Unload();
            button.OnClick -= OnButtonClicked;
            button.Dispose();
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            context.AudioPlayer.LoadAndPlay(audioResourceHandle);
            OnClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
