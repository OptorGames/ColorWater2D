using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEvents;

public class LoadingProgress : MonoBehaviour
{
    [SerializeField] private InternetConnectionDataChecker internetConnectionDataChecker;
    [SerializeField] private Image _progress;

    private void Awake()
    {
        GameEvents.OnFirebaseLoaded += OnFirebaseLoaded;
    }

    void Start()
    {
        _progress.DOFillAmount(1f, 3f);
    }

    void OnFirebaseLoaded()
    {
        internetConnectionDataChecker.SetLevelOnStart(PlayerPrefs.GetInt("CurrentLevel_OFF", 0));
        SceneManager.LoadScene("Gameplay");
    }
}
