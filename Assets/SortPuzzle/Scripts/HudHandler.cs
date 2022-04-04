using DevToDev.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudHandler : MonoBehaviour
{
    public Camera Cam;
    public GameObject PauseMenu, LoseMenu, WinMenu;
    public static bool IsGamePaused, isObserveMode;

    public Ads ads;
    private int old_value;

    [Header("Links")]
    [SerializeField] private GameObject MainInterface;

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
        IsGamePaused = true;

        UpdateDifficultyLevel();
        GameObject[] tubes = GameObject.FindGameObjectsWithTag("Tube");

        for (int i = 0; i < tubes.Length; i++)
            tubes[i].gameObject.SetActive(false);

        WinMenu.SetActive(true);
        MainInterface.SetActive(false);

        if (old_value == 1 || old_value == 3 || old_value == 5 || old_value == 7 || old_value == 10 ||
            old_value == 15 || old_value == 20 || old_value == 50 || old_value == 100 || old_value == 150)
            DTDAnalytics.CustomEvent(eventName: "Level_" + old_value.ToString());

        PlayerPrefs.SetInt("Steps", 5);

        
        if (old_value > 2 && PlayerPrefs.GetInt("NoAds") != 1)
            ads.ShowInterstitial();
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
            case 4:
                old_value = PlayerPrefs.GetInt("CurrentLevel_Extreme") + 1;
                PlayerPrefs.SetInt("CurrentLevel_Extreme", old_value);
                break;
        }
    }

    public void Restart()
    {
        if (PlayerPrefs.GetInt("RestartCount") > 5)
        {
            if (PlayerPrefs.GetInt("NoAds") != 1)
                ads.ShowInterstitial();
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        int new_count = PlayerPrefs.GetInt("RestartCount") + 1;
        PlayerPrefs.SetInt("RestartCount", new_count);
    }

    public void LoseGame()
    {
        IsGamePaused = true;
        LoseMenu.SetActive(true);
        MainInterface.SetActive(false);
    }
}
