#nullable enable

using System;
using Musicmania.Settings.Ui;
using Musicmania.Ui.Controls;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Screens.MainMenu
{
    /// <summary>
    ///     Composite for the main screen navigation using UI Toolkit.
    /// </summary>
    public sealed class MainScreenNavigationComposite : VisualElement, IDisposable
    {
        private readonly MusicmaniaContext context;

        private readonly ButtonControl settingsButton;
        private readonly ButtonControl openQuestionManagerButton;
        private readonly ButtonControl startRandomButton;
        private readonly ButtonControl quitButton;

        /// <summary>
        ///     Initializes a new instance of the <see cref="MainScreenNavigationComposite"/> class.
        /// </summary>
        /// <param name="contextToUse">The application context.</param>
        /// <param name="parent">The parent element to attach to.</param>
        /// <exception cref="ArgumentNullException">Thrown when any required parameter is null.</exception>
        public MainScreenNavigationComposite(MusicmaniaContext contextToUse, VisualElement parent)
        {
            context = contextToUse ?? throw new ArgumentNullException(nameof(contextToUse));
            _ = parent ?? throw new ArgumentNullException(nameof(parent));

            style.flexGrow = 1;
            parent.Add(this);

            context.ThemeProvider.ThemeChanged += OnThemeChanged;

            settingsButton = CreateButton("Settings", OnSettingsButtonClicked);
            openQuestionManagerButton = CreateButton("Question Manager", OnOpenQuestionManagerButtonClicked);
            startRandomButton = CreateButton("Start Random", OnStartRandomButtonClicked);
            quitButton = CreateButton("Quit", OnQuitButtonClicked);

            Hide();
        }

        /// <summary>
        ///     Shows the navigation composite.
        /// </summary>
        public void Show() => style.display = DisplayStyle.Flex;

        /// <summary>
        ///     Hides the navigation composite.
        /// </summary>
        public void Hide() => style.display = DisplayStyle.None;

        /// <inheritdoc />
        public void Dispose()
        {
            context.ThemeProvider.ThemeChanged -= OnThemeChanged;

            settingsButton.OnClick -= OnSettingsButtonClicked;
            openQuestionManagerButton.OnClick -= OnOpenQuestionManagerButtonClicked;
            startRandomButton.OnClick -= OnStartRandomButtonClicked;
            quitButton.OnClick -= OnQuitButtonClicked;

            settingsButton.Dispose();
            openQuestionManagerButton.Dispose();
            startRandomButton.Dispose();
            quitButton.Dispose();
        }

        private UITheme Theme => context.ThemeProvider.CurrentTheme;

        private ButtonControl CreateButton(string text, EventHandler onClick)
        {
            var control = new ButtonControl(Theme.ButtonStyle) { Text = text };
            control.OnClick += onClick;
            Add(control);
            return control;
        }

        private void OnThemeChanged(object? sender, UITheme e)
        {
            settingsButton.SetTheme(e.ButtonStyle);
            openQuestionManagerButton.SetTheme(e.ButtonStyle);
            startRandomButton.SetTheme(e.ButtonStyle);
            quitButton.SetTheme(e.ButtonStyle);
        }

        private void OnSettingsButtonClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnOpenQuestionManagerButtonClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnStartRandomButtonClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnQuitButtonClicked(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

