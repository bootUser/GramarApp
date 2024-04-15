using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Json;
using Models;
using UnityEngine;

public class DataProvider
{
    private string _dataPath;
    private string _jsonResource;
    private List<Word> _grammarWords;
    public const string TutorialText = 
@"Это приложение создано для запоминания написания словарных слов русского языка. За правильные ответы в тестах будут начисляться билеты, с помощью которых можно покупать слова в магазине.

Словарь отображает купленные слова, а так-же ошибки в них. Красными отмечены дефисы, пробелы, удвоенные буквы и буквы, которые можно написать неверно. Желтым помечаются потенциальные ошибки: слитное написание (кажется, что раздельное), одиночная буква (кажется что удвоенная). Рядом с каждым словом присутствует процент выученности.

В тестах ошибки слов будут заменены квадратами, которые необходимо заполнить: вставить букву/дефис, не вставлять, если нужно слитное написание, поставить /, если нужно раздельное написание. Если появилось два квадрата, но буква должна быть одна - заполните любой из них, а другой оставьте пустым.

Ежедневную награду можно забирать каждые новые сутки. Чем больше серия дней подряд, тем больше сумма награды. При пропуске, серия сбивается.

Это сообщение больше не будет показываться при запуске, но его можно заново прочитать в настройках.

Не забудьте приобрести первые слова!!

Удачи и успехов в обучении!";
    
    public DataProvider(string path, string grammarResourceName)
    {
        _dataPath = path;
        _jsonResource = grammarResourceName;
        LoadGrammarWords();
    }

    private void LoadGrammarWords()
    {
        var jsonText = Resources.Load<TextAsset>(_jsonResource);
        _grammarWords = JsonHelper.FromJson<Word>(jsonText.text).ToList();
        RestoreEnumTypes(_grammarWords);
    }
    
    public void SaveUsersData(int ticketBalance, List<Word> usersWords, int visitingSeries, DateTime lastVisitDate, bool tutorialCompleted)
    {
        if(File.Exists(_dataPath))
            File.Delete(_dataPath);
        var dataToSave = new SaveData() {tickets = ticketBalance, usersWords = usersWords, visitingSeries = visitingSeries,
            lastVisitDateString = lastVisitDate.ToString(), tutorialCompleted = tutorialCompleted};
        var file = File.Open(_dataPath, FileMode.Create, FileAccess.Write);
        new BinaryFormatter().Serialize(file, dataToSave);
        file.Close();
        file.Dispose();
    }

    public (int tickets,List<Word> usersWords,List<Word> unknownWords, int visitingSeries, DateTime lastVisitDate, bool tutorialCompleted) LoadUsersData()
    {
        var result = (tickets:7000,usersWords:new List<Word>(),unknownWords:_grammarWords,
            visitingSeries:0, lastVisitDate:default(DateTime), tutorialCompleted:false);
        //Load users save data
        if (File.Exists(_dataPath))
        {
            var file = File.Open(_dataPath, FileMode.Open, FileAccess.Read);
            var loadedData =
                (SaveData) new BinaryFormatter().Deserialize(file);
            file.Close();
            file.Dispose();
            result.usersWords = loadedData.usersWords;
            result.tickets = loadedData.tickets;
            result.visitingSeries = loadedData.visitingSeries;
            result.tutorialCompleted = loadedData.tutorialCompleted;
            if(DateTime.TryParse(loadedData.lastVisitDateString, out var date))
                result.lastVisitDate = date;
        }
        
        //Delete users words from unknown words
        foreach (var w in result.usersWords)
        {
            var usersWord = result.unknownWords.Find((word) => word.name == w.name);
            if (usersWord != null)
                result.unknownWords.Remove(usersWord);
        }
        RestoreEnumTypes(result.usersWords);

        return result;
    }

    private void RestoreEnumTypes(List<Word> words)
    {
        foreach(var word in words)
        foreach (var mst in word.mistakes)
            mst.type = (MistakeType) Enum.Parse(typeof(MistakeType), mst.strType);
    }
}
