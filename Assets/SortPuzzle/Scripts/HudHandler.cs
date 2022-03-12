using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudHandler : MonoBehaviour
{
    public Camera Cam;
    public GameObject PauseMenu, LoseMenu, WinMenu;
    public static bool IsGamePaused, isObserveMode;

    public Ads ads;
    //private JsonSaver json;

    [Header("Links")]
    [SerializeField] private Button ButBuy5Steps;
    [SerializeField] private Button AddTubeButton;

    [SerializeField] private GameObject MainInterface;

    private void Start()
    {
        Cam = Camera.main;
        //json = GetComponent<JsonSaver>();

        ButBuy5Steps.onClick.AddListener(() => ads.ShowRewarded(1));
        AddTubeButton.onClick.AddListener(() => ads.ShowRewarded(0));
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
            Destroy(tubes[i].gameObject);

        //if (PlayerPrefs.GetInt("SelectedLevel") == 1)
        //{
        //    if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress1 < 100)
        //        dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress1 += 10;
        //}
        //else if (PlayerPrefs.GetInt("SelectedLevel") == 2)
        //{
        //    if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress2 < 100)
        //        dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress2 += 10;
        //}
        //else if (PlayerPrefs.GetInt("SelectedLevel") == 3)
        //{
        //    if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress3 < 100)
        //        dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress3 += 10;
        //}
        //else if (PlayerPrefs.GetInt("SelectedLevel") == 4)
        //{
        //    if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress4 < 100)
        //        dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress4 += 10;
        //    else dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].isCompleted = true;
        //}

        //json.Save(dontDestroy.collections);

        WinMenu.SetActive(true);
        MainInterface.SetActive(false);

        if(PlayerPrefs.GetInt("NoAds") != 1)
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
