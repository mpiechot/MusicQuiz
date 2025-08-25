#nullable enable

using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "UITheme", menuName = "Musicmania/UI/Theme")]
    public sealed class UITheme : ScriptableObject
    {
        [field: SerializeField]
        public ButtonStyle ButtonStyle { get; private set; } = null!;

        private void OnEnable() => EnsureDefaults();
#if UNITY_EDITOR
        private void OnValidate() => EnsureDefaults();
#endif

        public void EnsureDefaults()
        {
            if (ButtonStyle == null)
            {
                ButtonStyle = ScriptableObject.CreateInstance<ButtonStyle>();
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
