#nullable enable

using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Musicmania;
using Musicmania.Data.Categories;
using Musicmania.ResourceManagement;
using Musicmania.Settings.Ui;
using Musicmania.Ui.Controls;
using Musicmania.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Presenter
{
    /// <summary>
    ///     Presents a category including its thumbnail using a themed <see cref="ButtonControl"/>.
    /// </summary>
    public sealed class CategoryPresenter : VisualElement, IDisposable
    {
        private readonly ButtonControl button;
        private readonly Image thumbnailImage;
        private readonly CancellableTaskCollection taskCollection = new();
        private readonly ResourceHandle<Sprite> thumbnailResourceHandle;
        private readonly MusicmaniaContext context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CategoryPresenter"/> class.
        /// </summary>
        /// <param name="category">The category to display.</param>
        /// <param name="buttonStyle">The style to apply to the button.</param>
        /// <param name="contextToUse">The application context.</param>
        /// <exception cref="ArgumentNullException">Thrown when a required argument is null.</exception>
        public CategoryPresenter(CategoryData category, ButtonStyle buttonStyle, MusicmaniaContext contextToUse)
        {
            Category = category ?? throw new ArgumentNullException(nameof(category));
            context = contextToUse ?? throw new ArgumentNullException(nameof(contextToUse));

            button = new ButtonControl(buttonStyle ?? throw new ArgumentNullException(nameof(buttonStyle)))
            {
                Text = category.Name,
            };
            button.OnClick += OnButtonClicked;

            thumbnailImage = new Image();

            Add(thumbnailImage);
            Add(button);

            thumbnailResourceHandle = context.ResourceManager.GetResource<Sprite>(category.ThumbnailResourceKey);
            taskCollection.StartExecution(LoadThumbnailAsync);
        }

        /// <summary>
        ///     Gets the category represented by this presenter.
        /// </summary>
        public CategoryData Category { get; }

        /// <summary>
        ///     Applies a new theme to the underlying button.
        /// </summary>
        /// <param name="style">The style to apply.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="style"/> is null.</exception>
        public void SetTheme(ButtonStyle style)
        {
            button.SetTheme(style ?? throw new ArgumentNullException(nameof(style)));
        }

        /// <summary>
        ///     Releases resources used by the presenter.
        /// </summary>
        public void Dispose()
        {
            taskCollection.Dispose();

            thumbnailImage.sprite = null;
            thumbnailResourceHandle.Unload();

            button.OnClick -= OnButtonClicked;
            button.Dispose();
        }

        private async UniTask LoadThumbnailAsync(CancellationToken cancellationToken)
        {
            try
            {
                var sprite = await thumbnailResourceHandle.LoadAsync(cancellationToken);
                thumbnailImage.sprite = sprite;
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load category thumbnail: {ex.Message}");
            }
        }

        private void OnButtonClicked(object? sender, EventArgs e)
        {
            context.ScreenManager.ShowQuizScreen(Category);
        }
    }
}
