#nullable enable

using Musicmania.Settings.Ui;
using System;

namespace Musicmania.Ui.Theming
{
    public class ThemeProvider : IThemeProvider
    {
        public UITheme CurrentTheme { get; }

        public ColorProfile CurrentColorProfile { get; }

        public event EventHandler<UITheme>? ThemeChanged;

        public event EventHandler<ColorProfile>? ColorProfileChanged;

        public ThemeProvider(UITheme theme, ColorProfile colorProfile)
        {
            CurrentTheme = theme;
            CurrentColorProfile = colorProfile;
            CurrentTheme.EnsureDefaults();
        }
    }
}
