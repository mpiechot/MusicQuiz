//#nullable enable

//using Musicmania.Categories;
//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using UnityEngine;

//namespace Musicmania.Questions
//{
//    /// <summary>
//    ///    Class that stores all questions and provides methods to filter them by tags.
//    /// </summary>
//    public class QuestionStorage
//    {
//        private readonly List<QuestionData> allQuestions = new();

//        /// <summary>
//        ///   Initializes a new instance of a <see cref="QuestionStorage"/>.
//        /// </summary>
//        /// <param name="context">The musicmania app context.</param>
//        public QuestionStorage(MusicmaniaContext context)
//        {
//            // Deserialize questions
//            var saveLocation = Path.Combine(Application.persistentDataPath, context.Settings.ResourceSettings.SaveDataLocation);

//            if (!Directory.Exists(saveLocation))
//            {
//                throw new InvalidOperationException($"The question location '{saveLocation}' does not exist. Therefore no questions could be found.");
//            }

//            // Get all file paths in the save location
//            foreach (var path in Directory.GetFiles(saveLocation))
//            {
//                Debug.Log($"Found save file: {path}");
//                var json = File.ReadAllText(path);
//                var question = JsonConvert.DeserializeObject<QuestionData>(json);

//                if (question == null)
//                {
//                    Debug.LogWarning($"Could not deserialize question found at '{path}'");
//                    continue;
//                }

//                question.Initialize(Path.GetFileNameWithoutExtension(path), context);
//                allQuestions.Add(question);
//            }
//        }

//        /// <summary>
//        ///    Gets all questions with the specified tags.
//        /// </summary>
//        /// <param name="tags">The tags to filter the questions by.</param>
//        /// <returns>All questions with the specified tags or null if no questions were found.</returns>
//        public IReadOnlyList<QuestionData> GetAllQuestionsWithTags(CategoryTag tags)
//        {
//            var questions = allQuestions.FindAll(question => question.Tags == tags);

//            foreach (var question in questions)
//            {
//                question.SetCurrentCategoryTags(tags);
//            }

//            return questions;
//        }

//        public void SaveQuestions(IReadOnlyList<QuestionData> unorderedQuestions)
//        {

//        }
//    }
//}