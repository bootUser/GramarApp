using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UIPages;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIElements
{
    public class SmartDropdown : MonoBehaviour
    {
        public const string SelectAllOptionName = "Все";
        public event Action<string> onSelectionChanged;
        
        [SerializeField] private TMP_Dropdown _dropdown;
        [SerializeField] private UIPage _page;
        void Start()
        {
            var sr = GetComponent<ScrollRect>();
            _page.PageActivatedEvent += () =>
            {
                _dropdown.value = _dropdown.options.IndexOf(_dropdown.options.Find(
                    o => o.text == SelectAllOptionName));
            };
        }

        public void Configure(List<string> options)
        {
            var result = (from option in options select new TMP_Dropdown.OptionData(option)).ToList();
            result.Add(new TMP_Dropdown.OptionData(SelectAllOptionName));
            result.Reverse();
            _dropdown.options = result;
            _dropdown.onValueChanged.AddListener((index) => onSelectionChanged.Invoke(_dropdown.options[index].text));
        }
    }
}