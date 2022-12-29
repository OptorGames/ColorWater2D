using UnityEngine;


public class FirebaseService : MonoBehaviour
{
    
    //[SerializeField] private string _remoteConfigValue = "TestData";

    private static FirebaseService _instance;

    //public static string RemoteConfigValue => _instance._remoteConfigValue;
    private Firebase.FirebaseApp app;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
    }

    async void Start()
    {
        Debug.LogError("Init firebase");
        await Firebase.FirebaseApp.CheckAndFixDependenciesAsync()
        .ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = Firebase.FirebaseApp.DefaultInstance;

                //InitializeFirebase();

                Debug.LogError("All okey firebase");

                Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);

            }

            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }

        });

        GameEvents.RaiseOnFirebaseLoaded();

    }


    private void InitializeFirebase()
    {
        //Debug.Log("FirebaseManager : Enabling data collection.");

        //FetchDataAsync();

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            Firebase.Analytics.FirebaseAnalytics.LogEvent(Firebase.Analytics.FirebaseAnalytics.EventLogin);
        });

        //Debug.Log("Already initialized");
       // EventAggregatorSimple.Post<FirebaseAnalyticInitializeEvent>(this, new FirebaseAnalyticInitializeEvent());
    }

    //public Task FetchDataAsync()
    //{
    //    //Debug.Log("Fetching data...");
    //    System.Threading.Tasks.Task fetchTask =
    //    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
    //        TimeSpan.Zero);
    //    return fetchTask.ContinueWithOnMainThread(FetchComplete);
    //}

    //void FetchComplete(Task fetchTask)
    //{
    //    //if (fetchTask.IsCanceled)
    //    //{
    //    //    Debug.Log("Fetch canceled.");
    //    //}
    //    //else if (fetchTask.IsFaulted)
    //    //{
    //    //    Debug.Log("Fetch encountered an error.");
    //    //}
    //    //else if (fetchTask.IsCompleted)
    //    //{
    //    //    Debug.Log("Fetch completed successfully!");
    //    //}

    //    var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
    //    switch (info.LastFetchStatus)
    //    {
    //        case Firebase.RemoteConfig.LastFetchStatus.Success:
    //            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
    //            .ContinueWithOnMainThread(task => {
    //                EventAggregatorSimple.Post(this, new FirebaseRemoteConfigInitializeEvent());
    //                //Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
    //                //               info.FetchTime));
    //            });

    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Failure:
    //            switch (info.LastFetchFailureReason)
    //            {
    //                case Firebase.RemoteConfig.FetchFailureReason.Error:
    //                    //Debug.Log("Fetch failed for unknown reason");
    //                    break;
    //                case Firebase.RemoteConfig.FetchFailureReason.Throttled:
    //                    //Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
    //                    break;
    //            }
    //            break;
    //        case Firebase.RemoteConfig.LastFetchStatus.Pending:
    //            //Debug.Log("Latest Fetch call still pending.");
    //            break;
    //    }
    //}
}