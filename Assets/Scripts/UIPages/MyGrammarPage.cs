using TMPro;
using UnityEngine;
using WordPanels;

namespace UIPages
{
    public class MyGrammarPage : UIPage
    {
        private const string WordsNumberInsertion = "Всего слов: ";
        [SerializeField] private TextMeshProUGUI _wordsCountText;
        [SerializeField] private GameObject _grammarContentPanel;
        [SerializeField] private GrammarWordPanel _wordPanelPref;
        [SerializeField] private UsersDataManager usersData;
        protected override void OnPageActivating()
        {
            _wordsCountText.text = WordsNumberInsertion + usersData.UsersWords.Count.ToString();
            foreach(var wp in _grammarContentPanel.transform.GetComponentsInChildren<GrammarWordPanel>())
                Destroy(wp.gameObject);
            CreateGrammarPanels();
        }
        private void CreateGrammarPanels()
        {
            for (var index = 0; index < usersData.UsersWords.Count; index++)
            {
                var word = usersData.UsersWords[index];
                var wordPanel = Instantiate(_wordPanelPref, _grammarContentPanel.transform).GetComponent<GrammarWordPanel>();
                wordPanel.Configure(word, word.countOfTrainings);
            }
        }
    }
}