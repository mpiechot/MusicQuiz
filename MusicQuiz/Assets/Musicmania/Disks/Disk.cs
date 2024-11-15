#nullable enable

using Musicmania.Data;
using Musicmania.Exceptions;
using Musicmania.Util;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Musicmania.Disks
{
    /// <summary>
    ///     Represents a visual disk which holds data of type <see cref="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of data the disk holds (e.g. <see cref="QuestionData"/> or <see cref="CategoryData"/>).</typeparam>
    public class Disk<T> : MonoBehaviour
        where T : BaseData
    {
        [SerializeField]
        private TMP_Text? label;

        [SerializeField]
        private Image? backgroundImage;

        [SerializeField]
        private Button? diskButton;

        [SerializeField]
        protected ColorProfile? colorProfile;

        [SerializeField]
        private Image? diskGlow;

        private DiskState diskState;

        private Image DiskGlow => SerializeFieldNotAssignedException.ThrowIfNull(diskGlow);

        protected TMP_Text Label => SerializeFieldNotAssignedException.ThrowIfNull(label);

        protected Image BackgroundImage => SerializeFieldNotAssignedException.ThrowIfNull(backgroundImage);

        private MusicmaniaContext? context;

        protected MusicmaniaContext Context => NotInitializedException.ThrowIfNull(context);

        protected DiskState DiskState
        {
            get => diskState;
            set => diskState = value;
        }

        public event EventHandler? DiskClicked;

        public void Initialize(string diskLabel, MusicmaniaContext contextToUse)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile, nameof(colorProfile));
            SerializeFieldNotAssignedException.ThrowIfNull(diskButton, nameof(diskButton));

            context = contextToUse;
            BackgroundImage.color = colorProfile.unknownColor;
            Label.text = diskLabel;
            diskButton.onClick.AddListener(OnDiskClicked);
        }

        protected virtual void OnDiskClicked()
        {
            DiskClicked?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        ///     Sets the state of this disk based on the given <see cref="DiskState"/>.
        ///     This will also update the visual appearance of the disk.
        /// </summary>
        /// <param name="diskState">The state to set.</param>
        public void SetDiskState(DiskState diskStateToSet)
        {
            // TODO Handle DiskState
            diskState = diskStateToSet;


            VisualizeDiskState();
        }

        private T? data;

        /// <summary>
        ///     Gets the data assigned to this disk.
        /// </summary>
        public T Data => data != null ? data : throw new InvalidOperationException($"'{nameof(data)}' is null, because there is no data assigned to this disk.");

        /// <summary>
        ///     Assigns the given data to this disk.
        /// </summary>
        /// <param name="dataToAssign">The data to assign.</param>
        public void AssignData(T dataToAssign)
        {
            data = dataToAssign;
            name = $"[{Label.text}] {dataToAssign.Name}";

            BackgroundImage.sprite = dataToAssign.Image;
        }

        protected void VisualizeDiskState()
        {
            if (data == null)
            {
                return;
            }

            DiskGlow.enabled = diskState != DiskState.Locked && diskState != DiskState.Normal;

            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile, nameof(colorProfile));

            switch (diskState)
            {
                case DiskState.Locked:
                    DiskGlow.color = colorProfile.unknownColor;
                    break;
                case DiskState.Normal:
                    DiskGlow.color = colorProfile.unknownColor;
                    break;
                case DiskState.CloseToSolved:
                    DiskGlow.color = colorProfile.closeToSolvedColor;
                    break;
                case DiskState.Solved:
                    if (data.Image != null)
                    {
                        BackgroundImage.sprite = data.Image;
                        BackgroundImage.color = Color.gray;
                    }

                    DiskGlow.color = colorProfile.solvedColor;
                    break;
                case DiskState.Wrong:
                    DiskGlow.color = colorProfile.wrongColor;
                    break;
                default:
                    break;
            }
        }
    }
}
