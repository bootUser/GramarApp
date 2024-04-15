using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;
using Models;

namespace WordsTests
{
    public abstract class WordsTest
    {
        private const char SpaceSurrogateSymbol = '/';
        
        protected int length;
        protected int rightAnswers;
        
        protected List<Word> _testWords = new List<Word>();
        protected UsersDataManager _usersData;
        
        private Stack<Word> _testStack;
        public Word currentWord;
        private List<Word> _providedWords;
        public WordsTest(List<Word> providedWords, UsersDataManager usersData, int length)
        {
            this.length = length;
            _usersData = usersData;
            _providedWords = providedWords;
        }

        protected abstract IEnumerable<Word> ChooseTestWords(List<Word> providedWords);
        private void PrepareTestStack()
        {
            var chosenWords = ChooseTestWords(_providedWords).ToList();
            MixTestWords(chosenWords, 200);
            _testStack = new Stack<Word>(chosenWords);
        }
        protected virtual void MixTestWords(List<Word> listToMix, int steps)
        {
            for(int k = 0; k< steps;k++)
                for (int i = 0; i < length; i++)
                {
                    var rnd = new Random();
                    var temp = listToMix[i];
                    var rndIndex = rnd.Next(0, length);
                    listToMix[i] = listToMix[rndIndex];
                    listToMix[rndIndex] = temp;
                }
        }
        private void ModifyMistakesInWord(int mistakesCount, Word word)
        {
            if (mistakesCount > word.mistakes.Count || mistakesCount < 0)
                mistakesCount = word.mistakes.Count;

            var wordMistakes = word.mistakes;
            var rndMistakes = new List<Mistake>();
            for (int i = 0; i < mistakesCount; i++)
            {
                var rndMistake = wordMistakes.Random();
                rndMistakes.Add(rndMistake);
                wordMistakes.Remove(rndMistake);
            }
            
            word.mistakes = rndMistakes;
        }
        

        public void Begin() => PrepareTestStack();
        public bool TryGetNextWord(out Word word)
        {
            word = null;
            try
            {
                word = _testStack.Pop();
            }
            catch
            {
                return false;
            }
            ModifyMistakesInWord(-1, word);
            currentWord = word;
            return true;
        }
        public bool CheckWord(string word)
        {
            word = word.Replace(SpaceSurrogateSymbol,' ');
            var result = word.ToLower() == currentWord.name.ToLower();
            if (result)
            {
                rightAnswers++;
                currentWord.countOfTrainings++;
                SaveResult();
            }
            return result;
        }

        public TestResult End()
        {
            _usersData.SaveUsersData();
            return new TestResult(length, rightAnswers);
        }
        private void SaveResult()
        {
            _usersData.ModifyUserWord(currentWord);
        }
    }
}