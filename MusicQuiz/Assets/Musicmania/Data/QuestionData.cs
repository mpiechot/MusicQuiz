#nullable enable

using Musicmania.Exceptions;
using Musicmania.SaveManagement;
using Newtonsoft.Json;
using System;
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

        [field: SerializeField]
        public bool AskForExactName { get; private set; } = false;

        /// <summary>
        ///     Gets or sets the difficulty level of the question.
        /// </summary>
        [field: SerializeField]
        public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Easy;

        public CategoryTag CurrentCategoryTags { get; private set; }

        public bool IsLocked
        {
            get => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).Locked;
            set => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).Locked = value;
        }

        public int Position
        {
            get => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).Position;
            set => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).Position = value;
        }

        public int TippCount
        {
            get => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).TippCount;
            set => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).TippCount = value;
        }

        public string LastAnswer
        {
            get => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).LastAnswer;
            set => QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags).LastAnswer = value;
        }

        /// <summary>
        ///     Gets the audio clip that plays when the question is selected.
        /// </summary>
        public AssetReferenceT<AudioClip> Audio => audio != null ? audio : throw new SerializeFieldNotAssignedException();

        private QuestionSaveContainer? questionSavesContainer;

        public QuestionSaveContainer QuestionSaves => NotInitializedException.ThrowIfNull(questionSavesContainer, nameof(questionSavesContainer));

        private QuestionSave? currentQuestionSave;

        private MusicmaniaContext? context;

        public void Initialize(MusicmaniaContext contextInsance)
        {
            context = contextInsance;

            questionSavesContainer = context.QuestionSaveContainerManager.GetQuestionSaveContainer(Name);
        }

        public void SetCurrentCategoryTags(CategoryTag categoryTags)
        {
            CurrentCategoryTags = categoryTags;
            currentQuestionSave = QuestionSaves.GetQuestionSaveViaTags(CurrentCategoryTags);
            currentQuestionSave.CategoryTags = CurrentCategoryTags;
        }
    }
}
