#nullable enable

using Newtonsoft.Json;
using System.Collections.Generic;

namespace Musicmania.Questions
{
    /// <summary>
    ///     Represents a DTO for question data.
    /// </summary>
    public class QuestionData
    {
        /// <summary>
        ///     Gets or sets the name of the data.
        /// </summary>
        public string Name { get; private set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the image resource key.
        ///     This is used to load the resource via the <see cref="ResourceManagement.ResourceManager"/>
        /// </summary>
        [JsonProperty("imageResourceKey")]
        public string ImageResourceKey { get; } = string.Empty;

        /// <summary>
        ///     Gets or sets a value indicating whether the user is asked for the exakt name or just the overall series name.
        /// </summary>
        [JsonProperty("exact")]
        public bool AskForExactName { get; }

        /// <summary>
        ///     Gets or sets the audio resource key.
        ///     This is used to load the resource via the <see cref="ResourceManagement.ResourceManager"/>
        /// </summary>
        [JsonProperty("audioResourceKey")]
        public string AudioResourceKey { get; } = string.Empty;

        /// <summary>
        ///     Gets the list of available tipps for the question.
        /// </summary>
        [JsonProperty("tipps")]
        public List<string> Tipps { get; } = new();

        /// <summary>
        ///     Gets or sets the difficulty level of the question.
        /// </summary>
        [JsonProperty("difficulty")]
        public QuestionDifficulty Difficulty { get; }

        /// <summary>
        ///     Gets or sets the list of savestates for this question.
        /// </summary>
        [JsonProperty("questionSaves")]
        public List<QuestionSaveData> QuestionSaves { get; } = new();
    }
}
