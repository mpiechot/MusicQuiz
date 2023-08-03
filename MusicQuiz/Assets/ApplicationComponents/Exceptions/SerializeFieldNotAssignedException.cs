#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace MusicQuiz.Exceptions
{
    /// <summary>
    ///     Exception for when a serialized field needs to be assigned in the inspector but wasn't.
    /// </summary>
    public class SerializeFieldNotAssignedException : Exception
    {
        /// <summary>
        /// Constructor for a SerializeFieldNotAssignedException instance.
        /// </summary>
        /// <param name="callerMemberName">The name of the caller member that threw the exception.</param>
        public SerializeFieldNotAssignedException([CallerMemberName] string callerMemberName = "") :
            base($"The serialize field '{callerMemberName}' was not assigned in the inspector.")
        {
        }


        /// <summary>
        ///     Throws a SerializeFieldNotAssignedException if the objectToTest is null.
        /// </summary>
        /// <param name="objectToTest">The object to test for null.</param>
        /// <param name="objectName">The name of the serialized field.</param>
        public static void ThrowIfNull([NotNull] object? objectToTest, string objectName)
        {
            if (objectToTest == null)
            {
                throw new($"SerializeField '{objectName}' was not assigned in the inspector.");
            }
        }
    }
}
