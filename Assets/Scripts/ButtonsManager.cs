using System.Security.Policy;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(GameManager))]
public class ButtonsManager : MonoBehaviour
{
    public HudHandler HUD;
    private GameManager _gameManager;

    [SerializeField] private TubesThemesController _tubesThemesController;

    [SerializeField] private string feedback_url;

    [SerializeField] private Sprite checkTrue;
    [SerializeField] private Sprite checkFalse;

    [Header("Sound")] [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image soundImg;

    [Header("Vibrate")] [SerializeField] private Image vibrateImg;

    [Header("Links")] [SerializeField] GameObject Main_Window;
    [SerializeField] GameObject Shop_Window;
    [SerializeField] GameObject Tubes_Window;
    [SerializeField] GameObject Backgrounds_Window;

    [SerializeField] Text TextTT;

    [SerializeField] Text CountText;

    [SerializeField] Button[] buttons_tubes;
    [SerializeField] ThemeButton[] buttons_themes;

    [SerializeField] private Button _menu;
    [SerializeField] private Button _additionalTube;
    [SerializeField] private GameObject stepBackButtonNormalContent;
    [SerializeField] private GameObject stepBackButtonAdsContent;
    [SerializeField] private Image stepBackImage;
    [SerializeField] private Sprite adsSpriteForStepBtn;
    [SerializeField] private Sprite normalSpriteForStepBtn;

    private void Start()
    {
        Tubes_Window.SetActive(true);
        Backgrounds_Window.SetActive(false);
        TextTT.text = "Tube";

        UpdateCountState();
        _gameManager = GetComponent<GameManager>();
        foreach (var buttonsTube in buttons_tubes)
        {
            buttonsTube.interactable = true;
        }

        buttons_tubes[PlayerPrefs.GetInt("Tube")].interactable = false;
        for (int i = 0; i < buttons_themes.Length; i++)
        {
            buttons_themes[i].SetInteractable(true);
            buttons_themes[i].Init(_gameManager.Themes[i], i, TrySelectTheme);
        }

        buttons_themes[PlayerPrefs.GetInt("Theme")].SetInteractable(false);
    }

    public void DisableMenuButton()
    {
        _menu.interactable = false;
    }

    public void EnableMenuButton()
    {
        _menu.interactable = true;
    }

    public void UpdateCountState()
    {
        CountText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void TrySelectTube(int num)
    {
        foreach (var buttonsTube in buttons_tubes)
        {
            buttonsTube.interactable = true;
        }

        HUD.ads.OnChangeTubeTheme(num);

        PlayerPrefs.SetInt("Tube", num);
        _tubesThemesController.SetTheme();
    }

    public void TrySelectTheme(int num)
    {
        foreach (var buttonsTheme in buttons_themes)
        {
            buttonsTheme.SetInteractable(true);
        }

        if (PlayerPrefs.GetInt("UnlockedThemes") != 9)
        {
            HUD.ads.OnChangeBackgroundTheme(num);
        }
        else if (PlayerPrefs.GetInt("UnlockedThemes") == 9)
        {
            PlayerPrefs.SetInt("Theme", num);
            _gameManager.SetSelectedBackground();
        }
    }

    public void ClearAllKeys()
    {
        PlayerPrefs.DeleteAll();
    }

    public void FeedbackButton()
    {
        Application.OpenURL(feedback_url);
    }

    public void ShopButton()
    {
        Shop_Window.SetActive(!Shop_Window.activeSelf);
        Main_Window.SetActive(!Main_Window.activeSelf);
    }

    public void ShowTubesButton()
    {
        Tubes_Window.SetActive(true);
        Backgrounds_Window.SetActive(false);
        TextTT.text = "Tube";
    }

    public void ShowBackgroundsButton()
    {
        Tubes_Window.SetActive(false);
        Backgrounds_Window.SetActive(true);
        TextTT.text = "Theme";
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.GetFloat("Volume") == 0f)
            soundImg.sprite = checkFalse;
        else
            soundImg.sprite = checkTrue;

        if (PlayerPrefs.GetInt("Vibrate") == 0)
            vibrateImg.sprite = checkFalse;
        else
            vibrateImg.sprite = checkTrue;
    }

    public void SoundButton()
    {
        if (PlayerPrefs.GetFloat("Volume") == 0f)
        {
            PlayerPrefs.SetFloat("Volume", 1f);
            soundImg.sprite = checkTrue;
            audioSource.volume = 1;
        }
        else
        {
            PlayerPrefs.SetFloat("Volume", 0f);
            soundImg.sprite = checkFalse;
            audioSource.volume = 0;
        }
    }

    public void VibrateButton()
    {
        if (PlayerPrefs.GetInt("Vibrate") == 0)
        {
            PlayerPrefs.SetInt("Vibrate", 1);
            vibrateImg.sprite = checkTrue;
        }
        else
        {
            PlayerPrefs.SetInt("Vibrate", 0);
            vibrateImg.sprite = checkFalse;
        }
    }

    public void AddBackStepsButton()
    {
        int count = PlayerPrefs.GetInt("Steps");
        count += 5;
        PlayerPrefs.SetInt("Steps", count);
        _gameManager.UpdateTextSteps();
    }

    public void DisableAdditionalTubeButton()
    {
        _additionalTube.interactable = false;
    }

    public void SetAdsSpriteForStepBackButton()
    {
        stepBackButtonNormalContent.gameObject.SetActive(false);
        stepBackButtonAdsContent.gameObject.SetActive(true);
    }

    public void SetNormalSpriteForStepBackButton()
    {
        stepBackButtonNormalContent.gameObject.SetActive(true);
        stepBackButtonAdsContent.gameObject.SetActive(false);
    }
}