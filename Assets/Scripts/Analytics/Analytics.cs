using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analytics : MonoBehaviour
{
    private static IAnalyticsAdapter _analyticsAdapter;
    public static IAnalyticsAdapter AnalyticsAdapter => _analyticsAdapter;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        var firebaseAnalyticsAdapter = new FirebaseAnalyticsAdapter();

        _analyticsAdapter = new DTDAnalyticsAdapter(firebaseAnalyticsAdapter);
    }
}
