#nullable enable

using Musicmania.Data;
using Musicmania.Disks;
using Musicmania.Exceptions;
using Musicmania.Settings;
using System.Collections.Generic;
using UnityEngine;

namespace Musicmania.Screens
{
    public class CategoryScreen : MonoBehaviour
    {
        [SerializeField]
        private RectTransform? categoryParent;

        private CategoryDisk? diskPrefab;

        private MusicmaniaContext? context;

        private MusicmaniaContext Context => NotInitializedException.ThrowIfNull(context, nameof(context));

        public void Initialize(MusicmaniaContext contextToUse)
        {
            context = contextToUse;
            diskPrefab = context.Settings.PrefabSettings.CategoryDiskPrefab;
        }

        public void Show(IReadOnlyCollection<CategoryData> categories)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));
            NotInitializedException.ThrowIfNull(categories, nameof(categories));

            gameObject.SetActive(true);

            foreach (var category in categories)
            {
                var disk = Instantiate(diskPrefab, categoryParent);

                disk.Initialize(category.Name, Context);
                disk.AssignData(category);
                Debug.Log("Created Category Disk for: " + category.Name);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}