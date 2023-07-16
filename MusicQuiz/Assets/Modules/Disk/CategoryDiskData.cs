#nullable enable

using System.Collections.Generic;
using UnityEngine;

namespace MusicQuiz.Disk
{
    [CreateAssetMenu(fileName = "CategoryDiskData", menuName = "MusicQuiz/Data/Category Disk Data")]
    public class CategoryDiskData : DiskData
    {
        [field: SerializeField]
        public List<QuestionDiskData> Questions { get; set; } = new();
    }
}
