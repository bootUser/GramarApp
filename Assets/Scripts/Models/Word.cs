using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Word:ICloneable<Word>
    {
        public string name;
        public List<Mistake> mistakes;
        public List<string> tags;
        public int countOfTrainings;
        public Word Clone()
        {
            var cloneMistakes = new List<Mistake>();
            foreach (var m in mistakes)
                cloneMistakes.Add(((ICloneable<Mistake>)m).Clone());
            return new Word() { name = name, tags = tags, countOfTrainings = countOfTrainings, mistakes = cloneMistakes};
        }
    }
}
