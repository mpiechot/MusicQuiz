using Musicmania.Exceptions;
using UnityEngine;

namespace Musicmania.Ui.Screens
{
    public abstract class ScreenBase : MonoBehaviour
    {
        private MusicmaniaContext? context;

        protected MusicmaniaContext Context => NotInitializedException.ThrowIfNull(context, nameof(context));

        /// <summary>
        ///     Initializes the QuizScreen.
        /// </summary>
        /// <param name="contextToUse">The appData to use.</param>
        public virtual void Initialize(MusicmaniaContext contextToUse)
        {
            context = contextToUse;
        }

        /// <summary>
        ///     Shows the screen.
        /// </summary>
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        ///     Hides the screen.
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
