#nullable enable

using Assets.Scripts.Extensions;
using MusicQuiz.Disk;
using MusicQuiz.Modules.Disk;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.QuizManagement
{
    public class QuizManager
    {
        private readonly DiskPlayer diskPlayer;

        private QuestionDisk? currentDisk;

        public QuizManager(DiskPlayer diskPlayer)
        {
            this.diskPlayer = diskPlayer;
        }

        public void SelectDisk(QuestionDisk disk)
        {
            if (currentDisk != null && currentDisk.Data == disk.Data)
            {
                diskPlayer.Restart();

                return;
            }

            currentDisk = disk;
            diskPlayer.LoadAndPlay(currentDisk.Data.Audio);
        }

        public void CheckAnswer(string answer)
        {
            if (currentDisk == null)
            {
                return;
            }

            currentDisk.Data.LastAnswer = answer;
            PlayerPrefs.SetString(currentDisk.Data.DiskName, answer);

            var normalizedAnswer = string.Concat(answer.ToLower().Where(c => !char.IsControl(c)));
            var correctAnswer = currentDisk.Data.DiskName;

            var distance = normalizedAnswer.LevenshteinDistance(correctAnswer);

            currentDisk.SetDiskState(CalculateDiskStateOnDistance(distance));
        }

        private DiskState CalculateDiskStateOnDistance(int distance) => distance switch
        {
            0 => DiskState.SOLVED,
            <= 3 => DiskState.CLOSE_TO_SOLVED,
            _ => DiskState.WRONG
        };
    }
}
