using System.Collections.Generic;
using System.Linq;
using Models;
using TMPro;
using UIElements;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using WordPanels;

namespace UIPages
{
    public class WordsShopPage : UIPage
    {
        private const string TotalTicketsTextInsertion = "Сумма: ";
        
        #region GameObjects
        [SerializeField] private TextMeshProUGUI _ticketsText;
        [SerializeField] private TextMeshProUGUI _totalTicketsText;
        [SerializeField] private UsersDataManager usersData;
        [SerializeField] private Button _buyButton;
        [SerializeField] private GameObject _wordPanelPref;
        [SerializeField] private GameObject _shopContentPanel;
        [SerializeField] private SmartDropdown _smartDropdown;
        [SerializeField] private Toggle _selectAllToggle;
        #endregion
        
        private List<SelectableWordPanel> _wordsInCart = new List<SelectableWordPanel>();
        private string _currentFilterTag = SmartDropdown.SelectAllOptionName;
        private int _totalPrice;
        private int totalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                _totalTicketsText.text = TotalTicketsTextInsertion + _totalPrice;
            }
        }

        public void Start()
        {
            //Ticket number update handler
            _ticketsText.text = usersData.TicketsBalance.ToString();
            usersData.TicketBalanceUpdatedEvent += () => { _ticketsText.text = usersData.TicketsBalance.ToString(); };
            _smartDropdown.Configure(AppSettings.allWordsTags);
            _smartDropdown.onSelectionChanged += (option) => 
            {
                _currentFilterTag = option;
                _selectAllToggle.isOn = false;
                ResetShop(FilterWordsByTag(option, usersData.UnknownWords));
            };
        }
        
        protected override void OnPageActivating()
        {
            ResetShop(usersData.UnknownWords);
            _buyButton.gameObject.SetActive(false);
        }
        
        private List<Word> FilterWordsByTag(string tag, List<Word> source) =>
            tag == SmartDropdown.SelectAllOptionName ? source : (from word in source where word.tags != null && word.tags.Contains(tag) select word).ToList();

        private void CreateShopPanels(List<Word> source)
        {
            for (var index = 0; index < source.Count; index++)
            {
                var word = source[index];
                var wordPanel = Instantiate(_wordPanelPref, _shopContentPanel.transform).GetComponent<SelectableWordPanel>();
                wordPanel.Configure(word, (toggled, self) =>
                {
                    if(toggled)
                        AddWordInCart(self);
                    else
                        RemoveWordFromCart(self);
                }, this);
            }
        }

        private void DestroyShopPanels()
        {
            foreach(var wp in _shopContentPanel.transform.GetComponentsInChildren<SelectableWordPanel>())
                Destroy(wp.gameObject);
        }

        public void ChangeAllWordPanelsSelection(bool value)
        {
            foreach(var wp in _shopContentPanel.transform.GetComponentsInChildren<SelectableWordPanel>())
                wp.GetComponentInChildren<Toggle>().isOn = value;
        }
        
        public void AddWordInCart(SelectableWordPanel selectableWordPanel)
        {
            totalPrice += 100;
            _wordsInCart.Add(selectableWordPanel);
            _buyButton.gameObject.SetActive(true);
        }
        public void RemoveWordFromCart(SelectableWordPanel selectableWordPanel)
        {
            totalPrice -= 100;
            _wordsInCart.Remove(selectableWordPanel);
            if(_wordsInCart.Count == 0)
                _buyButton.gameObject.SetActive(false);
        }
        public void BuyWords()
        {
            if (usersData.TicketsBalance < totalPrice || totalPrice == 0)
                return;
            usersData.TicketsBalance -= totalPrice;
            foreach (var wordPanel in _wordsInCart)
                usersData.AddUserWord(wordPanel.word);
            
            usersData.SaveUsersData();
            ResetShop(FilterWordsByTag(_currentFilterTag, usersData.UnknownWords));
            _buyButton.gameObject.SetActive(false);
        }

        private void ResetShop(List<Word> source)
        {
            _wordsInCart.Clear();
            DestroyShopPanels();
            CreateShopPanels(source);
            totalPrice = 0;
        }
    }
}