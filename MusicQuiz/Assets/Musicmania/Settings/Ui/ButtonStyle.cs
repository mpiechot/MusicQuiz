#nullable enable

using TMPro;
using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "ButtonStyle", menuName = "Musicmania/UI/Button Style", order = 0)]
    public class ButtonStyle : ScriptableObject
    {
        [Header("Text Settings")]
        [field: SerializeField]
        public TMP_FontAsset Font { get; set; } = null!;

        [field: SerializeField]
        public int FontSize { get; set; } = 36;

        [field: SerializeField]
        public Color TextColor { get; set; } = Color.white;

        [Header("Background Settings")]
        [field: SerializeField]
        public Color NormalColor { get; set; } = new Color(0.2f, 0.2f, 0.2f);

        [field: SerializeField]
        public Color HighlightedColor { get; set; } = new Color(0.3f, 0.3f, 0.3f);

        [field: SerializeField]
        public Color PressedColor { get; set; } = new Color(0.15f, 0.15f, 0.15f);

        [field: SerializeField]
        public Sprite BackgroundSprite { get; set; } = null!;

    }
}
