#nullable enable

using Musicmania.Exceptions;
using Musicmania.Ui.Presenter;
using UnityEngine;

namespace Musicmania.Ui.Screens.MainMenu
{
    public class MainScreen : ScreenBase
    {
        [SerializeField]
        private MainScreenNavigationComposite? navigation;

        [SerializeField]
        private CategoryListPresenter? categoryListPresenter;

        private MainScreenNavigationComposite Navigation => NotInitializedException.ThrowIfNull(navigation);

        private CategoryListPresenter CategoryListPresenter => NotInitializedException.ThrowIfNull(categoryListPresenter);

        public override void Initialize(MusicmaniaContext contextToUse)
        {
            base.Initialize(contextToUse);

            Navigation.Initialize(contextToUse);
            CategoryListPresenter.Initialize(contextToUse);
        }

        /// <summary>
        ///     Shows the main menu screen.
        /// </summary>
        public override void Show()
        {
            base.Show();
        }

        /// <summary>
        ///     Hides the main menu screen.
        /// </summary>
        public override void Hide()
        {
            base.Hide();
        }
    }
}
