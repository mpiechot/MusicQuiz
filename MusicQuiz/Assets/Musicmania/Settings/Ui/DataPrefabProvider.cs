#nullable enable

using Musicmania.Exceptions;
using Musicmania.Ui.Presenter;
using UnityEngine;

namespace Musicmania.Settings.Ui
{
    [CreateAssetMenu(fileName = "DataPrefabProvider", menuName = "Musicmania/Data Prefab Provider")]
    public class DataPrefabProvider : ScriptableObject
    {
        [SerializeField]
        private CategoryPresenter? categoryPresenterPrefab;

        public CategoryPresenter CategoryPresenterPrefab => categoryPresenterPrefab != null ? categoryPresenterPrefab : throw new SerializeFieldNotAssignedException();
    }
}