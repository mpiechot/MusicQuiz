#nullable enable

using System;
using System.Collections.Generic;
using Musicmania.Data.Categories;
using Musicmania.Settings.Ui;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Presenter
{
    /// <summary>
    ///     Presents a list of available categories using UI Toolkit.
    /// </summary>
    public sealed class CategoryListPresenter : VisualElement, IDisposable
    {
        private readonly MusicmaniaContext context;

        private readonly List<CategoryPresenter> categoryPresenters = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="CategoryListPresenter"/> class.
        /// </summary>
        /// <param name="contextToUse">The application context.</param>
        /// <param name="parent">The parent element to attach to.</param>
        /// <param name="categories">The categories to present.</param>
        /// <exception cref="ArgumentNullException">Thrown when any argument is null.</exception>
        public CategoryListPresenter(MusicmaniaContext contextToUse, VisualElement parent, CategoriesCollectionData categories)
        {
            context = contextToUse ?? throw new ArgumentNullException(nameof(contextToUse));
            _ = parent ?? throw new ArgumentNullException(nameof(parent));
            _ = categories ?? throw new ArgumentNullException(nameof(categories));

            style.flexDirection = FlexDirection.Column;
            parent.Add(this);

            context.ThemeProvider.ThemeChanged += OnThemeChanged;

            foreach (var category in categories.Categories)
            {
                var presenter = new CategoryPresenter(category, Theme.ButtonStyle, context);
                Add(presenter);
                categoryPresenters.Add(presenter);
            }
        }

        /// <inheritdoc />
        public void Dispose()
        {
            context.ThemeProvider.ThemeChanged -= OnThemeChanged;

            foreach (var presenter in categoryPresenters)
            {
                presenter.Dispose();
            }
        }

        private UITheme Theme => context.ThemeProvider.CurrentTheme;

        private void OnThemeChanged(object? sender, UITheme e)
        {
            foreach (var presenter in categoryPresenters)
            {
                presenter.SetTheme(e.ButtonStyle);
            }
        }
    }
}

