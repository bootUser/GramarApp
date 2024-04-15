using Models;
using UnityEngine;

public class UsersEconomicSystem : MonoBehaviour
{
    private const int TestReward = 10;
    [SerializeField] private UsersDataManager _usersData;
    [SerializeField] private InfoBox _infoBox;
    public void GiveAdReward()
    {
        _usersData.TicketsBalance += 300;
    }

    public void GiveTestReward(TestResult result)
    {
        _usersData.TicketsBalance += result.rightAnswers * TestReward;
    }

    public void GetDailyReward()
    {
        int reward;
        if(_usersData.VisitingSeries >= 1 && _usersData.VisitingSeries < 4)
            reward = 50;
        else if(_usersData.VisitingSeries >= 4 && _usersData.VisitingSeries < 7)
            reward = 100;
        else
            reward = 150;
        
        _usersData.TicketsBalance += reward;
        _infoBox.Show("Ежедневная награда!", $"Награда за {_usersData.VisitingSeries} дней подряд: {reward}");
    }
}
