using System;
using Ads;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WordsTests;

namespace UIPages
{
    public class TrainingSettingsPage : UIPage
    {
        [SerializeField] private BannerAd _adBanner;
        [SerializeField] private UsersDataManager _dataManager;
        [SerializeField] private TrainingPage _trainingPage;
        [SerializeField] private CustomTestSettingsPage _customTestSettingsPage;
        [SerializeField] private TMP_InputField _wordsNumberField;
        [SerializeField] private UIPagesManager _pagesManager;
        [SerializeField] private InfoBox _infoBox;

        public void StartTest(string type)
        {
            switch (type.ToLower())
            {
                case "general":
                    if (!IsCorrectWordsNumber())
                    {
                        _infoBox.Show("Внимание!",
                            $"Введено неверное колличество. Вам доступно: {_dataManager.UsersWords.Count} слов.");
                        return;
                    }
                    _trainingPage.StartTest(new GeneralTest(int.Parse(_wordsNumberField.text),_dataManager.UsersWords , _dataManager));
                    break;
                case "custom":
                    if (_dataManager.UsersWords.Count == 0)
                    {
                        _infoBox.Show("Внимание!", $"Купите слова в магазине.");
                        return;
                    }
                    _customTestSettingsPage.Configure((words) =>
                    {
                        _trainingPage.StartTest(new CustomTest(words, _dataManager));
                        _pagesManager.SetCurrentScreen(_trainingPage);
                    });
                    _pagesManager.SetCurrentScreen(_customTestSettingsPage);
                    return;
                case "full":
                    if (_dataManager.UsersWords.Count == 0)
                    {
                        _infoBox.Show("Внимание!", $"Купите слова в магазине.");
                        return;
                    }
                    _trainingPage.StartTest(new FullTest(_dataManager.UsersWords, _dataManager));
                    break;
            }
            _pagesManager.SetCurrentScreen(_trainingPage);
        }

        private bool IsCorrectWordsNumber()
        {
            if (int.TryParse(_wordsNumberField.text, out int number))
                return number <= _dataManager.UsersWords.Count && number > 0;
            else
                return false;
        }
        protected override void OnPageActivating()
        {
            _adBanner.HideBanner();
        }
    }
}