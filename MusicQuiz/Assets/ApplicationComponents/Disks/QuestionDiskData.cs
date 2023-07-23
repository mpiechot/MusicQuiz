#nullable enable

using MusicQuiz.Exceptions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MusicQuiz.Disks
{
    [CreateAssetMenu(fileName = "QuestionDiskData", menuName = "MusicQuiz/Question Disk Data")]
    public class QuestionDiskData : DiskData
    {
        [SerializeField]
        private AssetReferenceT<AudioClip>? audio;

        [field: SerializeField]
        public QuestionDifficulty Difficulty { get; set; } = QuestionDifficulty.Easy;

        public AssetReferenceT<AudioClip> Audio => audio != null ? audio : throw new SerializeFieldNotAssignedException();

        public string LastAnswer { get; set; } = string.Empty;
    }
}
