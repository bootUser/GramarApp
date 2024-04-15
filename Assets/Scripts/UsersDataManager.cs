using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Json;
using Models;

public class UsersDataManager : MonoBehaviour
{
    private static DataProvider _dataProvider;

    #region TicketsBalance
    private int _ticketsBalance;
    public event Action TicketBalanceUpdatedEvent;
    public int TicketsBalance
    {
        get => _ticketsBalance;
        set
        {
            _ticketsBalance = value;
            TicketBalanceUpdatedEvent?.Invoke();
            SaveUsersData();
        }
    }
    public void AddTickets(int value) => TicketsBalance += value;
    #endregion
    
    #region VisitingSeries
    private int _visitingSeries;
    public event Action VisitingSeriesUpdatedEvent;
    public int VisitingSeries
    {
        get => _visitingSeries;
        set
        {
            _visitingSeries = value;
            VisitingSeriesUpdatedEvent?.Invoke();
            SaveUsersData();
        }
    }
    #endregion
    
    #region LastVisitDate
    private DateTime _lastVisitDate;
    public event Action LastVisitDateUpdatedEvent;
    public DateTime LastVisitDate
    {
        get => _lastVisitDate;
        set
        {
            _lastVisitDate = value;
            LastVisitDateUpdatedEvent?.Invoke();
            SaveUsersData();
        }
    }
    #endregion

    #region UnknownWords
    private List<Word> _unknownWords = new List<Word>();
    public List<Word> UnknownWords => (from w in _unknownWords select w.Clone()).ToList();
    #endregion
    
    #region UsersWords
    private List<Word> _usersWords = new List<Word>();
    public List<Word> UsersWords => (from w in _usersWords select w.Clone()).ToList();
    #endregion

    #region TutorialCompleted
    private bool _tutorialCompleted;
    public bool TutorialCompleted
    {
        get => _tutorialCompleted;
        set
        {
            _tutorialCompleted = value;
            SaveUsersData();
        }
    }
    #endregion
    
    public void AddUserWord(Word word)
    {
        _unknownWords.Remove(_unknownWords.Find((w) => word.name == w.name));
        _usersWords.Add(word);
    }
    public void ModifyUserWord(Word word)
    {
        var origWord = _usersWords.Find((w) => word.name == w.name);
        origWord.countOfTrainings = word.countOfTrainings;
    }

    public void SaveUsersData()
    {
        _dataProvider.SaveUsersData(_ticketsBalance,_usersWords, _visitingSeries, _lastVisitDate, _tutorialCompleted);
    }
    
    public void Awake()
    {
        _dataProvider = new DataProvider(Application.persistentDataPath + "/Save.dat", "WordsGrammar");
        var data = _dataProvider.LoadUsersData();
        _usersWords = data.usersWords;
        _unknownWords = data.unknownWords;
        _ticketsBalance = data.tickets;
        _lastVisitDate = data.lastVisitDate;
        _visitingSeries = data.visitingSeries;
        _tutorialCompleted = data.tutorialCompleted;
    }
    
}