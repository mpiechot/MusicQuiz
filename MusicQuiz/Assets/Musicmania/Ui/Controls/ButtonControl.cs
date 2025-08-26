#nullable enable

using System;
using Musicmania.Settings.Ui;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Controls
{
    /// <summary>
    ///     UI Toolkit button that applies theming from a <see cref="ButtonStyle"/>.
    /// </summary>
    public sealed class ButtonControl : Button, IDisposable
    {
        private ButtonStyle buttonStyle;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ButtonControl"/> class.
        /// </summary>
        /// <param name="initialStyle">The initial style to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required argument is null.</exception>
        public ButtonControl(ButtonStyle initialStyle)
        {
            buttonStyle = initialStyle ?? throw new ArgumentNullException(nameof(initialStyle));

            ApplyStyle();
            RegisterCallbacks();
        }

        /// <summary>
        ///     Raised when the button is clicked.
        /// </summary>
        public event EventHandler? OnClick;

        /// <summary>
        ///     Gets or sets the button text.
        /// </summary>
        public string Text
        {
            get => text;
            set => text = value;
        }

        /// <summary>
        ///     Applies a new style to the button.
        /// </summary>
        /// <param name="buttonStyle">The style to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="buttonStyle"/> is null.</exception>
        public void SetTheme(ButtonStyle style)
        {
            buttonStyle = style ?? throw new ArgumentNullException(nameof(style));
            ApplyStyle();
        }

        /// <summary>
        ///     Releases resources used by the <see cref="ButtonControl"/>.
        /// </summary>
        public void Dispose()
        {
            UnregisterCallbacks();
            GC.SuppressFinalize(this);
        }

        private void RegisterCallbacks()
        {
            clicked += OnButtonClicked;
            RegisterCallback<PointerEnterEvent>(OnPointerEnter);
            RegisterCallback<PointerLeaveEvent>(OnPointerLeave);
            RegisterCallback<PointerDownEvent>(OnPointerDown);
            RegisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void UnregisterCallbacks()
        {
            clicked -= OnButtonClicked;
            UnregisterCallback<PointerEnterEvent>(OnPointerEnter);
            UnregisterCallback<PointerLeaveEvent>(OnPointerLeave);
            UnregisterCallback<PointerDownEvent>(OnPointerDown);
            UnregisterCallback<PointerUpEvent>(OnPointerUp);
        }

        private void OnButtonClicked()
        {
            OnClick?.Invoke(this, EventArgs.Empty);
        }

        private void OnPointerEnter(PointerEnterEvent _)
        {
            style.unityBackgroundImageTintColor = buttonStyle.HighlightedColor;
        }

        private void OnPointerLeave(PointerLeaveEvent _)
        {
            style.unityBackgroundImageTintColor = buttonStyle.NormalColor;
        }

        private void OnPointerDown(PointerDownEvent _)
        {
            style.unityBackgroundImageTintColor = buttonStyle.PressedColor;
        }

        private void OnPointerUp(PointerUpEvent _)
        {
            style.unityBackgroundImageTintColor = buttonStyle.HighlightedColor;
        }

        private void ApplyStyle()
        {
            style.fontSize = buttonStyle.FontSize;
            style.color = buttonStyle.TextColor;

            if (buttonStyle.BackgroundSprite != null)
            {
                style.backgroundImage = new StyleBackground(buttonStyle.BackgroundSprite);
            }

            style.unityBackgroundImageTintColor = buttonStyle.NormalColor;
        }
    }
}
