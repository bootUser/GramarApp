using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WordsTests;

namespace UIPages
{
    public class TrainingPage : UIPage
    {
        private const string RightAnswerPlaceholder = "Правильный вариант: ";
        private WordsTest _currentTest;
        [SerializeField] private GameObject _wordPanel;
        [SerializeField] private TMP_InputField _inputFieldPref;
        [SerializeField] private TextMeshProUGUI _wordSymbolPref;
        [SerializeField] private UIPagesManager _pagesManager;
        [SerializeField] private TextMeshProUGUI _rightAnswerLabel;
        [SerializeField] private TextMeshProUGUI _rightAnswerText;
        [SerializeField] private Button _checkButton;
        [SerializeField] private ResultsPage _resultsPage;
        [SerializeField] private AudioManager _audioManager;

        private bool isShowingAnswer = false;
        public void StartTest(WordsTest test)
        {
            _rightAnswerLabel.gameObject.SetActive(false);
            _currentTest = test;
            _currentTest.Begin();
            DisplayNextWord();
        }

        private void DestroyLastWord()
        {
            foreach (var s in GameObject.FindGameObjectsWithTag("WordSymbol"))
                Destroy(s);
        }
        private void DisplayNextWord()
        {
            DestroyLastWord();

            Word word;
            if (!_currentTest.TryGetNextWord(out word))
            {
                OnTestEnds();
                return;
            }

            var mistakes = word.mistakes;
            for(int i =0;i<word.name.Length;i++)
            {
                string symbol = word.name[i].ToString();
                var mistake = mistakes.Find((mistake => mistake.index == i));
                
                if (mistake == null)
                {
                    Instantiate(_wordSymbolPref, _wordPanel.transform).text = symbol;
                    continue;
                }
                switch (mistake.type)
                {
                    case MistakeType.HyphenWriting:
                    case MistakeType.WrongLetter: Instantiate(_inputFieldPref, _wordPanel.transform);
                        break;
                    case MistakeType.DoubleLetter:
                        Instantiate(_inputFieldPref, _wordPanel.transform);
                        Instantiate(_inputFieldPref, _wordPanel.transform);
                        if(word.name[i] == word.name[i+1])
                            i++;
                        break;
                    case MistakeType.SplitWriting:
                        if(word.name[i] == ' ')
                            Instantiate(_inputFieldPref, _wordPanel.transform);
                        else
                        {
                            Instantiate(_wordSymbolPref, _wordPanel.transform).text = symbol;
                            Instantiate(_inputFieldPref, _wordPanel.transform);
                        }
                        break;
                }
            }
        }

        public void CheckWordOrGoNext()
        {
            if (isShowingAnswer)
            {
                HideRightAnswer();
                DisplayNextWord();
            }
            else if (CheckWord())
            {
                StartCoroutine(OnRightAnswer(1.2f));
            }
            else
                ShowRightAnswer();
        }

        private bool CheckWord()
        {
            string usersWord = "";
            foreach (var s in GameObject.FindGameObjectsWithTag("WordSymbol"))
            {
                if(s.TryGetComponent(out TextMeshProUGUI textMesh))
                    usersWord += textMesh.text;
                else if (s.TryGetComponent(out TMP_InputField field))
                    usersWord += field.text;
            }
            return _currentTest.CheckWord(usersWord);
        }

        private IEnumerator OnRightAnswer(float seconds)
        {
            _audioManager.PlayRightAnswerSound();
            DestroyLastWord();
            _checkButton.gameObject.SetActive(false);
            _rightAnswerText.text = $"<color=green>{_currentTest.currentWord.name}</color>";
            yield return new WaitForSeconds(seconds);
            _rightAnswerText.text = "";
            _checkButton.gameObject.SetActive(true);
            DisplayNextWord();
        }
        private void ShowRightAnswer()
        {
            _audioManager.PlayWrongAnswerSound();
            DestroyLastWord();
            isShowingAnswer = true;
            _rightAnswerLabel.gameObject.SetActive(true);
            _rightAnswerText.text = _currentTest.currentWord.name;
            if(AppSettings.VibrationEnabled)
                Handheld.Vibrate();
        }
        private void HideRightAnswer()
        {
            _rightAnswerLabel.gameObject.SetActive(false);
            isShowingAnswer = false;
            _rightAnswerText.text = "";
        }

        private void OnTestEnds()
        {
            var result = _currentTest.End();
            Debug.Log($"{result.rightAnswers}/{result.totalQuestions}");
            _resultsPage.ConfigurePage(result);
            _pagesManager.SetCurrentScreen(_resultsPage);
        }
    }
}
