#nullable enable

using MusicQuiz.Disk;
using MusicQuiz.Exceptions;
using System;
using System.Collections.Generic;
using UnityEngine;

public class CategoryScreen : MonoBehaviour
{
    [SerializeField]
    private CategoryDisk? diskPrefab;

    [SerializeField]
    private List<CategoryDiskData> categories = new();

    [SerializeField]
    private RectTransform? categoryParent;

    public event EventHandler<CategoryDiskData>? CategorySelectedEvent;

    public void Show()
    {
        SerializeFieldNotAssignedException.ThrowIfNull(diskPrefab, nameof(diskPrefab));
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
        CategorySelectedEvent?.Invoke(this, category);
    }
}
