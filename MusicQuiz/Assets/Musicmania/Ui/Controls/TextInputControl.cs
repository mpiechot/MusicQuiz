#nullable enable

using System;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Controls
{
    /// <summary>
    ///     UI Toolkit text input control that exposes an event when the text changes.
    /// </summary>
    public sealed class TextInputControl : TextField, IDisposable
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="TextInputControl"/> class.
        /// </summary>
        public TextInputControl()
        {
            RegisterCallback<ChangeEvent<string>>(OnValueChanged);
        }

        /// <summary>
        ///     Raised when the text input changes.
        /// </summary>
        public event EventHandler<string>? OnTextChanged;

        /// <summary>
        ///     Gets or sets the current text.
        /// </summary>
        public string Text
        {
            get => base.value;
            set => SetValueWithoutNotify(value);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            UnregisterCallback<ChangeEvent<string>>(OnValueChanged);
            GC.SuppressFinalize(this);
        }

        private void OnValueChanged(ChangeEvent<string> evt)
        {
            OnTextChanged?.Invoke(this, evt.newValue);
        }
    }
}
