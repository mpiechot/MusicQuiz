#nullable enable

using Musicmania.Exceptions;
using Musicmania.Ui.Presenter;
using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "DataPrefabCollection", menuName = "Musicmania/Data Prefab Collection")]
    public class DataPrefabCollection : ScriptableObject
    {
        [SerializeField]
        private CategoryPresenter? categoryPresenterPrefab;

        public CategoryPresenter CategoryPresenterPrefab => SerializeFieldNotAssignedException.ThrowIfNull(categoryPresenterPrefab);
    }
}
