#nullable enable

using Cysharp.Threading.Tasks;
using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.Extensions;
using Musicmania.Utils;
using System.Threading;
using UnityEngine;

namespace Musicmania.Ui.Presenter
{
    public class CategoryListPresenter : MonoBehaviour
    {
        [SerializeField]
        private Transform? listElementsParent;

        private Transform ListElementsParentTransform => SerializeFieldNotAssignedException.ThrowIfNull(listElementsParent);

        private MusicmaniaContext? context;

        private CategoriesCollectionData? categoriesCollectionData;

        private CancellableTaskCollection taskCollection = new();

        private CategoryPresenter? categoryPresenterPrefab;

        private MusicmaniaContext Context => NotInitializedException.ThrowIfNull(context);

        private CategoriesCollectionData CategoriesCollectionData => NotInitializedException.ThrowIfNull(categoriesCollectionData);

        private CategoryPresenter CategoryPresenterPrefab => NotInitializedException.ThrowIfNull(categoryPresenterPrefab);

        public void Initialize(MusicmaniaContext contextInstance)
        {
            context = contextInstance;
            categoryPresenterPrefab = Context.Settings.DataPrefabProvider.CategoryPresenterPrefab;

            taskCollection.CancelExecution();
            taskCollection.StartExecution(UpdateAvailableCategoriesAsync);
        }
        public async UniTask UpdateAvailableCategoriesAsync(CancellationToken cancellationToken)
        {
            ListElementsParentTransform.DestroyAllChildren();

            var categoriesResource = Context.ResourceManager.GetResource<CategoriesCollectionData>(Context.Settings.ResourceSettings.CategoriesFileLocation);

            categoriesCollectionData = await categoriesResource.LoadAsync(cancellationToken);

            foreach (var category in CategoriesCollectionData.Categories)
            {
                var categoryElement = GameObject.Instantiate<CategoryPresenter>(CategoryPresenterPrefab, ListElementsParentTransform);
                categoryElement.Initialize(category, Context);
            }
        }
    }
}
