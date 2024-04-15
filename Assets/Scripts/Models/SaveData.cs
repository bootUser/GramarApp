using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class SaveData
    {
        public int tickets;
        public List<Word> usersWords;
        public int visitingSeries;
        public string lastVisitDateString;
        public bool tutorialCompleted;
    }
}
