#nullable enable


using MusicQuiz.Exceptions;
using System;

namespace MusicQuiz.Disks
{
    public class QuestionDisk : Disk
    {
        private QuestionDiskData? data;

        /// <summary>
        ///     Gets the data assigned to this disk or null if no data was assigned.
        /// </summary>
        public QuestionDiskData Data => data != null ? data : throw new InvalidOperationException($"'{nameof(data)}' is null, because there is no data assigned to this disk.");

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

        private void VisualizeDiskState()
        {
            if (data == null)
            {
                return;
            }

            SerializeFieldNotAssignedException.ThrowIfNull(colorProfile, nameof(colorProfile));

            switch (data.DiskState)
            {
                case DiskState.LOCKED:
                    BackgroundImage.color = colorProfile.unknownColor;
                    break;
                case DiskState.UNLOCKED:
                    BackgroundImage.color = colorProfile.unknownColor;
                    break;
                case DiskState.CLOSE_TO_SOLVED:
                    BackgroundImage.color = colorProfile.closeToSolvedColor;
                    break;
                case DiskState.SOLVED:
                    if (data.Image != null)
                    {
                        BackgroundImage.sprite = data.Image;
                    }

                    BackgroundImage.color = colorProfile.solvedColor;
                    break;
                case DiskState.WRONG:
                    BackgroundImage.color = colorProfile.wrongColor;
                    break;
                default:
                    break;
            }
        }
    }
}
