#nullable enable

using MusicQuiz.Exceptions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MusicQuiz.Disks
{
    /// <summary>
    ///     Scriptable object containing data for a single question disk. Inherits from the abstract DiskData class.
    /// </summary>
    [CreateAssetMenu(fileName = "QuestionDiskData", menuName = "MusicQuiz/Question Disk Data")]
    public class QuestionDiskData : DiskData
    {
        [SerializeField]
        private AssetReferenceT<AudioClip>? audio;

        /// <summary>
        ///     Gets or sets the difficulty level of the question.
        /// </summary>
        [field: SerializeField]
        public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Easy;

        /// <summary>
        ///     Gets the audio clip that plays when the question is selected.
        /// </summary>
        public AssetReferenceT<AudioClip> Audio => audio != null ? audio : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///     Gets or sets the last answer given by the player.
        /// </summary>
        public string LastAnswer { get; set; } = string.Empty;
    }
}
