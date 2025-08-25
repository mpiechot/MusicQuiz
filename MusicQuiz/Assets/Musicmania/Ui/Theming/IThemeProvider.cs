#nullable enable

using Musicmania.Settings.Ui;
using System;

namespace Musicmania.Ui.Theming
{

    public interface IThemeProvider
    {
        UITheme CurrentTheme { get; }

        ColorProfile CurrentColorProfile { get; }

        event EventHandler<UITheme>? ThemeChanged;

        event EventHandler<ColorProfile>? ColorProfileChanged;
    }
}
