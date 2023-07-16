using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Question
    {
        public int id;
        public string sourcePath;
        public string lastAnswer;
        public bool onlySeries;
        public string[] tipps;
        public int actualTipLevel;
        public bool unlocked;
    }
}
