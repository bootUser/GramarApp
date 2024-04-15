using System;
using System.Collections.Generic;
using Models;

namespace WordsTests
{
    public class CustomTest:WordsTest
    {
        public CustomTest(List<Word> providedWords, UsersDataManager usersData) : base(providedWords, usersData, providedWords.Count)
        {
        }

        protected override IEnumerable<Word> ChooseTestWords(List<Word> providedWords)
        {
            return providedWords;
        }
    }
}