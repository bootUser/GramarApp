using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Ads
{
    public class BannerAd : MonoBehaviour
    {
        [SerializeField] private string _adndroidUnitId;
        [SerializeField] private string _IOSUnitId;
        [SerializeField] private BannerPosition _bannerPosition;

        
        private AdsInitializer _adsInitializer;
        private string _AdUnitId;
        private void Awake()
        {
            _adsInitializer = GetComponent<AdsInitializer>();
            _AdUnitId = (Application.platform == RuntimePlatform.IPhonePlayer) ? _IOSUnitId : _adndroidUnitId;
        }

        private void Start()
        {
            if (_adsInitializer.enableAdvertisement)
            {
                Advertisement.Banner.SetPosition(_bannerPosition);
                StartCoroutine(LoadBannerAfterSeconds(1f));
            }
            else
            {
                HideBanner();
            }
        }

        private IEnumerator LoadBannerAfterSeconds(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            LoadBanner();
        }

        public void LoadBanner()
        {
            var options = new BannerLoadOptions()
            {
                errorCallback = OnBannerError,
                loadCallback = OnBannerLoaded
            };
            Advertisement.Banner.Load(_AdUnitId, options);
        }

        private void OnBannerLoaded()
        {
            Debug.Log("Banner loaded!");
        }

        private void OnBannerError(string message)
        {
            Debug.Log($"Banner error: {message}");
        }

        public void ShowBanner()
        {
            var options = new BannerOptions()
            {
                clickCallback = OnBannerClicked,
                showCallback = OnBannerShown,
                hideCallback = OnBannerHidden
            };
            Advertisement.Banner.Show(_AdUnitId, options);
        }
        private void OnBannerClicked()
        {}
        private void OnBannerShown()
        {}
        private void OnBannerHidden()
        {}

        public void HideBanner()
        {
            Advertisement.Banner.Hide();
        }
        
    }
}