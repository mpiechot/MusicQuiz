#nullable enable

using MusicQuiz.Disks;
using MusicQuiz.Exceptions;
using MusicQuiz.Management;
using System.Collections.Generic;
using UnityEngine;

namespace MusicQuiz.Screens
{
    public class CategoryScreen : MonoBehaviour
    {
        [SerializeField]
        private RectTransform? categoryParent;

        private CategoryDisk? diskPrefab;

        private IEnumerable<CategoryDiskData>? categories;

        private ScreenManager? screenManager;

        public void Initialize(AppData appData, ScreenManager screenManager)
        {
            this.screenManager = screenManager;
            diskPrefab = appData.CategoryDiskPrefab;
            categories = appData.QuizCategories;
        }

        public void Show()
        {
            SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));
            NotInitializedException.ThrowIfNull(categories, nameof(categories));

            gameObject.SetActive(true);

            foreach (var category in categories)
            {
                var disk = Instantiate(diskPrefab, categoryParent);

                disk.Initialize(category.DiskName, () => SelectCategory(category));
                disk.AssignCategoryData(category);
                Debug.Log("Created Category Disk for: " + category.DiskName);
            }
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void SelectCategory(CategoryDiskData category)
        {
            Debug.Log("Load Category " + category.DiskName + "...");
            screenManager?.ShowQuizScreen(category);
        }
    }
}