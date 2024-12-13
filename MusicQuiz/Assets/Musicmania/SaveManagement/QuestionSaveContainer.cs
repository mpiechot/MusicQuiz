#nullable enable

using Musicmania.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Musicmania.SaveManagement
{
    public class QuestionSaveContainer
    {
        [JsonProperty("questionSaves")]
        public List<QuestionSave> QuestionSaves { get; set; } = new();

        public QuestionSave GetQuestionSaveViaTags(CategoryTag tags)
        {
            foreach (var questionSave in QuestionSaves)
            {
                if (questionSave.CategoryTags == tags)
                {
                    return questionSave;
                }
            }

            var newQuestionSave = new QuestionSave
            {
                CategoryTags = tags
            };

            QuestionSaves.Add(newQuestionSave);
            return newQuestionSave;
        }
    }
}
