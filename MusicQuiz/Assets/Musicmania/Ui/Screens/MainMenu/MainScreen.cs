#nullable enable

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.Ui.Presenter;
using Musicmania.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace Musicmania.Ui.Screens.MainMenu
{
    /// <summary>
    ///     Main menu screen implemented using UI Toolkit.
    /// </summary>
    public class MainScreen : ScreenBase
    {
        [SerializeField]
        private UIDocument? document;

        private MainScreenNavigationComposite? navigation;
        private CategoryListPresenter? categoryListPresenter;
        private readonly CancellableTaskCollection taskCollection = new();

        private UIDocument Document => SerializeFieldNotAssignedException.ThrowIfNull(document);

        private MainScreenNavigationComposite Navigation => NotInitializedException.ThrowIfNull(navigation);

        /// <summary>
        ///     Initializes the main screen with the provided context.
        /// </summary>
        /// <param name="contextToUse">The application context.</param>
        public override void Initialize(MusicmaniaContext contextToUse)
        {
            base.Initialize(contextToUse);

            var root = Document.rootVisualElement;
            var navigationRoot = root.Q("mainScreenNavigation") ?? throw new InvalidOperationException("mainScreenNavigation element not found");
            navigation = new MainScreenNavigationComposite(contextToUse, navigationRoot);

            taskCollection.StartExecution(InitializeCategoriesAsync);
        }

        /// <summary>
        ///     Shows the main menu screen.
        /// </summary>
        public override void Show()
        {
            base.Show();
            Navigation.Show();
        }

        /// <summary>
        ///     Hides the main menu screen.
        /// </summary>
        public override void Hide()
        {
            Navigation.Hide();
            base.Hide();
        }

        /// <summary>
        ///     Cleans up resources on destruction.
        /// </summary>
        private void OnDestroy()
        {
            navigation?.Dispose();
            categoryListPresenter?.Dispose();
            taskCollection.Dispose();
        }

        /// <summary>
        ///     Loads categories and creates the corresponding UI elements.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        private async UniTask InitializeCategoriesAsync(CancellationToken cancellationToken)
        {
            var categoriesResource = Context.ResourceManager.GetResource<CategoriesCollectionData>(Context.Settings.ResourceSettings.CategoriesFileLocation);
            var categories = await categoriesResource.LoadAsync(cancellationToken);
            var root = Document.rootVisualElement;
            var listRoot = root.Q("categoryList") ?? root;
            categoryListPresenter = new CategoryListPresenter(Context, listRoot, categories);
        }
    }
}
