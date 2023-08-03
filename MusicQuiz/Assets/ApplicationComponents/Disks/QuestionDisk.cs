#nullable enable


using MusicQuiz.Exceptions;
using MusicQuiz.Extensions;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MusicQuiz.Disks
{
    public class QuestionDisk : Disk
    {
        [SerializeField]
        private Image? diskGlow;

        private QuestionDiskData? data;

        /// <summary>
        ///     Gets the data assigned to this disk or null if no data was assigned.
        /// </summary>
        public QuestionDiskData Data => data != null ? data : throw new InvalidOperationException($"'{nameof(data)}' is null, because there is no data assigned to this disk.");

        private Image DiskGlow => diskGlow != null ? diskGlow : throw new SerializeFieldNotAssignedException();

        /// <summary>
        ///     Assigns the given <see cref="QuestionDiskData"/> to this disk by updating the gameobject name and visual appearance.
        /// </summary>
        /// <param name="questionData">The data for this disk.</param>
        public void AssignQuestionData(QuestionDiskData questionData)
        {
            data = questionData;
            name = $"[{Label.text}] {questionData.DiskName}";

            VisualizeDiskState();
        }

        /// <summary>
        ///     Sets the state of this disk based on the given <see cref="DiskState"/>.
        ///     This will also update the visual appearance of the disk.
        /// </summary>
        /// <param name="diskState">The state to set.</param>
        public void SetDiskState(DiskState diskState)
        {
            Data.DiskState = diskState;
            VisualizeDiskState();
        }

        public void CheckAnswer(string answer)
        {
            Data.LastAnswer = answer;

            // Save the last answer persistently
            PlayerPrefs.SetString(Data.DiskName, answer);

            var normalizedAnswer = string.Concat(answer.ToLower().Where(c => !char.IsControl(c)));
            var correctAnswer = Data.DiskName;

            var distance = normalizedAnswer.LevenshteinDistance(correctAnswer);

            Data.DiskState = CalculateDiskStateOnDistance(distance);
            VisualizeDiskState();
        }

        private DiskState CalculateDiskStateOnDistance(int distance) => distance switch
        {
            0 => DiskState.SOLVED,
            <= 3 => DiskState.CLOSE_TO_SOLVED,
            _ => DiskState.WRONG
        };

        private void VisualizeDiskState()
        {
            if (data == null)
            {
                return;
            }

            DiskGlow.enabled = data.DiskState != DiskState.LOCKED && data.DiskState != DiskState.UNLOCKED;

            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile, nameof(colorProfile));

            switch (data.DiskState)
            {
                case DiskState.LOCKED:
                    DiskGlow.color = colorProfile.unknownColor;
                    break;
                case DiskState.UNLOCKED:
                    DiskGlow.color = colorProfile.unknownColor;
                    break;
                case DiskState.CLOSE_TO_SOLVED:
                    DiskGlow.color = colorProfile.closeToSolvedColor;
                    break;
                case DiskState.SOLVED:
                    if (data.Image != null)
                    {
                        BackgroundImage.sprite = data.Image;
                        BackgroundImage.color = Color.gray;
                    }

                    DiskGlow.color = colorProfile.solvedColor;
                    break;
                case DiskState.WRONG:
                    DiskGlow.color = colorProfile.wrongColor;
                    break;
                default:
                    break;
            }
        }
    }
}
