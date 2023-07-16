#nullable enable

using MusicQuiz.Exceptions;
using MusicQuiz.Modules.Disk;
using UnityEngine;

namespace MusicQuiz.Disk
{
    public abstract class DiskData : ScriptableObject
    {
        [SerializeField]
        private string? diskName = string.Empty;

        [SerializeField]
        private Sprite? image;

        [field: SerializeField]
        public DiskState DiskState { get; set; } = DiskState.LOCKED;

        public string DiskName => !string.IsNullOrEmpty(diskName) ? diskName : throw new SerializeFieldNotAssignedException();
        public Sprite? Image => image;
    }
}
