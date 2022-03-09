using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GameManager))]
public class ButtonsManager : MonoBehaviour
{
    [SerializeField] private string feedback_url;

    [SerializeField] private Sprite checkTrue;
    [SerializeField] private Sprite checkFalse;

    [Header("Sound")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image soundImg;

    [Header("Vibrate")]
    [SerializeField] private Image vibrateImg;

    [Header("Links")]
    [SerializeField] GameObject Main_Window;
    [SerializeField] GameObject Shop_Window;
    [SerializeField] GameObject Tubes_Window;
    [SerializeField] GameObject Backgrounds_Window;

    [SerializeField] Text TextTT;

    [SerializeField] Text CountText;

    [SerializeField] Button[] buttons_tubes;
    [SerializeField] Button[] buttons_themes;

    private GameManager GM;

    private void Start()
    {
        Tubes_Window.SetActive(true);
        Backgrounds_Window.SetActive(false);
        TextTT.text = "Tube";

        UpdateCountState();

        GM = GetComponent<GameManager>();
    }

    public void UpdateCountState()
    {
        CountText.text = PlayerPrefs.GetInt("Coins").ToString();
    }

    public void SelectTube(int num)
    {
        PlayerPrefs.SetInt("Tube", num);
        GM.SetSelectedTubes();
    }

    public void SelectTheme(int num)
    {
        PlayerPrefs.SetInt("Theme", num);
        GM.SetSelectedBackground();
    }

    public void UnlockButtons()
    {
        for (int i = 0; i <= PlayerPrefs.GetInt("UnlockedTubes"); i++)
        {
            buttons_tubes[i].interactable = true;
        }

        for (int i = 0; i <= PlayerPrefs.GetInt("UnlockedThemes"); i++)
        {
            buttons_themes[i].interactable = true;
        }

        UpdateCountState();
    }

    public void BuyForCoins()
    {
        if (PlayerPrefs.GetInt("Coins") < 5)
            return;

        if (Tubes_Window.activeSelf && PlayerPrefs.GetInt("UnlockedTubes") < 2)
        {
            int count = PlayerPrefs.GetInt("UnlockedTubes") + 1;
            PlayerPrefs.SetInt("UnlockedTubes", count);
            
            int coins = PlayerPrefs.GetInt("Coins") - 5;
            PlayerPrefs.SetInt("Coins", coins);
            UpdateCountState();
        }
        else if (Backgrounds_Window.activeSelf && PlayerPrefs.GetInt("UnlockedThemes") < 9)
        {
            int count = PlayerPrefs.GetInt("UnlockedThemes") + 1;
            PlayerPrefs.SetInt("UnlockedThemes", count);

            int coins = PlayerPrefs.GetInt("Coins") - 5;
            PlayerPrefs.SetInt("Coins", coins);
            UpdateCountState();
        }
        UnlockButtons();
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

        UnlockButtons();
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


}
