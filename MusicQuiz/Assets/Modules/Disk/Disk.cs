#nullable enable

using MusicQuiz.Exceptions;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MusicQuiz.Disk
{
    public class Disk : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text? label;

        [SerializeField]
        private Image? backgroundImage;

        [SerializeField]
        private Button? diskButton;

        [SerializeField]
        protected ColorProfile? colorProfile;

        protected TMP_Text Label => label != null ? label : throw new SerializeFieldNotAssignedException();

        protected Image BackgroundImage => backgroundImage != null ? backgroundImage : throw new SerializeFieldNotAssignedException();

        public void Initialize(string diskLabel, Action discClickAction)
        {
            SerializeFieldNotAssignedException.ThrowIfNull(diskButton, nameof(diskButton));
            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile, nameof(colorProfile));

            diskButton.onClick.AddListener(() => discClickAction.Invoke());
            BackgroundImage.color = colorProfile.unknownColor;
            Label.text = diskLabel;
        }
    }
}
