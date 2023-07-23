#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MusicQuiz.Exceptions
{
    [Serializable]
    public class NotInitializedException : Exception
    {
        public NotInitializedException([CallerMemberName] string callerMemberName = "") :
               base($"The variable '{callerMemberName}' was not yet initialized by calling a 'Initialize' function.")
        {
        }

        public static void ThrowIfNull([NotNull] object? objectToTest, string objectName)
        {
            if (objectToTest == null)
            {
                throw new($"The variable '{objectName}' was not yet initialized by calling a 'Initialize' function.");
            }
        }
    }
}