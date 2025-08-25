#nullable enable

using Musicmania.Data.Categories;
using Musicmania.Disks;
using Musicmania.Exceptions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Musicmania.Ui.Screens
{
    [Obsolete("Use MainScreen instead. This screen is deprecated and will be removed in a future version.")]
    public class CategoryScreen : ScreenBase
    {
        [SerializeField]
        private RectTransform? categoryParent;

        private CategoryDisk? diskPrefab;

        private List<CategoryDisk> availableCategories = new();

        public override void Initialize(MusicmaniaContext contextToUse)
        {
            base.Initialize(contextToUse);
            //diskPrefab = Context.Settings.ScreenPrefabProvider.CategoryDiskPrefab;
        }

        /// <inheritdoc />
        public override void Show()
        {
            CreateCategoryDisks();

            base.Show();
        }

        private void CreateCategoryDisks()
        {
            if (availableCategories.Count > 0)
            {
                // Already created the disks
                return;
            }

            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

            //var categories = Context.CategoryStorage.Categories;

            //foreach (var category in categories)
            //{
            //    var disk = Instantiate(diskPrefab, categoryParent);

            //    disk.Initialize(category.Name, Context);
            //    disk.AssignData(category);
            //    availableCategories.Add(disk);
            //    Debug.Log("Created Category Disk for: " + category.Name);
            //}
        }
    }
}