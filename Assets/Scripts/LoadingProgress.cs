using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static GameEvents;

public class LoadingProgress : MonoBehaviour
{
    [SerializeField] private Image _progress;

    private void Awake()
    {
        GameEvents.OnFirebaseLoaded += OnFirebaseLoaded;
    }

    void Start()
    {
        _progress.DOFillAmount(0.8f, 2f);
    }

    void OnFirebaseLoaded()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
