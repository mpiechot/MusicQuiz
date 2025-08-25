#nullable enable

using Musicmania.Exceptions;
using Musicmania.Settings.Ui;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Musicmania.Ui.Controls
{
    [RequireComponent(typeof(Button))]
    public class ButtonControl : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text? label;

        [SerializeField]
        private Image? background;

        public event EventHandler? OnClick;

        private Button? button;

        private ButtonStyle? style;

        private TMP_Text Label => SerializeFieldNotAssignedException.ThrowIfNull(label);

        private Image Background => SerializeFieldNotAssignedException.ThrowIfNull(background);

        private ButtonStyle Style => NotInitializedException.ThrowIfNull(style);

        private Button Button => NotInitializedException.ThrowIfNull(button);

        public string Text
        {
            get => Label.text;
            set => Label.text = value;
        }

        public void Initialize(ButtonStyle initialStyle)
        {
            if (initialStyle == null)
            {
                throw new ArgumentNullException(nameof(initialStyle), "ButtonStyle cannot be null.");
            }

            style = initialStyle;
            button = GetComponent<Button>();

            UpdateTheming();

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() => OnClick?.Invoke(this, EventArgs.Empty));
        }

        public void SetTheme(ButtonStyle buttonStyle)
        {
            style = buttonStyle ?? throw new ArgumentNullException(nameof(buttonStyle), "ButtonStyle cannot be null.");

            UpdateTheming();
        }

        private void UpdateTheming()
        {

            Label.font = Style.Font;
            Label.fontSize = Style.FontSize;
            Label.color = Style.TextColor;

            Background.sprite = Style.BackgroundSprite;
            Background.color = Style.NormalColor;

            var colors = Button.colors;
            colors.normalColor = Style.NormalColor;
            colors.highlightedColor = Style.HighlightedColor;
            colors.pressedColor = Style.PressedColor;
            Button.colors = colors;
        }
    }
}
