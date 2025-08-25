#nullable enable

using System;
using System.IO;
using UnityEngine;

namespace Musicmania.Utils
{
    /// <summary>
    /// Utility class for loading JSON data from files.
    /// </summary>
    public static class JsonDataLoader
    {
        /// <summary>
        /// Loads a JSON file from the specified path and deserializes it into an object of type T.
        /// </summary>
        /// <typeparam name="T">The type to deserialize the JSON into.</typeparam>
        /// <param name="path">The file path to the JSON file.</param>
        /// <returns>The deserialized object of type T, or null if the file does not exist or an error occurs.</returns>
        public static T? LoadFromJsonFile<T>(string path)
        {
            if (!File.Exists(path))
            {
                Debug.LogError($"JSON file not found at path: {path}");
                return default;
            }

            try
            {
                var json = File.ReadAllText(path);
                return JsonUtility.FromJson<T>(json);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load or parse JSON file at {path}: {ex.Message}");
                return default;
            }
        }
    }
}
