using CAS;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class Ads : MonoBehaviour
{
    public ConsentStatus userConsent;
    public CCPAStatus userCCPAStatus;
    public IMediationManager manager;

    private bool isAppReturnEnable = false;

    //private IAdView bannerView;

    private int rew_id;
    private int rev_VisualNumber;

    public GameManager GM;

    private DateTime _lastInterstitialTime;
    private int _interstitialPause = 120;

    private DateTime _lastRewardTime;
    private int _rewardPause = 300;

    private DateTime _lastAdditionalTubeTime;

    public void Start()
    {
        GetValues();
        MobileAds.settings.userConsent = userConsent;
        MobileAds.settings.userCCPAStatus = userCCPAStatus;

        MobileAds.settings.isExecuteEventsOnUnityThread = true;

        manager = MobileAds.BuildManager().Initialize();

        manager.OnRewardedAdCompleted += RewardedSuccessful;
        manager.OnInterstitialAdClosed += OnInterstitialAdClosed;

        //InvokeRepeating("OnRefreshStatus", 1.0f, 1.0f);

        //bannerView = manager.GetAdView(AdSize.Banner);
        //ShowBanner();
        rew_id = 10;
    }

    private void OnInterstitialAdClosed()
    {
        _lastInterstitialTime = DateTime.Now;
    }

    public void OnLevelComplete(int level)
    {
        if (PlayerPrefs.GetInt("NoAds") != 1 &&
            (level == 6 || level == 8 ||
            (level >= 11 && (DateTime.Now - _lastInterstitialTime).TotalSeconds > _interstitialPause)))
        {
            ShowInterstitial();
        }
    }

    public void OnChangeTubeTheme(int visualNumber)
    {
        int id = 2;

        if ((DateTime.Now - _lastRewardTime).TotalSeconds > _rewardPause)
        {
            _lastRewardTime = DateTime.Now;
            ShowRewarded(id, visualNumber);
        }
        else
        {
            rew_id = id;
            rev_VisualNumber = visualNumber;
            RewardedSuccessful();
        }
    }

    public void OnAddSteps(int visualNumber)
    {
        int id = 1;

        if ((DateTime.Now - _lastRewardTime).TotalSeconds > _rewardPause)
        {
            _lastRewardTime = DateTime.Now;
            ShowRewarded(id, visualNumber);
        }
        else
        {
            rew_id = id;
            rev_VisualNumber = visualNumber;
            RewardedSuccessful();
        }
    }

    public void OnChangeBackgroundTheme(int visualNumber)
    {
        int id = 3;

        if ((DateTime.Now - _lastRewardTime).TotalSeconds > _rewardPause)
        {
            _lastRewardTime = DateTime.Now;
            ShowRewarded(id, visualNumber);
        }
        else
        {
            rew_id = id;
            rev_VisualNumber = visualNumber;
            RewardedSuccessful();
        }
    }

    public void OnGetAdditionalTube(int visualNumber)
    {
        int id = 0;

        if ((DateTime.Now - _lastAdditionalTubeTime).TotalSeconds > _rewardPause)
        {
            _lastAdditionalTubeTime = DateTime.Now;
            ShowRewarded(id, visualNumber);
        }
        else
        {
            rew_id = id;
            rev_VisualNumber = visualNumber;
            RewardedSuccessful();
        }
    }

    public void OnGetAdTube(int visualNumber)
    {
        int id = 4;

        if ((DateTime.Now - _lastAdditionalTubeTime).TotalSeconds > 1)//_rewardPause)
        {
            _lastAdditionalTubeTime = DateTime.Now;
            ShowRewarded(id, visualNumber);
        }
    }

    //public void ShowBanner()
    //{
    //    if (PlayerPrefs.GetInt("NoAds") != 1)
    //        bannerView.SetActive(true);
    //}

    //public void HideBanner()
    //{
    //    bannerView.SetActive(false);
    //}

    //public void SetBannerPosition(int positionEnum)
    //{
    //    bannerView.position = (AdPosition)positionEnum;
    //}

    //public void SetBannerSize(int sizeID)
    //{
    //    bannerView = manager.GetAdView((AdSize)sizeID);
    //    ShowBanner();
    //}

    public void ShowInterstitial()
    {
        if (manager.IsReadyAd(AdType.Interstitial))
            manager.ShowAd(AdType.Interstitial);
        else
        {
            Debug.LogError("Interstitial Ad are not ready. Please try again later.");
            manager.LoadAd(AdType.Interstitial);
        }
    }

    public void ShowRewarded(int id, int visualNumber)
    {
        rew_id = id;
        rev_VisualNumber = visualNumber;
        if (manager.IsReadyAd(AdType.Rewarded))
        {
            manager.ShowAd(AdType.Rewarded);
            manager.OnRewardedAdCompleted += RewardedSuccessful;
        }
        else
        {
            Debug.LogError("Rewarded Video Ad are not ready. Please try again later.");
            manager.LoadAd(AdType.Rewarded);
        }
    }

    public void ChangeAppReturnState()
    {
        if (isAppReturnEnable)
        {
            Debug.Log("App Return Status DISABLED");
            Debug.Log("App Return Button Text ENABLE");
            manager.SetAppReturnAdsEnabled(false);
            isAppReturnEnable = false;
        }
        else
        {
            Debug.Log("App Return Status ENABLE");
            Debug.Log("App Return Button Text DISABLED");
            manager.SetAppReturnAdsEnabled(true);
            isAppReturnEnable = true;
        }
    }

    private void OnRefreshStatus()
    {
        if (manager.IsReadyAd(AdType.Banner)) Debug.Log("Banner Ready");
        else Debug.Log("Banner Loading");

        if (manager.IsReadyAd(AdType.Interstitial)) Debug.Log("Interstitial Ready");
        else Debug.Log("Interstitial Loading");

        if (manager.IsReadyAd(AdType.Rewarded)) Debug.Log("Rewarded Ready");
        else Debug.Log("Rewarded Loading");
    }

    private void RewardedSuccessful()
    {
        manager.OnRewardedAdCompleted -= RewardedSuccessful;
        if (rew_id == 0)
        {
            //GM.AddTube();
            GM.spawnController.AddAdditionalTube();
        }
        else if (rew_id == 1)
        {
            int steps = PlayerPrefs.GetInt("Steps") + 5;

            PlayerPrefs.SetInt("Steps", steps);
            GM.UpdateTextSteps();
        }
        else if (rew_id == 2)
        {
            PlayerPrefs.SetInt("Tube", rev_VisualNumber);
            GM.SetSelectedTubes();
        }
        else if (rew_id == 3)
        {
            PlayerPrefs.SetInt("Theme", rev_VisualNumber);
            GM.SetSelectedBackground();
        }
        else if (rew_id == 4)
        {
            GM.UpdateAdTubeWatchCount();
        }
        else
        {
            Debug.LogError("Реклама з'явилась зашвидко, на телефоні такої проблеми немає.");
        }
    }

    private void SaveValues()
    {
        PlayerPrefs.SetString(nameof(_lastAdditionalTubeTime), JsonConvert.SerializeObject(_lastAdditionalTubeTime));
        PlayerPrefs.SetString(nameof(_lastInterstitialTime), JsonConvert.SerializeObject(_lastInterstitialTime));
        PlayerPrefs.SetString(nameof(_lastRewardTime), JsonConvert.SerializeObject(_lastRewardTime));
    }

    private void GetValues()
    {
        if (PlayerPrefs.HasKey(nameof(_lastAdditionalTubeTime)))
        {
            _lastAdditionalTubeTime = (DateTime)JsonConvert.DeserializeObject(PlayerPrefs.GetString(nameof(_lastAdditionalTubeTime)));
        }

        if (PlayerPrefs.HasKey(nameof(_lastInterstitialTime)))
        {
            _lastInterstitialTime = (DateTime)JsonConvert.DeserializeObject(PlayerPrefs.GetString(nameof(_lastInterstitialTime)));
        }

        if (PlayerPrefs.HasKey(nameof(_lastRewardTime)))
        {
            _lastRewardTime = (DateTime)JsonConvert.DeserializeObject(PlayerPrefs.GetString(nameof(_lastRewardTime)));
        }
    }

    private void OnDestroy()
    {
        SaveValues();
        manager.OnRewardedAdCompleted -= RewardedSuccessful;
        manager.OnInterstitialAdClosed -= OnInterstitialAdClosed;
    }
}
