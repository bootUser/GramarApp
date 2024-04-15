using Models;
using TMPro;
using UnityEngine;

namespace WordPanels
{
    public class GrammarWordPanel : WordPanel
    {
        private const string PercentNumberInsertion = "%";
        [SerializeField] private TextMeshProUGUI _wordText;
        [SerializeField] private TextMeshProUGUI _learnPercenText;

        public void Configure(Word panelWord, int learnPercent)
        {
            _wordText.text = MarkupWordText(panelWord);
            _learnPercenText.text = learnPercent.ToString() +PercentNumberInsertion;
        }
    }
}
