#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace Musicmania.Exceptions
{
    [Serializable]
    public class ResourceLoadException : Exception
    {
        public ResourceLoadException([CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "") :
               base($"Could not load the resource '{callerMemberName}' in '{Path.GetFileName(filePath)}'.")
        {
        }

        /// <summary>
        ///     Checks if the object is null and throws an exception if it is.
        /// </summary>
        /// <typeparam name="T">The type of the object to check.</typeparam>
        /// <param name="obj">The object to check.</param>
        /// <param name="filePath">The filepath of the objects class.</param>
        /// <param name="callerMemberName">The caller name of the object to check.</param>
        /// <returns>The non-nullable object if the given object is not null.</returns>
        /// <exception cref="ResourceLoadException">Throws the exception if the given object is null.</exception>
        public static T ThrowIfNull<T>([NotNull] T? obj, [CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "")
            where T : class
        {
            if (obj == null)
            {
                throw new ResourceLoadException(filePath, callerMemberName);
            }

            return obj;
        }
    }
}