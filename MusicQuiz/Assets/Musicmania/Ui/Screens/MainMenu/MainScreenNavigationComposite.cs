#nullable enable

using Musicmania.Exceptions;
using Musicmania.Settings.Ui;
using Musicmania.Ui.Controls;
using System;
using UnityEngine;

namespace Musicmania.Ui.Screens.MainMenu
{
    public class MainScreenNavigationComposite : ScreenBase
    {
        [SerializeField]
        private ButtonControl? settingsButton;

        [SerializeField]
        private ButtonControl? openQuestionManagerButton;

        [SerializeField]
        private ButtonControl? startRandom;

        [SerializeField]
        private ButtonControl? quitButton;

        private ButtonControl SettingsButton => SerializeFieldNotAssignedException.ThrowIfNull(settingsButton);

        private ButtonControl OpenQuestionManagerButton => SerializeFieldNotAssignedException.ThrowIfNull(openQuestionManagerButton);

        private ButtonControl StartRandom => SerializeFieldNotAssignedException.ThrowIfNull(startRandom);

        private ButtonControl QuitButton => SerializeFieldNotAssignedException.ThrowIfNull(quitButton);

        private UITheme Theme => Context.ThemeProvider.CurrentTheme;


        public override void Initialize(MusicmaniaContext contextToUse)
        {
            base.Initialize(contextToUse);

            Context.ThemeProvider.ThemeChanged += OnThemeChanged;

            SettingsButton.Initialize(Theme.ButtonStyle);
            OpenQuestionManagerButton.Initialize(Theme.ButtonStyle);
            StartRandom.Initialize(Theme.ButtonStyle);
            QuitButton.Initialize(Theme.ButtonStyle);

            SettingsButton.OnClick += OnSettingsButtonClicked;
            OpenQuestionManagerButton.OnClick += OnOpenQuestionManagerButtonClicked;
            StartRandom.OnClick += OnStartRandomButtonClicked;
            QuitButton.OnClick += OnQuitButtonClicked;
        }

        private void OnOpenQuestionManagerButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnStartRandomButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnQuitButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OnThemeChanged(object sender, UITheme e)
        {
            SettingsButton.SetTheme(Theme.ButtonStyle);
            OpenQuestionManagerButton.SetTheme(Theme.ButtonStyle);
            StartRandom.SetTheme(Theme.ButtonStyle);
            QuitButton.SetTheme(Theme.ButtonStyle);
        }

        /// <summary>
        ///     Shows the main menu screen.
        /// </summary>
        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        ///     Hides the main menu screen.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
        }
    }
}
