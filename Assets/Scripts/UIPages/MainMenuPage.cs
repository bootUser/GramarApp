using Ads;
using TMPro;
using UnityEngine;

namespace UIPages
{
    public class MainMenuPage : UIPage
    {
        private const string SeriesTextPlaceholder = "Серия:";
        private const string SeriesTextPlaceholderLast = "дней подряд";
        [SerializeField] private TextMeshProUGUI _ticketsNumber;
        [SerializeField] private TextMeshProUGUI _seriesNumber;
        [SerializeField] private UsersDataManager usersData;
        [SerializeField] private BannerAd _adBanner;
        [SerializeField] private InfoBox _tutorialBox;

        public void Start()
        {
            _ticketsNumber.text = usersData.TicketsBalance.ToString();
            usersData.TicketBalanceUpdatedEvent += () =>
            {
                _ticketsNumber.text = usersData.TicketsBalance.ToString();
            };
            _seriesNumber.text = $"{SeriesTextPlaceholder} {usersData.VisitingSeries} {SeriesTextPlaceholderLast}";
            usersData.VisitingSeriesUpdatedEvent += () =>
            {
                _seriesNumber.text = $"{SeriesTextPlaceholder} {usersData.VisitingSeries} {SeriesTextPlaceholderLast}";
            };
            if (!usersData.TutorialCompleted)
            {
                _tutorialBox.Show("Обучение",DataProvider.TutorialText);
                usersData.TutorialCompleted = true;
            }
        }

        protected override void OnPageActivating()
        {
            _adBanner.ShowBanner();
        }

        protected override void OnPageClosing()
        {
            _adBanner.HideBanner();
        }
    }
}
