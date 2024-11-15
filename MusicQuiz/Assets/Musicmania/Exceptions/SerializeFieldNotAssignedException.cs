#nullable enable

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;

namespace Musicmania.Exceptions
{
    /// <summary>
    /// Represents errors that occur when a serialized field required by Unity is not assigned.
    /// This exception is specifically designed to be thrown when a serialized field expected
    /// to be set in the Unity Inspector is found to be null at runtime, aiding in debugging.
    /// </summary>
    [Serializable]
    public class SerializeFieldNotAssignedException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SerializeFieldNotAssignedException"/> class.
        /// </summary>
        /// <param name="filePath">The path of the file where the unassigned field was found. This parameter is automatically populated by the compiler.</param>
        /// <param name="callerMemberName">The name of the member (method, property, etc.) where the unassigned field was detected. This parameter is automatically populated by the compiler.</param>
        public SerializeFieldNotAssignedException([CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "") :
            base($"The object '{callerMemberName}' of '{Path.GetFileName(filePath)}' was not assigned via the inspector.")
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
        /// <exception cref="SerializeFieldNotAssignedException">Throws the exception if the given object is null.</exception>
        public static T ThrowIfNull<T>([NotNull] T? obj, [CallerFilePath] string filePath = "", [CallerMemberName] string callerMemberName = "")
            where T : class
        {
            if (obj == null)
            {
                throw new SerializeFieldNotAssignedException(filePath, callerMemberName);
            }

            return obj;
        }
    }
}
