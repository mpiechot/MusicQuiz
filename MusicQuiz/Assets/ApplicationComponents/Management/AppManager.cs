#nullable enable

using MusicQuiz.Exceptions;
using UnityEngine;

namespace MusicQuiz.Management
{
    public class AppManager : MonoBehaviour
    {
        [SerializeField]
        private AppData? appData;

        private ScreenManager? screenManager;

        void Awake()
        {
            SerializeFieldNotAssignedException.ThrowIfNull(appData, nameof(appData));

            screenManager = new ScreenManager(appData);

            screenManager.ShowCategoryScreen();
        }
    }
}
