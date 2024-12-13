using Musicmania.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="diskPlayer">The diskPlayer to use.</param>
        public virtual void Initialize(MusicmaniaContext contextToUse)
        {
            context = contextToUse;
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
