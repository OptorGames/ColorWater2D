using CAS;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void Start()
    {
        MobileAds.settings.userConsent = userConsent;
        MobileAds.settings.userCCPAStatus = userCCPAStatus;

        MobileAds.settings.isExecuteEventsOnUnityThread = true;

        manager = MobileAds.BuildManager().Initialize();

        manager.OnRewardedAdCompleted += RewardedSuccessful;

        InvokeRepeating("OnRefreshStatus", 1.0f, 1.0f);

        //bannerView = manager.GetAdView(AdSize.Banner);
        //ShowBanner();
        rew_id = 10;
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
        if(rew_id == 0)
        {
            GM.AddTube();
        }
        else if(rew_id == 1)
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
        else if(rew_id == 3)
        {
            PlayerPrefs.SetInt("Theme", rev_VisualNumber);
            GM.SetSelectedBackground();
        }
        else
        {
            Debug.LogError("?????????????? ??'?????????????? ????????????????, ???? ???????????????? ?????????? ???????????????? ??????????.");
        }
    }
}
