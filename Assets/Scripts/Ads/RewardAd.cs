using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

namespace Ads
{
    public class RewardAd : MonoBehaviour, IUnityAdsShowListener, IUnityAdsLoadListener
    {
        [SerializeField] private string _adndroidUnitId;
        [SerializeField] private string _IOSUnitId;
        [SerializeField] private UsersEconomicSystem _economicSystem;
        [SerializeField] private Button _button;
        private string _AdUnitId;

        private void Awake()
        {
            _AdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _IOSUnitId : _adndroidUnitId;
            _button.interactable = false;
        }

        private void Start()
        {
            StartCoroutine(LoadAdAfterSeconds(1f));
        }
        private IEnumerator LoadAdAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            LoadAd();
        }

        public void ShowAd()
        {
            Advertisement.Show(_AdUnitId, this);
        }

        public void LoadAd()
        {
            Advertisement.Load(_AdUnitId, this);
        }
        
        public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
        {
        }

        public void OnUnityAdsShowStart(string placementId)
        {
        }

        public void OnUnityAdsShowClick(string placementId)
        {
        }

        public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
        {
            if ( placementId == _AdUnitId && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            {
                _economicSystem.GiveAdReward();
            }
            LoadAd();
        }

        public void OnUnityAdsAdLoaded(string placementId)
        {
            _button.interactable = true;
            Debug.Log($"{placementId} ad loaded");
        }

        public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
        {
            Debug.Log($"{placementId} ad failed to load. Message: {message}");
        }
    }
}