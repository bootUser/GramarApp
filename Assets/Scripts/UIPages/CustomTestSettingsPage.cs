using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using TMPro;
using UIElements;
using UnityEngine;
using UnityEngine.UI;
using WordPanels;

namespace UIPages
{
    public class CustomTestSettingsPage:UIPage
    {
        #region GameObjects
        [SerializeField] private TextMeshProUGUI _wordsSelectedCountText;
        [SerializeField] private UsersDataManager usersData;
        [SerializeField] private Button _applyButton;
        [SerializeField] private GameObject _wordPanelPref;
        [SerializeField] private GameObject _wordsContentPanel;
        [SerializeField] private SmartDropdown _smartDropdown;
        [SerializeField] private Toggle _selectAllToggle;
        #endregion
        
        private const string SelectedWordsNumberInsertion = "Слов выбрано: ";
        
        private Action<List<Word>> _applyAction;
        private string _currentFilterTag = SmartDropdown.SelectAllOptionName;
        private List<SelectableWordPanel> _selectedWords = new List<SelectableWordPanel>();

        private void Start()
        {
            _smartDropdown.Configure(AppSettings.allWordsTags);
            _smartDropdown.onSelectionChanged += (option) => 
            {
                _currentFilterTag = option;
                _selectAllToggle.isOn = false;
                ResetWordsPanel(FilterWordsByTag(option, usersData.UsersWords));
            };
        }

        public void Configure(Action<List<Word>> actionOnApply)
        {
            _applyAction = actionOnApply;
        }

        private void CreateWordsPanels(List<Word> source)
        {
            for (var index = 0; index < source.Count; index++)
            {
                var word = source[index];
                var wordPanel = Instantiate(_wordPanelPref, _wordsContentPanel.transform).GetComponent<SelectableWordPanel>();
                wordPanel.Configure(word,(toggled,self)=>
                {
                    if (toggled)
                        AddWord(self);
                    else
                        RemoveWord(self);
                }, this);
            }
        }
        private List<Word> FilterWordsByTag(string tag, List<Word> source) =>
            tag == SmartDropdown.SelectAllOptionName ? source : (from word in source where word.tags != null && word.tags.Contains(tag) select word).ToList();
        
        public void ChangeAllWordPanelsSelection(bool value)
        {
            foreach(var wp in _wordsContentPanel.transform.GetComponentsInChildren<SelectableWordPanel>())
                wp.GetComponentInChildren<Toggle>().isOn = value;
        }
        private void DestroyWordsPanels()
        {
            foreach(var wp in _wordsContentPanel.transform.GetComponentsInChildren<SelectableWordPanel>())
                Destroy(wp.gameObject);
        }
        
        public void AddWord(SelectableWordPanel selectableWordPanel)
        {
            _selectedWords.Add(selectableWordPanel);
            _applyButton.gameObject.SetActive(true);
            _wordsSelectedCountText.text = SelectedWordsNumberInsertion + _selectedWords.Count.ToString();
        }
        public void RemoveWord(SelectableWordPanel selectableWordPanel)
        {
            _selectedWords.Remove(selectableWordPanel);
            if(_selectedWords.Count == 0)
                _applyButton.gameObject.SetActive(false);
            _wordsSelectedCountText.text = SelectedWordsNumberInsertion + _selectedWords.Count.ToString();
        }
        public void Apply()
        {
            _applyButton.gameObject.SetActive(false);
            var selectedWords = (from word in _selectedWords select word.word).ToList();
            _applyAction.Invoke(selectedWords);
        }

        private void ResetWordsPanel(List<Word> source)
        {
            DestroyWordsPanels();
            CreateWordsPanels(source);
            _selectedWords.Clear();
            _wordsSelectedCountText.text = SelectedWordsNumberInsertion + _selectedWords.Count.ToString();
        }

        protected override void OnPageActivating()
        {
            ResetWordsPanel(usersData.UsersWords);
            _applyButton.gameObject.SetActive(false);
        }
    }
}