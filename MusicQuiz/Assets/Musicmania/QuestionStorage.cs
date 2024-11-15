#nullable enable

using Musicmania.Data;
using System.Collections.Generic;

namespace Musicmania
{
    /// <summary>
    ///    Class that stores all questions and provides methods to filter them by tags.
    /// </summary>
    public class QuestionStorage
    {
        private readonly List<QuestionData> allQuestions = new();

        /// <summary>
        ///   Initializes a new instance of a <see cref="QuestionStorage"/>.
        /// </summary>
        /// <param name="questions">The questions to store.</param>
        public QuestionStorage(IReadOnlyCollection<QuestionData> questions)
        {
            allQuestions.AddRange(questions);
        }

        /// <summary>
        ///    Gets all questions with the specified tags.
        /// </summary>
        /// <param name="tags">The tags to filter the questions by.</param>
        /// <returns>All questions with the specified tags or null if no questions were found.</returns>
        public IReadOnlyList<QuestionData>? GetAllQuestionsWithTags(CategoryTag tags)
        {
            var questions = allQuestions.FindAll(question => question.QuestionTags == tags);

            return questions.Count > 0 ? questions : null;
        }
    }
}