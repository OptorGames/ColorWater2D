using UnityEngine;
using UnityEngine.UI;

public class HudHandler : MonoBehaviour
{
    public Camera Cam;
    public GameObject PauseMenu, LoseMenu, WinMenu;
    public static bool IsGamePaused, isObserveMode;

    [HideInInspector] public DontDestroyOnLoad dontDestroy;
    private JsonSaver json;

    [Header("Links")]
    [SerializeField] private Button ButBuy5Steps;
    [SerializeField] private Button AddTubeButton;

    private void Awake()
    {
        dontDestroy = FindObjectOfType<DontDestroyOnLoad>();
    }

    private void Start()
    {
        Cam = Camera.main;
        json = GetComponent<JsonSaver>();

        ButBuy5Steps.onClick.AddListener(() => dontDestroy.ads.ShowRewarded(1));
        AddTubeButton.onClick.AddListener(() => dontDestroy.ads.ShowRewarded(0));
    }

    public void PauseGame()
    {
        IsGamePaused = true;
        PauseMenu.SetActive(true);

        Time.timeScale = 0;
        gameObject.SetActive(false);
    }

    public void WinGame()
    {
        IsGamePaused = true;

        int old_value = PlayerPrefs.GetInt("CurrentLevel") + 1;
        PlayerPrefs.SetInt("CurrentLevel", old_value);

        if (PlayerPrefs.GetInt("SelectedLevel") == 1)
        {
            if(dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress1 < 100)
                dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress1 += 10;
        }
        else if(PlayerPrefs.GetInt("SelectedLevel") == 2)
        {
            if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress2 < 100)
                dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress2 += 10;
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") == 3)
        {
            if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress3 < 100)
                dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress3 += 10;
        }
        else if (PlayerPrefs.GetInt("SelectedLevel") == 4)
        {
            if (dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress4 < 100)
                dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].image_progress4 += 10;
            else dontDestroy.collections[PlayerPrefs.GetInt("SelectedCollection")].isCompleted = true;
        }

        json.Save(dontDestroy.collections);

        WinMenu.SetActive(true);
        gameObject.SetActive(false);

        if(PlayerPrefs.GetInt("NoAds") != 1)
            dontDestroy.ads.ShowInterstitial();
    }

    public void LoseGame()
    {
        IsGamePaused = true;
        LoseMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
