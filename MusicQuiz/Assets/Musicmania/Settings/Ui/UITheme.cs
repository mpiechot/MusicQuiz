#nullable enable

using Musicmania.Exceptions;
using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "UITheme", menuName = "Musicmania/UI/Theme")]
    public sealed class UITheme : ScriptableObject
    {
        [SerializeField]
        private ButtonStyle? buttonStyle;

        public ButtonStyle ButtonStyle => SerializeFieldNotAssignedException.ThrowIfNull(buttonStyle);

        private void OnEnable() => EnsureDefaults();
#if UNITY_EDITOR
        private void OnValidate() => EnsureDefaults();
#endif

        public void EnsureDefaults()
        {
            if (buttonStyle == null)
            {
                buttonStyle = ScriptableObject.CreateInstance<ButtonStyle>();
                ButtonStyle.name = "RuntimeDefaultButtonStyle";
                ButtonStyle.FontSize = 24;
                ButtonStyle.TextColor = Color.white;
                ButtonStyle.NormalColor = new Color(0.15f, 0.15f, 0.15f, 1f);
                ButtonStyle.HighlightedColor = new Color(0.20f, 0.20f, 0.20f, 1f);
                ButtonStyle.PressedColor = new Color(0.10f, 0.10f, 0.10f, 1f);
                // Font / BackgroundSprite kannst du im Editor setzen; optional hier mit Fallback belegen
            }
        }
    }
}
