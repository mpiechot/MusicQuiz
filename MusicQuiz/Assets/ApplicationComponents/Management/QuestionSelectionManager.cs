#nullable enable

using MusicQuiz.Disks;

namespace MusicQuiz.Management
{
    /// <summary>
    ///     Represents a manager for the quiz, which handles the selection of disks and provides access to the current selected disk.
    /// </summary>
    public class QuestionSelectionManager
    {
        private readonly DiskPlayer diskPlayer;

        private QuestionDisk? currentDisk;

        /// <summary>
        ///    Gets the currently selected <see cref="QuestionDisk"/> or null if no disk is selected.
        /// </summary>
        public QuestionDisk? CurrentDisk => currentDisk;

        /// <summary>
        ///     Initializes a new instance of the <see cref="QuestionSelectionManager"/> class.
        /// </summary>
        /// <param name="diskPlayer">The disk player to use.</param>
        public QuestionSelectionManager(DiskPlayer diskPlayer)
        {
            this.diskPlayer = diskPlayer;
        }

        /// <summary>
        ///    Selects the given <see cref="QuestionDisk"/> and plays the audio of the disk.
        /// </summary>
        /// <param name="disk">The disk to select and play.</param>
        public void SelectDisk(QuestionDisk disk)
        {


            currentDisk = disk;
        }

        /// <summary>
        ///     Checks the given answer against the current disk.
        /// </summary>
        /// <param name="answer"></param>
        public void CheckAnswer(string answer)
        {
            if (currentDisk == null)
            {
                return;
            }

            currentDisk.CheckAnswer(answer);
        }
    }
}
