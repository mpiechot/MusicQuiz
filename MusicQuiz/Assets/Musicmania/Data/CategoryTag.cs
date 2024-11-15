#nullable enable

using System;

namespace Musicmania.Data
{
    [Flags]
    public enum CategoryTag
    {
        None = 0,
        Games = 1,
        Movies = 2,
        Disney = 4,
        Zelda = 8,
        Serien = 16,
        Animes = 32,
    }
}
