#nullable enable

using Musicmania.Data;
using Musicmania.Extensions;
using System.Linq;
using UnityEngine;

namespace Musicmania.Disks
{
    public class QuestionDisk : Disk<QuestionData>
    {
        public void CheckAnswer(string answer)
        {
            Data.LastAnswer = answer;

            // Save the last answer persistently
            PlayerPrefs.SetString(Data.Name, answer);

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

        protected override void OnDiskClicked()
        {
            // TODO: Implement the quizManager field in MusicmaniaContext
            Debug.Log("Question selected: " + Data.Name + " - " + Data.Difficulty);
            quizManager?.SelectDisk(questionDisk);
            DiskPlayer.LoadAndPlay(questionDisk.Data.Audio);

            base.OnDiskClicked();
        }
    }
}
