using DevToDev.Analytics;
using UnityEngine;

public class DTDInit : MonoBehaviour
{
    private void Start()
    {
        var config = new DTDAnalyticsConfiguration
        {
            ApplicationVersion = Application.version,
            LogLevel = DTDLogLevel.Debug,
            TrackingAvailability = DTDTrackingStatus.Enable,
            CurrentLevel = PlayerPrefs.GetInt("CurrentLevel") <= 0 ? 1 : PlayerPrefs.GetInt("CurrentLevel"),
#if UNITY_ANDROID
            UserId = SystemInfo.deviceUniqueIdentifier,
#elif UNITY_IOS
            UserId = "IOS_" + Random.Range(1000000000, 9999999999)
#endif
        };

#if UNITY_ANDROID
        DTDAnalytics.Initialize("d6ee0cdf-cc5c-0764-a2a2-9f7ecbba7c0c", config);
        DTDUserCard.Set("ad_tracker_id", "wwrTvr4uswcjnQGCFTawDT");
#elif UNITY_IOS
        DTDAnalytics.Initialize("d6ee0cdf-cc5c-0764-a2a2-9f7ecbba7c0c", config);
#elif UNITY_WEBGL
        DTDAnalytics.Initialize("d6ee0cdf-cc5c-0764-a2a2-9f7ecbba7c0c", config);
#elif UNITY_STANDALONE_WIN
        DTDAnalytics.Initialize("d6ee0cdf-cc5c-0764-a2a2-9f7ecbba7c0c", config);
#elif UNITY_STANDALONE_OSX
        DTDAnalytics.Initialize("d6ee0cdf-cc5c-0764-a2a2-9f7ecbba7c0c", config);
#elif UNITY_WSA
        DTDAnalytics.Initialize("d6ee0cdf-cc5c-0764-a2a2-9f7ecbba7c0c", config);
#endif
    }
}
