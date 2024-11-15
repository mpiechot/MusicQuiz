#nullable enable

using Musicmania.Exceptions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Musicmania.Data
{
    /// <summary>
    ///     Scriptable object containing data for a single question disk. Inherits from the abstract DiskData class.
    /// </summary>
    [CreateAssetMenu(fileName = "QuestionDiskData", menuName = "MusicQuiz/Question Disk Data")]
    public class QuestionData : BaseData
    {
        [SerializeField]
        private AssetReferenceT<AudioClip>? audio;

        /// <summary>
        ///     Gets or sets the difficulty level of the question.
        /// </summary>
        [field: SerializeField]
        public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Easy;

        [field: SerializeField]
        public CategoryTag QuestionTags { get; set; } = CategoryTag.None;

        public CategoryTag CurrentCategoryTags { get; set; }

        public bool IsLocked
        {
            get => QuestionSave.QuestionSaves[CurrentCategoryTags].Locked;
            set => QuestionSave.QuestionSaves[CurrentCategoryTags].Locked = value;
        }

        public int Position
        {
            get => QuestionSave.QuestionSaves[CurrentCategoryTags].Position;
            set => QuestionSave.QuestionSaves[CurrentCategoryTags].Position = value;
        }

        public int TippCount
        {
            get => QuestionSave.QuestionSaves[CurrentCategoryTags].TippCount;
            set => QuestionSave.QuestionSaves[CurrentCategoryTags].TippCount = value;
        }

        public string LastAnswer
        {
            get => QuestionSave.QuestionSaves[CurrentCategoryTags].LastAnswer;
            set => QuestionSave.QuestionSaves[CurrentCategoryTags].LastAnswer = value;
        }

        /// <summary>
        ///     Gets the audio clip that plays when the question is selected.
        /// </summary>
        public AssetReferenceT<AudioClip> Audio => audio != null ? audio : throw new SerializeFieldNotAssignedException();

        private Quizes? questionSave;

        public Quizes QuestionSave => NotInitializedException.ThrowIfNull(questionSave, nameof(questionSave));

        public void Initialize(Quizes? quiz, int questionPosition)
        {
            if (quiz != null)
            {
                questionSave = quiz;
                return;
            }

            questionSave = new();
            questionSave.QuestionSaves.Add(CurrentCategoryTags, new() { CategoryTags = CurrentCategoryTags, Position = questionPosition });
        }
    }

    [Serializable]
    public class Quizes
    {
        public Dictionary<CategoryTag, QuestionSave> QuestionSaves { get; set; } = new();
    }

    [Serializable]
    public class QuestionSave
    {
        public CategoryTag CategoryTags { get; set; }

        public int Position { get; set; } = 0;

        /// <summary>
        ///     Gets or sets the last answer given by the player.
        /// </summary>
        public string LastAnswer { get; set; } = string.Empty;

        public int TippCount { get; set; }

        public bool Locked { get; set; }
    }
}
