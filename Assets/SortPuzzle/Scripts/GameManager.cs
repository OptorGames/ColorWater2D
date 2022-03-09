using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(BuySteps), typeof(ButtonsManager))]
public class GameManager : MonoBehaviour
{
    public int EmptyTubes = 0;
    public int FullTubes = 0;

    public List<GameObject> tubesInGame;
    private GameObject selectedTube;
    public HudHandler HUD;
    private static bool isGameOver;
    private bool islevelStart;

    [Header("Links")]
    [SerializeField] private GameObject BuyStepsPanel;
    [SerializeField] private Button ButBackStep;
    [SerializeField] private Button AddTubeButton;

    [SerializeField] private GameObject Tube;

    [SerializeField] private LevelHandler levelHandler;

    [SerializeField] private Text LeveNumlText;
    [SerializeField] private Text textCountSteps;

    [SerializeField] public bool addingColor;

    public List<TubeController> tubeControllers = new List<TubeController>();
    private List<AllTubesInfo> savedTubes = new List<AllTubesInfo>();

    public AudioSource audioSource;
    [HideInInspector] public ButtonsManager buttonsManager;

    [Header("FromShop")]
    public Sprite[] tubes;
    public Sprite[] tubesMasks;

    [SerializeField] private Sprite[] themes;

    [SerializeField] private Image background;

    [SerializeField] private ParticleSystem confetti;

    private bool needUnPause = false;

    private void Start()
    {
        buttonsManager = GetComponent<ButtonsManager>();

        Time.timeScale = 1;
        islevelStart = true;
        StartCoroutine(LevelInitialized());
        EmptyTubes = 0;
        FullTubes = 0;
        selectedTube = null;
        isGameOver = false;

        SetSelectedBackground();
        UpdateTextSteps();

        int curr_level = PlayerPrefs.GetInt("CurrentLevel") + 1;
        LeveNumlText.text = "Level " + curr_level.ToString();

        if (!PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.SetFloat("Volume", 1f);
        else
            audioSource.volume = PlayerPrefs.GetFloat("Volume");

        if (!PlayerPrefs.HasKey("Vibrate"))
            PlayerPrefs.SetInt("Vibrate", 1);

        buttonsManager.LoadSettings();
    }

    private void Update()
    {
        if (addingColor && ButBackStep.interactable)
        {
            ButBackStep.interactable = false;
        }
        else if (!addingColor && !ButBackStep.interactable)
        {
            ButBackStep.interactable = true;
        }

        if (Time.timeScale <= 0.1f)
        {
            if(confetti.gameObject.activeSelf)
                confetti.gameObject.SetActive(false);

            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                needUnPause = true;
            }
        }
        else
        {
            if(!confetti.gameObject.activeSelf)
                confetti.gameObject.SetActive(true);

            if (needUnPause)
            {
                audioSource.UnPause();
                needUnPause = false;
            }
        }
    }

    public void UpdateTextSteps()
    {
        textCountSteps.text = PlayerPrefs.GetInt("Steps").ToString();
    }

    public void StepsPanelButton()
    {
        BuyStepsPanel.SetActive(!BuyStepsPanel.activeSelf);
    }

    public void AddTube()
    {
        Tube.SetActive(true);
        AddTubeButton.interactable = false;
    }

    public void UpdateEF_Info()
    {
        EmptyTubes = 0;
        FullTubes = 0;

        for (int i = 0; i < tubeControllers.Count; i++)
        {
            if (tubeControllers[i].isEmpty)
                AddEmpty();
            else if (tubeControllers[i].isFull)
                AddFull(tubeControllers[i].transform.position, false);
        }
    }

    public void PlusCoins()
    {
        HUD.ads.ShowRewarded(2);
    }

    public void SetSelectedBackground()
    {
        background.sprite = themes[PlayerPrefs.GetInt("Theme")];
    }

    public void SetSelectedTubes()
    {
        for (int i = 0; i < tubeControllers.Count; i++)
        {
            tubeControllers[i].tubeSR.sprite = tubes[PlayerPrefs.GetInt("Tube")];
            tubeControllers[i].GetComponentInChildren<SpriteMask>().sprite = tubesMasks[PlayerPrefs.GetInt("Tube")];
        }
    }

    public void TubeClicked(GameObject TubeObj)
    {
        if (Time.timeScale == 0)
            return;

        TubeController tube = TubeObj.GetComponent<TubeController>();

        if (selectedTube == null && !tube.isBusy)
        {
            selectedTube = TubeObj;

            if (tube.currColors == 0)
                UpdateEF_Info();

            for (int i = 0; i < tube.ColorObjects_Renderers.Length; i++)
            {
                tube.ColorObjects_Renderers[i].sortingOrder = 1;
            }

            tube.tubeSR.sortingOrder = 1;
            tube.sorting.sortingOrder = 1;
            TubeObj.transform.position += new Vector3(0, 2, 0);
        }
        else if (selectedTube == TubeObj)
        {
            TubeObj.transform.position -= new Vector3(0, 2, 0);

            for (int i = 0; i < tube.ColorObjects_Renderers.Length; i++)
            {
                tube.ColorObjects_Renderers[i].sortingOrder = 0;
            }

            tube.tubeSR.sortingOrder = 0;
            tube.sorting.sortingOrder = 0;
            selectedTube = null;
        }
        else if (!addingColor)
        {
            TubeController tubeSelected = selectedTube.GetComponent<TubeController>();

            tubeSelected.PourColor(TubeObj);

            for (int i = 0; i < tubeSelected.ColorObjects_Renderers.Length; i++)
            {
                tubeSelected.ColorObjects_Renderers[i].sortingOrder = 0;
            }

            tubeSelected.tubeSR.sortingOrder = 0;
            tube.sorting.sortingOrder = 0;
            selectedTube = null;
        }
    }

    private IEnumerator LevelInitialized()
    {
        yield return new WaitForSeconds(2);
        islevelStart = false;
    }

    public void SaveTubes()
    {
        AllTubesInfo tubes_info = new AllTubesInfo();
        for (int i = 0; i < tubesInGame.Count; i++)
        {
            TubeInfo tubeInfo = new TubeInfo();
            tubeInfo.currColors = tubeControllers[i].currColors;
            tubeInfo.isEmpty = tubeControllers[i].isEmpty;
            tubeInfo.isFull = tubeControllers[i].isFull;

            tubeInfo.colors[0] = tubeControllers[i].ColorObjects_Renderers[0].color;
            tubeInfo.colors[1] = tubeControllers[i].ColorObjects_Renderers[1].color;
            tubeInfo.colors[2] = tubeControllers[i].ColorObjects_Renderers[2].color;
            tubeInfo.colors[3] = tubeControllers[i].ColorObjects_Renderers[3].color;

            tubeInfo.scale = tubeControllers[i].transform.localScale;

            tubes_info.tubes.Add(tubeInfo);
        }

        tubes_info.empty = EmptyTubes;
        tubes_info.full = FullTubes;
        savedTubes.Add(tubes_info);
    }

    public void StepBackForTubes()
    {
        int count = PlayerPrefs.GetInt("Steps");
        if (count <= 0)
            return;

        if (savedTubes.Count >= 1)
        {
            selectedTube = null;
            for (int i = 0; i < tubeControllers.Count; i++)
            {
                tubeControllers[i].transform.position = tubeControllers[i].Pos;
            }

            for (int i = 0; i < tubeControllers.Count; i++)
            {
                if (tubeControllers.Count > savedTubes[savedTubes.Count - 1].tubes.Count && i == tubeControllers.Count - 1)
                    break;

                tubeControllers[i].currColors = savedTubes[savedTubes.Count - 1].tubes[i].currColors;
                tubeControllers[i].isEmpty = savedTubes[savedTubes.Count - 1].tubes[i].isEmpty;
                tubeControllers[i].isFull = savedTubes[savedTubes.Count - 1].tubes[i].isFull;

                tubeControllers[i].transform.position = tubeControllers[i].Pos;
                tubeControllers[i].transform.localScale = savedTubes[savedTubes.Count - 1].tubes[i].scale;

                tubeControllers[i].NextTube = null;

                for (int a = 0; a < tubeControllers[i].ColorObjects.Length; a++)
                {
                    if (a <= savedTubes[savedTubes.Count - 1].tubes[i].currColors - 1)
                        tubeControllers[i].ColorObjects[a].SetActive(true);
                    else tubeControllers[i].ColorObjects[a].SetActive(false);

                    tubeControllers[i].ColorObjects[a].transform.localScale = new Vector2(1.8375f, 0.25f);
                    tubeControllers[i].ColorObjects_Renderers[a].color = savedTubes[savedTubes.Count - 1].tubes[i].colors[a];
                    tubeControllers[i].colorsInTube[a] = savedTubes[savedTubes.Count - 1].tubes[i].colors[a];
                }
            }

            EmptyTubes = savedTubes[savedTubes.Count - 1].empty;
            FullTubes = savedTubes[savedTubes.Count - 1].full;

            savedTubes.RemoveAt(savedTubes.Count - 1);

            count--;
            PlayerPrefs.SetInt("Steps", count);
            UpdateTextSteps();
        }
    }

    public void AddEmpty()
    {
        EmptyTubes++;
        if (!islevelStart && FullTubes + EmptyTubes == tubesInGame.Count && !isGameOver)
        {
            levelHandler.Levels[PlayerPrefs.GetInt("CurrentGameLevel")].SetActive(false);
            confetti.gameObject.SetActive(false);

            isGameOver = true;
            int currGameLevel = PlayerPrefs.GetInt("CurrentGameLevel") + 1;

            if (currGameLevel >= levelHandler.Levels.Length)
                currGameLevel = 40;

            PlayerPrefs.SetInt("CurrentGameLevel", currGameLevel);

            HUD.WinGame();
        }
    }

    private IEnumerator VibrateTurnOn(Vector3 pos)
    {
        yield return new WaitForSeconds(0.5f);

        if (PlayerPrefs.GetInt("Vibrate") == 1 && !isGameOver)
        {
            confetti.transform.position = pos;
            confetti.Play();

            Handheld.Vibrate();
            Debug.Log("Vibrate");
        }
    }

    public void RemoveEmpty()
    {
        EmptyTubes--;
    }

    public void AddFull(Vector3 pos, bool isPlay)
    {
        FullTubes++;

        if (isPlay)
            StartCoroutine(VibrateTurnOn(pos));

        if (!islevelStart && FullTubes + EmptyTubes == tubesInGame.Count && !isGameOver)
        {
            levelHandler.Levels[PlayerPrefs.GetInt("CurrentGameLevel")].SetActive(false);
            confetti.gameObject.SetActive(false);

            isGameOver = true;
            int currGameLevel = PlayerPrefs.GetInt("CurrentGameLevel") + 1;

            if (currGameLevel >= levelHandler.Levels.Length)
                currGameLevel = 40;

            PlayerPrefs.SetInt("CurrentGameLevel", currGameLevel);

            HUD.WinGame();
        }
    }

    public void RemoveFull()
    {
        FullTubes--;
    }
}

public class AllTubesInfo
{
    public List<TubeInfo> tubes = new List<TubeInfo>();
    public int full;
    public int empty;
}

public class TubeInfo
{
    public bool isFull;
    public bool isEmpty;
    public int currColors;

    public Vector3 scale;

    public Color[] colors = new Color[4];
}