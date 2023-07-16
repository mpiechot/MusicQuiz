#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MusicQuiz.Exceptions
{
    public class SerializeFieldNotAssignedException : Exception
    {
        public SerializeFieldNotAssignedException([CallerMemberName] string callerMemberName = "") :
            base($"The serialize field '{callerMemberName}' was not assigned in the inspector.")
        {
        }



        public static void ThrowIfNull([NotNull] object? objectToTest, string objectName)
        {
            if (objectToTest == null)
            {
                throw new($"SerializeField {objectName} was not assigned in the inspector.");
            }
        }
    }
}
