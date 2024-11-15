#nullable enable

using Musicmania.Data;
using Musicmania.Exceptions;
using System;
using Unity.Android.Gradle.Manifest;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Musicmania.Disks
{
    public sealed class CategoryDisk : Disk<CategoryData>
    {
        protected override void OnDiskClicked()
        {
            Debug.Log("Load Category " + Data.Name + "...");
            Context.ScreenManager.ShowQuizScreen(Data);

            base.OnDiskClicked();
        }
    }
}
