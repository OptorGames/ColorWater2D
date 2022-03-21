using DevToDev.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HudHandler : MonoBehaviour
{
    public Camera Cam;
    public GameObject PauseMenu, LoseMenu, WinMenu;
    public static bool IsGamePaused, isObserveMode;

    public Ads ads;

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

        int old_value = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("CurrentLevel", old_value);

        GameObject[] tubes = GameObject.FindGameObjectsWithTag("Tube");

        for (int i = 0; i < tubes.Length; i++)
            tubes[i].gameObject.SetActive(false);

        WinMenu.SetActive(true);
        MainInterface.SetActive(false);

        if (old_value == 1 || old_value == 3 || old_value == 5 || old_value == 7 || old_value == 10 ||
            old_value == 15 || old_value == 20 || old_value == 50 || old_value == 100 || old_value == 150)
            DTDAnalytics.CustomEvent(eventName: "Level_" + old_value.ToString());

        PlayerPrefs.SetInt("Steps", 5);

        if (PlayerPrefs.GetInt("NoAds") != 1)
            ads.ShowInterstitial();
    }

    public void Restart()
    {
        if (PlayerPrefs.GetInt("NoAds") != 1)
            ads.ShowInterstitial();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseGame()
    {
        IsGamePaused = true;
        LoseMenu.SetActive(true);
        MainInterface.SetActive(false);
    }
}
