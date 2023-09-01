using DevToDev.Analytics;
//using Firebase.Analytics;
using ForTutorial;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudHandler : MonoBehaviour
{
    public Camera Cam;
    public GameObject PauseMenu, LoseMenu, WinMenu;
    public static bool IsGamePaused, isObserveMode;

    public Ads ads;

    [HideInInspector] public GameObject Pour;

    private int old_value;

    [SerializeField] private GameObject noInternetPopup;
    [SerializeField] private GameObject blockLevelPopup;
    [Header("Links")] [SerializeField] private GameObject MainInterface;
    [SerializeField] private TutorialController _tutorialController;
    [SerializeField] private GameObject _stand;
    [SerializeField] private GameObject _confetti;
    [HideInInspector] public GameObject Flasks;

    private void Start()
    {
        Cam = Camera.main;
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        PauseMenu.SetActive(true);

        Time.timeScale = 0;
        MainInterface.SetActive(false);
    }

    public void WinGame()
    {
        StartCoroutine(WinGameCoroutine());
    }

    public IEnumerator WinGameCoroutine()
    {
        yield return new WaitForSeconds(0.5f);

        IsGamePaused = true;

        UpdateDifficultyLevel();

        Flasks.SetActive(false);
        _stand.SetActive(false);
        _confetti.SetActive(false);

        if (Pour != null)
        {
            Pour.SetActive(false);
        }

        WinMenu.SetActive(true);
        MainInterface.SetActive(false);
        if (!PlayerPrefs.HasKey("FirstStart"))
        {
            foreach (var tutorialArrow in _tutorialController.TutorialArrows)
            {
                tutorialArrow.SetActive(false);
            }
            PlayerPrefs.SetInt("CurrentLevel_OFF", 0);
            PlayerPrefs.SetInt("FirstStart", 1);
            Analytics.AnalyticsAdapter.TutorialCompleted();
        }

        //if (old_value == 1) FirebaseAnalytics.LogEvent("level_1");
        //if (old_value == 1 || old_value == 3 || old_value == 5 || old_value == 7 || old_value == 10 ||
        //    old_value == 15 || old_value == 20 || old_value == 50 || old_value == 100 || old_value == 150)
        Analytics.AnalyticsAdapter.LevelCompleted(old_value, true);

        PlayerPrefs.SetInt("Steps", 5);
        ads.OnLevelComplete(old_value);
    }

    private void UpdateDifficultyLevel()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty_", 0);
        switch (difficulty)
        {
            case 0:
                old_value = PlayerPrefs.GetInt("CurrentLevel_OFF") + 1;
                PlayerPrefs.SetInt("CurrentLevel_OFF", old_value);
                break;
            case 1:
                old_value = PlayerPrefs.GetInt("CurrentLevel_Easy") + 1;
                PlayerPrefs.SetInt("CurrentLevel_Easy", old_value);
                break;
            case 2:
                old_value = PlayerPrefs.GetInt("CurrentLevel_Medium") + 1;
                PlayerPrefs.SetInt("CurrentLevel_Medium", old_value);
                break;
            case 3:
                old_value = PlayerPrefs.GetInt("CurrentLevel_Hard") + 1;
                PlayerPrefs.SetInt("CurrentLevel_Hard", old_value);
                break;

        }
    }

    public void Restart()
    {
        int new_count = PlayerPrefs.GetInt("RestartCount") + 1;
        PlayerPrefs.SetInt("RestartCount", new_count);
        PlayerPrefs.SetInt("Steps", 5);
        ads.OnLevelRestart(5);
    }

    public void RestartWithoutAds() => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    public void LoseGame()
    {
        IsGamePaused = true;
        LoseMenu.SetActive(true);
        MainInterface.SetActive(false);
    }

    public void ShowNoInternetPopup()
    {
        noInternetPopup.SetActive(true);
    }

    public void ShowBlockLevelPopup()
    {
        blockLevelPopup.SetActive(true);
    }
}