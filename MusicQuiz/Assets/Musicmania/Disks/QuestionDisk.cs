#nullable enable

using Musicmania.Exceptions;
using Musicmania.Extensions;
using Musicmania.Questions;
using System.Linq;
using UnityEngine;

namespace Musicmania.Disks
{
    public class QuestionDisk : Disk
    {
        private QuestionData? data;

        public QuestionData Data => NoDataAssignedException.ThrowIfNull(data);

        public void CheckAnswer(string answer)
        {
            //Data.LastAnswer = answer;

            var normalizedAnswer = string.Concat(answer.ToLower().Where(c => !char.IsControl(c)));
            var correctAnswer = Data.Name;

            var distance = normalizedAnswer.LevenshteinDistance(correctAnswer);

            DiskState = CalculateDiskStateOnDistance(distance);
            VisualizeDiskState();
        }

        private DiskState CalculateDiskStateOnDistance(int distance) => distance switch
        {
            0 => DiskState.Solved,
            <= 3 => DiskState.CloseToSolved,
            _ => DiskState.Wrong
        };

        /// <inheritdoc/>
        public override void OnDiskClicked()
        {
            // TODO: Implement the quizManager field in MusicmaniaContext
            Debug.Log("Question selected: " + Data.Name + " - " + Data.Difficulty);
            //Context.AudioPlayer.LoadAndPlay(Data.Audio);

            base.OnDiskClicked();
        }

        /// <summary>
        ///     Assigns the given data to this disk.
        /// </summary>
        /// <param name="dataToAssign">The data to assign.</param>
        public void AssignData(QuestionData dataToAssign)
        {
            data = dataToAssign;
            name = $"[{Label.text}] {dataToAssign.Name}";

            //BackgroundImage.sprite = dataToAssign.Image;
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
                    //if (data.Image != null)
                    //{
                    //    BackgroundImage.sprite = data.Image;
                    //    //BackgroundImage.color = Color.gray;
                    //}

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
