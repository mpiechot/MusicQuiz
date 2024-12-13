#nullable enable

using Musicmania.Data;
using Musicmania.Extensions;
using System;
using System.Linq;
using UnityEngine;

namespace Musicmania.Disks
{
    public class QuestionDisk : Disk<QuestionData>
    {
        public void CheckAnswer(string answer)
        {
            Data.LastAnswer = answer;

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
            Context.AudioPlayer.LoadAndPlay(Data.Audio);

            base.OnDiskClicked();
        }
    }
}
