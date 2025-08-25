#nullable enable

using System;

namespace Musicmania.Questions
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