using System;
using UnityEngine;

public class DailyRewardManager : MonoBehaviour
{
    [SerializeField] private UsersDataManager _usersData;
    [SerializeField] private UsersEconomicSystem _economicSystem;
    [SerializeField] private InfoBox _infoBox;

    public void GetDailyReward()
    {
        var delta = (DateTime.Now.Date - _usersData.LastVisitDate.Date).TotalDays;
        bool firstClaim = _usersData.LastVisitDate == default(DateTime);
        _usersData.LastVisitDate = DateTime.Now;
        if ((delta >= 1 && delta < 2) || firstClaim)
        {
            _usersData.VisitingSeries++;
            _usersData.SaveUsersData();
            _economicSystem.GetDailyReward();
        }
        else if (delta < 1)
        {
            _infoBox.Show("Внимание!","Награда уже получена, возвращайтесь завтра.");
        }
        else
        {
            _infoBox.Show("Внимание!", "Вы пропустили день. Серия обнулена.");
            _usersData.VisitingSeries = 1;
            _usersData.SaveUsersData();
            _economicSystem.GetDailyReward();
        }
    }
}
