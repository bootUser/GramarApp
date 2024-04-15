using System;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class AdsInitializer : MonoBehaviour,IUnityAdsInitializationListener
    {
        [SerializeField] private bool _testMode;
        [SerializeField] private string _androidGameId;
        [SerializeField] private string _IOSGameId;
        public bool enableAdvertisement;

        private string _gameId;

        private void Awake()
        {
            if(enableAdvertisement)
                InitializeAds();
        }

        public void InitializeAds()
        {
            _gameId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _IOSGameId : _androidGameId;
            Advertisement.Initialize(_gameId, _testMode, this);
        }

        public void OnInitializationComplete()
        {
            Debug.Log("Initialization completed!");
        }

        public void OnInitializationFailed(UnityAdsInitializationError error, string message)
        {
            Debug.Log("Initialization failed :(");
        }
    }
}