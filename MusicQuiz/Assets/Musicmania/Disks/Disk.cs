#nullable enable

using Musicmania.Data.Categories;
using Musicmania.Exceptions;
using Musicmania.Settings.Ui;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Musicmania.Disks
{
    /// <summary>
    ///     Represents a visual disk which holds data of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of data the disk holds (e.g. <see cref="Question"/> or <see cref="CategoryData"/>).</typeparam>
    public abstract class Disk : MonoBehaviour
    {
        [SerializeField]
        private RectTransform? diskContainer;

        [SerializeField]
        private TMP_Text? label;

        [SerializeField]
        private Image? backgroundImage;

        [SerializeField]
        private Image? backgroundMask;

        [SerializeField]
        private Button? diskButton;

        [SerializeField]
        protected ColorProfile? colorProfile;

        [SerializeField]
        private Image? diskGlow;

        protected DiskState diskState;

        protected Image DiskGlow => SerializeFieldNotAssignedException.ThrowIfNull(diskGlow);

        protected TMP_Text Label => SerializeFieldNotAssignedException.ThrowIfNull(label);

        protected Image BackgroundImage => SerializeFieldNotAssignedException.ThrowIfNull(backgroundImage);

        private Image BackgroundMask => SerializeFieldNotAssignedException.ThrowIfNull(backgroundImage);

        private RectTransform DiskContainer => SerializeFieldNotAssignedException.ThrowIfNull(diskContainer);

        protected ColorProfile ColorProfile => SerializeFieldNotAssignedException.ThrowIfNull(colorProfile);

        private MusicmaniaContext? context;

        protected MusicmaniaContext Context => NotInitializedException.ThrowIfNull(context);

        protected DiskState DiskState
        {
            get => diskState;
            set
            {
                diskState = value;
                EvaluateDiskState();
            }
        }

        private void EvaluateDiskState()
        {
            switch (diskState)
            {
                case DiskState.Locked:
                case DiskState.Normal:
                    DiskGlow.gameObject.SetActive(false);
                    BackgroundImage.gameObject.SetActive(false);
                    //BackgroundMask.material.color = ColorProfile.defaultColor;
                    break;
                case DiskState.CloseToSolved:
                    DiskGlow.gameObject.SetActive(true);
                    DiskGlow.color = ColorProfile.closeToSolvedColor;
                    //BackgroundMask.material.color = ColorProfile.defaultColor;
                    BackgroundImage.gameObject.SetActive(false);
                    break;
                case DiskState.Solved:
                    DiskGlow.gameObject.SetActive(true);
                    DiskGlow.color = ColorProfile.solvedColor;
                    BackgroundMask.material.color = Color.white;
                    BackgroundImage.gameObject.SetActive(true);
                    break;
                case DiskState.Wrong:
                    DiskGlow.gameObject.SetActive(true);
                    DiskGlow.color = ColorProfile.wrongColor;
                    //BackgroundMask.material.color = ColorProfile.defaultColor;
                    BackgroundImage.gameObject.SetActive(false);
                    break;
            }
        }

        public event EventHandler? DiskClicked;

        public void Initialize(string diskLabel, MusicmaniaContext contextToUse)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile, nameof(colorProfile));
            SerializeFieldNotAssignedException.ThrowIfNull(diskButton, nameof(diskButton));

            context = contextToUse;
            //BackgroundImage.material.color = colorProfile.unknownColor;
            DiskState = DiskState.Normal;
            Label.text = diskLabel;
        }

        /// <summary>
        ///    Called by Unity when the disk is clicked.
        /// </summary>
        public virtual void OnDiskClicked()
        {
            DiskClicked?.Invoke(this, EventArgs.Empty);
        }

        public void SetDiskYOffset(int y)
        {
            DiskContainer.localPosition = new(0, y, 0);
        }
    }
}
