#nullable enable

using Musicmania.Data;
using Musicmania.Disks;
using Musicmania.Exceptions;
using System.Collections.Generic;
using UnityEngine;

namespace Musicmania.Ui.Screens
{
    public class CategoryScreen : ScreenBase
    {
        [SerializeField]
        private RectTransform? categoryParent;

        private CategoryDisk? diskPrefab;

        private List<CategoryDisk> availableCategories = new();

        public override void Initialize(MusicmaniaContext contextToUse)
        {
            base.Initialize(contextToUse);
            diskPrefab = Context.Settings.PrefabSettings.CategoryDiskPrefab;
        }

        public override void Show()
        {
            CreateCategoryDisks();

            gameObject.SetActive(true);
        }

        private void CreateCategoryDisks()
        {
            if (availableCategories.Count > 0)
            {
                // Already created the disks
                return;
            }

            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));

            var categories = Context.CategoryStorage.Categories;

            foreach (var category in categories)
            {
                var disk = Instantiate(diskPrefab, categoryParent);

                disk.Initialize(category.Name, Context);
                disk.AssignData(category);
                availableCategories.Add(disk);
                Debug.Log("Created Category Disk for: " + category.Name);
            }
        }
    }
}