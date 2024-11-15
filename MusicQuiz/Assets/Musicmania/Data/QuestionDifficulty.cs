#nullable enable

using System;

namespace Musicmania.Data
{
    /// <summary>
    ///     Determines the difficulty of a question.
    /// </summary>
    [Serializable]
    public enum QuestionDifficulty
    {
        VeryEasy,
        Easy,
        Medium,
        Hard,
        VeryHard
    }
}