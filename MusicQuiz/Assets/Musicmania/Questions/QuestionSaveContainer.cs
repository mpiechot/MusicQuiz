#nullable enable

using Musicmania.Data.Categories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Musicmania.Questions
{
    public class QuestionSaveContainer
    {
        //[JsonProperty("questionSaves")]
        //public List<QuizSave> QuestionSaves { get; set; } = new();

        //public QuizSave GetQuestionSaveViaTags(CategoryTag tags)
        //{
        //    foreach (var questionSave in QuestionSaves)
        //    {
        //        if (questionSave.CategoryTags == tags)
        //        {
        //            return questionSave;
        //        }
        //    }

        //    var newQuestionSave = new QuizSave
        //    {
        //        CategoryTags = tags
        //    };

        //    QuestionSaves.Add(newQuestionSave);
        //    return newQuestionSave;
        //}
    }
}
