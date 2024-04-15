using System;
using Models;
using TMPro;
using UIPages;
using UnityEngine;
using UnityEngine.UI;

namespace WordPanels
{
    public class SelectableWordPanel : WordPanel
    {
        private TextMeshProUGUI _wordLabel;
        private Toggle _toggle;
        public Word word;

        public void Awake()
        {
            _wordLabel = GetComponentInChildren<TextMeshProUGUI>();
            _toggle = GetComponentInChildren<Toggle>();
        }

        public void Configure(Word panelWord, Action<bool, SelectableWordPanel> selectedAction, UIPage uiPage)
        {
            word = panelWord;
            _wordLabel.text = MarkupWordText(panelWord);
            _toggle.GetComponent<ToggleUpdater>().Configure(uiPage);
            _toggle.onValueChanged.AddListener((value)=> selectedAction.Invoke(value, this));
        }
    }
}