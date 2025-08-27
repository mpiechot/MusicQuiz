#nullable enable

using System;
using Musicmania;
using Musicmania.Settings.Ui;
using Musicmania.Ui.Controls;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Screens
{
    /// <summary>
    ///     Navigation composite for the quiz screen.
    /// </summary>
    public sealed class QuizScreenNavigationComposite : VisualElement, IDisposable
    {
        private readonly MusicmaniaContext context;
        private readonly ButtonControl settingsButton;
        private readonly ButtonControl exitButton;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuizScreenNavigationComposite"/> class.
        /// </summary>
        public QuizScreenNavigationComposite(MusicmaniaContext contextToUse, VisualElement parent)
        {
            context = contextToUse ?? throw new ArgumentNullException(nameof(contextToUse));
            _ = parent ?? throw new ArgumentNullException(nameof(parent));

            style.flexGrow = 1;
            parent.Add(this);

            context.ThemeProvider.ThemeChanged += OnThemeChanged;

            settingsButton = CreateButton("Settings", OnSettingsClicked);
            exitButton = CreateButton("Exit", OnExitClicked);

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

            settingsButton.OnClick -= OnSettingsClicked;
            exitButton.OnClick -= OnExitClicked;

            settingsButton.Dispose();
            exitButton.Dispose();
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
            exitButton.SetTheme(e.ButtonStyle);
        }

        private void OnSettingsClicked(object? sender, EventArgs e)
        {
            // Settings screen not implemented yet.
        }

        private void OnExitClicked(object? sender, EventArgs e)
        {
            context.ScreenManager.ShowMainScreen();
        }
    }
}
