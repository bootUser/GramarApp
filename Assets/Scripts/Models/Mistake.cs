using System;

namespace Models
{
    [Serializable]
    public class Mistake:ICloneable<Mistake>
    {
        public int index;
        [NonSerialized]
        public MistakeType type;
        public string strType;
        
        public Mistake Clone()
        {
            return new Mistake() { index = index, strType = strType, type = type};
        }
    }
    public enum MistakeType
    {
        WrongLetter,
        DoubleLetter,
        SplitWriting,
        HyphenWriting
    }
}