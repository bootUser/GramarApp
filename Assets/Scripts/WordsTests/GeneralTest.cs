using System.Collections.Generic;
using System.Runtime.InteropServices;
using Extensions;
using Models;
using UnityEngine.UIElements;

namespace WordsTests
{
    public class GeneralTest:WordsTest
    {
        public GeneralTest(int wordsNumber, List<Word> providedWords, UsersDataManager dataManager) : base(providedWords, dataManager, wordsNumber)
        {
        }

        protected override IEnumerable<Word> ChooseTestWords(List<Word> providedWords)
        {
            for (int i = 0; i < length; i++)
            {
                var rndWord = providedWords.Random();
                providedWords.Remove(rndWord);
                yield return rndWord;
            }
        }
        
    }
}