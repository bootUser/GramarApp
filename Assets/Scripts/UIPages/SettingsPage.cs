using UnityEngine;
using UnityEngine.UI;
using WordsTests;

namespace UIPages
{
    public class SettingsPage : UIPage
    {
        [SerializeField] private Toggle _rightAnswerToggle;
        [SerializeField] private Toggle _wrongAnswerToggle;
        [SerializeField] private Toggle _moneyEarnedToggle;
        [SerializeField] private Toggle _vibrationToggle;
        [SerializeField] private InfoBox _tutorialBox;

        private void Awake()
        {
            _rightAnswerToggle.onValueChanged.AddListener(value => AppSettings.RightAnswerSoundEnabled = value);
            _wrongAnswerToggle.onValueChanged.AddListener(value=> AppSettings.WrongAnswerSoundEnabled = value);
            _moneyEarnedToggle.onValueChanged.AddListener(value=> AppSettings.MoneyEarnedSoundEnabled = value);
            _vibrationToggle.onValueChanged.AddListener(value => AppSettings.VibrationEnabled = value);
        }
        protected override void OnPageActivating()
        {
            _rightAnswerToggle.isOn = AppSettings.RightAnswerSoundEnabled;
            _wrongAnswerToggle.isOn = AppSettings.WrongAnswerSoundEnabled;
            _moneyEarnedToggle.isOn = AppSettings.MoneyEarnedSoundEnabled;
            _vibrationToggle.isOn = AppSettings.VibrationEnabled;
        }

        public void ShowTutorial()
        {
            _tutorialBox.Show("Обучение",DataProvider.TutorialText);
        }
    }
}