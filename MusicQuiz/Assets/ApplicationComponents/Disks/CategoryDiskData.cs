#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace MusicQuiz.Disks
{
    [CreateAssetMenu(fileName = "CategoryDiskData", menuName = "MusicQuiz/Category Disk Data")]
    public class CategoryDiskData : DiskData
    {
        [field: SerializeField]
        public List<QuestionDiskData> Questions { get; set; } = new();
    }
}
