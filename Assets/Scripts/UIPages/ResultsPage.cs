using Models;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIPages
{
    public class ResultsPage : UIPage
    {
        private const string EarnedMoneyTextPlaceholder = "Получено:";
        private const string EarnedMoneyTextSecondPlaceholder = "билетов";
        [SerializeField] private TextMeshProUGUI _resultText;
        [SerializeField] private TextMeshProUGUI _earnedMoneyText;
        [SerializeField] private UsersEconomicSystem _economicSystem;
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private TextMeshProUGUI _ticketsNumber;
        [SerializeField] private UsersDataManager usersData;

        public void Start()
        {
            _ticketsNumber.text = usersData.TicketsBalance.ToString();
            usersData.TicketBalanceUpdatedEvent += () =>
            {
                _ticketsNumber.text = usersData.TicketsBalance.ToString();
            };
        }
        
        public void ConfigurePage(TestResult result)
        {
            _resultText.text = $" {result.rightAnswers}/{result.totalQuestions}";
            _earnedMoneyText.text = EarnedMoneyTextPlaceholder + $" {result.rightAnswers * 10} " + EarnedMoneyTextSecondPlaceholder;
            _economicSystem.GiveTestReward(result);
            _audioManager.PlayMoneyEarnedSound();
        }
    }
}