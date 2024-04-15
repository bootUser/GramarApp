using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using UnityEngine;

namespace WordPanels
{
    public class WordPanel : MonoBehaviour
    {
        protected string MarkupWordText(Word word)
        {
            List<string> markedSymbols = new List<string>();
            var mistakes = word.mistakes;
            for(int i =0;i<word.name.Length;i++)
            {
                string markingSymbol = word.name[i].ToString();
                var mistake = mistakes.Find((mistake => mistake.index == i));
                if (mistake == null)
                {
                    markedSymbols.Add(markingSymbol);
                    continue;
                }

                switch (mistake.type)
                {
                    case MistakeType.HyphenWriting:
                    case MistakeType.WrongLetter: markingSymbol = $"<color=red>{markingSymbol}</color>";
                        break;
                    case MistakeType.DoubleLetter:
                        if (word.name[i] == word.name[i + 1])
                        {
                            markedSymbols.Add(markingSymbol);
                            i++;
                            markingSymbol = word.name[i].ToString();
                            markingSymbol = $"<color=red>{markingSymbol}</color>";
                        }
                        else
                            markingSymbol = $"<color=yellow>{markingSymbol}</color>";
                        break;
                    case MistakeType.SplitWriting:
                        if(word.name[i] == ' ')
                            markingSymbol = "<color=red>□</color>";
                        else
                            markingSymbol = $"<color=yellow>{markingSymbol}</color>";
                        break;
                }
                markedSymbols.Add(markingSymbol);
            }

            string result = "";
            foreach (var substring in markedSymbols)
                result += substring;
            return result;
        }
    }
}