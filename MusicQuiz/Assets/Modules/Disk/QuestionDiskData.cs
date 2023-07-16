#nullable enable

using MusicQuiz.Exceptions;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MusicQuiz.Disk
{
    [CreateAssetMenu(fileName = "QuestionDiskData", menuName = "MusicQuiz/Data/Question Disk Data")]
    public class QuestionDiskData : DiskData
    {
        [SerializeField]
        private AssetReferenceT<AudioClip>? audio;

        public AssetReferenceT<AudioClip> Audio => audio != null ? audio : throw new SerializeFieldNotAssignedException();

        public string LastAnswer { get; set; } = string.Empty;
    }
}
