#nullable enable

using Musicmania.Data;
using Newtonsoft.Json;
using System;

namespace Musicmania.SaveManagement
{
    public class QuestionSave
    {
        [JsonProperty("categoryTags")]
        public CategoryTag CategoryTags { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; } = -1;

        /// <summary>
        ///     Gets or sets the last answer given by the player.
        /// </summary>
        [JsonProperty("lastAnswer")]
        public string LastAnswer { get; set; } = string.Empty;

        [JsonProperty("tippcount")]
        public int TippCount { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }
    }
}
