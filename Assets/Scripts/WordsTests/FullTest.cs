using System.Collections.Generic;
using Models;

namespace WordsTests
{
    public class FullTest:WordsTest
    {
        public FullTest(List<Word> providedWords, UsersDataManager usersData) : base(providedWords, usersData, providedWords.Count)
        {
        }

        protected override IEnumerable<Word> ChooseTestWords(List<Word> providedWords)
        {
            return providedWords;
        }
    }
}