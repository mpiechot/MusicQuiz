#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace Musicmania.Exceptions
{
    /// <summary>
    ///     Exception thrown when a member is accessed before its owning object's <c>Show</c> method was called.
    /// </summary>
    [Serializable]
    public class ShowNotCalledException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ShowNotCalledException"/> class.
        /// </summary>
        public ShowNotCalledException([CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "") :
            base($"The member '{callerMemberName}' of '{Path.GetFileName(filePath)}' was accessed before its owner was shown.")
        {
        }

        /// <summary>
        ///     Checks if the object is null and throws an exception if it is.
        /// </summary>
        /// <typeparam name="T">The type of the object to check.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="filePath">The filepath of the object's class.</param>
        /// <param name="callerMemberName">The caller name of the object to check.</param>
        /// <returns>The non-null object if the given object is not null.</returns>
        /// <exception cref="ShowNotCalledException">Thrown when the given object is null.</exception>
        public static T ThrowIfNull<T>([NotNull] T? obj, [CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "")
            where T : class
        {
            if (obj == null)
            {
                throw new ShowNotCalledException(filePath, callerMemberName);
            }

            return obj;
        }
    }
}
