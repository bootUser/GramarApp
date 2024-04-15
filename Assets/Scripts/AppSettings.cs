using System;
using System.Collections.Generic;
using UnityEngine;

public static class AppSettings
{
    private const string RightAnswerSoundEnabledKey = "RightAnswerSoundEnabled";
    private const string WrongAnswerSoundEnabledKey = "WrongAnswerSoundEnabled";
    private const string MoneyEarnedSoundEnabledKey = "MoneyEarnedSoundEnabled";
    private const string VibrationEnabledKey = "VibrationEnabled";
    public static List<string> allWordsTags = new List<string>{"НН", "Пре-При"};

    static AppSettings()
    {
        if(!PlayerPrefs.HasKey(RightAnswerSoundEnabledKey))
            PlayerPrefs.SetInt(RightAnswerSoundEnabledKey, 1);
        if(!PlayerPrefs.HasKey(WrongAnswerSoundEnabledKey))
            PlayerPrefs.SetInt(WrongAnswerSoundEnabledKey, 1);
        if(!PlayerPrefs.HasKey(MoneyEarnedSoundEnabledKey))
            PlayerPrefs.SetInt(MoneyEarnedSoundEnabledKey, 1);
        if(!PlayerPrefs.HasKey(VibrationEnabledKey))
            PlayerPrefs.SetInt(VibrationEnabledKey, 1);
    }
    
    public static bool RightAnswerSoundEnabled
    {
        get => PlayerPrefs.GetInt(RightAnswerSoundEnabledKey) == 1;
        set
        {
            PlayerPrefs.SetInt(RightAnswerSoundEnabledKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    public static bool WrongAnswerSoundEnabled
    {
        get => PlayerPrefs.GetInt(WrongAnswerSoundEnabledKey) == 1;
        set
        {
            PlayerPrefs.SetInt(WrongAnswerSoundEnabledKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool MoneyEarnedSoundEnabled
    {
        get => PlayerPrefs.GetInt(MoneyEarnedSoundEnabledKey) == 1;
        set
        {
            PlayerPrefs.SetInt(MoneyEarnedSoundEnabledKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
    public static bool VibrationEnabled
    {
        get => PlayerPrefs.GetInt(VibrationEnabledKey) == 1;
        set
        {
            PlayerPrefs.SetInt(VibrationEnabledKey, value ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}