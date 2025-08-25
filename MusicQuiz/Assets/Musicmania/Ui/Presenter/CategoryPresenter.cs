#nullable enable

using Cysharp.Threading.Tasks;
using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.ResourceManagement;
using Musicmania.Utils;
using System;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Musicmania.Ui.Presenter
{
    public class CategoryPresenter : MonoBehaviour
    {
        [SerializeField]
        private Image? thumbnailImage;

        [SerializeField]
        private TMP_Text? categoryNameText;

        private ResourceHandle<Sprite>? thumbnailResourceHandle;
        private CancellableTaskCollection taskCollection = new();
        private bool isInitialized;

        public event EventHandler? CategoryClicked;

        private Image ThumbnailImage => SerializeFieldNotAssignedException.ThrowIfNull(thumbnailImage);

        private TMP_Text CategoryNameText => SerializeFieldNotAssignedException.ThrowIfNull(categoryNameText);

        private ResourceHandle<Sprite> ThumbnailResourceHandle => NotInitializedException.ThrowIfNull(thumbnailResourceHandle);

        public void Initialize(CategoryData categoryData, MusicmaniaContext context)
        {
            if (isInitialized)
            {
                return;
            }

            CategoryNameText.text = categoryData.Name;
            thumbnailResourceHandle = context.ResourceManager.GetResource<Sprite>(categoryData.ThumbnailResourceKey);
            taskCollection.StartExecution(InitializeAsync);
            isInitialized = true;
        }

        public void OnCategoryClicked()
        {
            CategoryClicked?.Invoke(this, EventArgs.Empty);
        }

        private void OnDestroy()
        {
            taskCollection.Dispose();

            ThumbnailImage.sprite = null;

            ThumbnailResourceHandle.Unload();
        }

        private async UniTask InitializeAsync(CancellationToken cancellationToken)
        {
            try
            {
                var thumbnail = await ThumbnailResourceHandle.LoadAsync(cancellationToken);
                ThumbnailImage.sprite = thumbnail;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load category thumbnail: {ex.Message}");
            }
        }
    }
}
