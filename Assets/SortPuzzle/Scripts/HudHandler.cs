using UnityEngine;
using UnityEngine.UI;
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

        if(PlayerPrefs.GetInt("NoAds") != 1)
        {
            int level = PlayerPrefs.GetInt("CurrentLevel");

            if (level % 10 == 0)
                ads.ShowRewarded(10);
            else if(level % 2 == 0)
                ads.ShowInterstitial();
        }
    }

    public void Restart()
    {
        //if (PlayerPrefs.GetInt("NoAds") != 1)
        //    ads.ShowInterstitial();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoseGame()
    {
        IsGamePaused = true;
        LoseMenu.SetActive(true);
        MainInterface.SetActive(false);
    }
}
