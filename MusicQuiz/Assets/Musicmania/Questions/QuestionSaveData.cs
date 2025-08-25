#nullable enable

namespace Musicmania.Questions
{
    public class QuestionSaveData
    {
        public string LastAnswer { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public bool IsLocked { get; set; }

        public int TippCount { get; set; }

        public int Position { get; set; }
    }
}
