#nullable enable

using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using UnityEngine;

namespace Musicmania.Disks
{
    public sealed class CategoryDisk : Disk
    {
        private CategoryData? data;

        public CategoryData Data => NoDataAssignedException.ThrowIfNull(data);

        /// <inheritdoc/>
        public override void OnDiskClicked()
        {
            Debug.Log("Load Category " + Data.Name + " ...");
            //Context.ScreenManager.ShowQuizScreen(Data);

            base.OnDiskClicked();
        }

        /// <summary>
        ///     Assigns the given data to this disk.
        /// </summary>
        /// <param name="dataToAssign">The data to assign.</param>
        public void AssignData(CategoryData dataToAssign)
        {
            data = dataToAssign;
            name = $"[{Label.text}] {dataToAssign.Name}";

            //BackgroundImage.sprite = dataToAssign.Thumbnail;
        }
    }
}
