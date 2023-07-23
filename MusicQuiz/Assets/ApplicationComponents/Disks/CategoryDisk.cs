#nullable enable

using System;

namespace MusicQuiz.Disks
{
    public class CategoryDisk : Disk
    {
        private CategoryDiskData? data;

        /// <summary>
        ///     Gets the data assigned to this disk or null if no data was assigned.
        /// </summary>
        public CategoryDiskData Data => data != null ? data : throw new InvalidOperationException($"'{nameof(data)}' is null, because there is no data assigned to this disk.");

        public void AssignCategoryData(CategoryDiskData categoryData)
        {
            data = categoryData;

            BackgroundImage.sprite = categoryData.Image;
        }
    }
}
