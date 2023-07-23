#nullable enable

using MusicQuiz.Exceptions;
using UnityEngine;

namespace MusicQuiz.Disks
{
    public abstract class DiskData : ScriptableObject
    {
        [SerializeField]
        private string? diskName = string.Empty;

        [SerializeField]
        private Sprite? image;

        public DiskState DiskState { get; set; } = DiskState.LOCKED;

        public string DiskName => !string.IsNullOrEmpty(diskName) ? diskName : throw new SerializeFieldNotAssignedException();
        public Sprite? Image => image;
    }
}
