using Google.Play.Review;
using System.Collections;
using System.Collections.Generic;
using ForTutorial;
using UnityEngine;
using UnityEngine.UI;

//[RequireComponent(typeof(BuySteps), typeof(ButtonsManager))]
public class GameManager : IGameManager
{
    public int EmptyTubes = 0;
    public int FullTubes = 0;

    //public List<GameObject> tubesInGame;
    protected GameObject selectedTube;
    public HudHandler HUD;
    public ISpawnController spawnController;
    protected static bool isGameOver;
    protected bool islevelStart;
    protected int difficulty = 0;
    protected int curr_level;

    [SerializeField] protected TutorialController _tutorialController;

    [Header("Links")] [SerializeField] protected GameObject BuyStepsPanel;
    [SerializeField] protected Button ButBackStep;
    [SerializeField] protected Button AddTubeButton;

    [SerializeField] protected GameObject NoAdsButton;
    [SerializeField] protected Button NoAdsButtonInPauseMenu;
    [SerializeField] protected Button UnlockAllButton;

    [SerializeField] public GameObject Tube;

    [SerializeField] protected LevelHandler levelHandler;

    [SerializeField] protected Text LeveNumlText;
    [SerializeField] protected Text textCountSteps;
    [SerializeField] protected Text textDifficulty;

    //[SerializeField] public bool addingColor;

    //public List<TubeController> tubeControllers = new List<TubeController>();
    protected List<AllTubesInfo> savedTubes = new List<AllTubesInfo>();

    //public AudioSource audioSource;
    [HideInInspector] public ButtonsManager buttonsManager;

    [Header("FromShop")] public Sprite[] tubes;
    public Sprite[] tubesMasks;

    [SerializeField] protected Sprite[] themes;

    [SerializeField] protected Image background;

    [SerializeField] protected ParticleSystem confetti;
    protected bool needUnPause = false;

    // Create instance of ReviewManager
    protected ReviewManager _reviewManager;
    protected PlayReviewInfo _playReviewInfo;

    public override void SetLvl(int numberOffLvl)
    {
        PlayerPrefs.DeleteKey("UnlockedThemes");
        PlayerPrefs.DeleteKey("UnlockedTubes");
        PlayerPrefs.SetInt("CurrentLevel_OFF", numberOffLvl);
        PlayerPrefs.DeleteKey("NoAds");
    }

    private void Start()
    {
        buttonsManager = GetComponent<ButtonsManager>();
        PlayerPrefs.DeleteKey("UnlockedAll");

        Time.timeScale = 1;
        islevelStart = true;
        StartCoroutine(LevelInitialized());
        EmptyTubes = 0;
        FullTubes = 0;
        selectedTube = null;
        isGameOver = false;
        difficulty = spawnController.GetDifficulty();
        SetTextDifficulty();
        if (PlayerPrefs.HasKey("FirstStart"))
        {
            _tutorialController.enabled = false;
            LoadDifficultyLevel();
        }
        else
        {
            _tutorialController.enabled = true;
        }

        SetSelectedBackground();
        UpdateTextSteps();
        spawnController.level = curr_level;


        spawnController.SpawnObject();
        LeveNumlText.text = "Level " + curr_level.ToString();

        if (!PlayerPrefs.HasKey("Steps"))
            PlayerPrefs.SetInt("Steps", 5);

        if (!PlayerPrefs.HasKey("Volume"))
            PlayerPrefs.SetFloat("Volume", 1f);
        else
            audioSource.volume = PlayerPrefs.GetFloat("Volume");

        if (!PlayerPrefs.HasKey("Vibrate"))
            PlayerPrefs.SetInt("Vibrate", 0);

        DisablePurchaseButtons();
        buttonsManager.LoadSettings();

        if (curr_level == 11)
        {
            StartCoroutine(ReviewInfo());
        }
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
            if (confetti.gameObject.activeSelf)
                confetti.gameObject.SetActive(false);

            if (audioSource.isPlaying)
            {
                audioSource.Pause();
                needUnPause = true;
            }
        }
        else
        {
            if (!confetti.gameObject.activeSelf)
                confetti.gameObject.SetActive(true);

            if (needUnPause)
            {
                audioSource.UnPause();
                needUnPause = false;
            }
        }
    }

    public override void DisablePurchaseButtons()
    {
        if (PlayerPrefs.GetInt("NoAds") == 1)
        {
            NoAdsButton.SetActive(false);
            NoAdsButtonInPauseMenu.interactable = false;
        }

        if (PlayerPrefs.GetInt("UnlockedThemes") == 9 && PlayerPrefs.GetInt("UnlockedTubes") == 2)
        {
            UnlockAllButton.interactable = false;
        }
    }

    public override void ChangeDifficulty()
    {
        difficulty = PlayerPrefs.GetInt("Difficulty_", 0);
        difficulty++;
        if (difficulty > 3)
            difficulty = 0;
        PlayerPrefs.SetInt("Difficulty_", difficulty);
        spawnController.NotFirstLoad();
        HUD.RestartWithoutAds();
    }

    private void SetTextDifficulty()
    {
        switch (difficulty)
        {
            case 0:
                textDifficulty.text = "Level Off";
                break;
            case 1:
                textDifficulty.text = "Easy";
                break;
            case 2:
                textDifficulty.text = "Medium";
                break;
            case 3:
                textDifficulty.text = "Hard";
                break;
        }
    }

    private void LoadDifficultyLevel()
    {
        switch (difficulty)
        {
            case 0:
                curr_level = PlayerPrefs.GetInt("CurrentLevel_OFF") + 1;
                break;
            case 1:
                curr_level = PlayerPrefs.GetInt("CurrentLevel_Easy") + 1;
                break;
            case 2:
                curr_level = PlayerPrefs.GetInt("CurrentLevel_Medium") + 1;
                break;
            case 3:
                curr_level = PlayerPrefs.GetInt("CurrentLevel_Hard") + 1;
                break;
        }
    }

    public override void UpdateTextSteps()
    {
        if (textCountSteps != null)
            textCountSteps.text = PlayerPrefs.GetInt("Steps").ToString();
    }

    public override void StepsPanelButton()
    {
        BuyStepsPanel.SetActive(!BuyStepsPanel.activeSelf);
    }

    public override void AddTube()
    {
        if (Tube != null)
        {
            Tube.SetActive(true);
            AddTubeButton.interactable = false;
            selectedTube = null;
            Tube = null;
        }

        spawnController.SetCenterPosition();
    }

    public override void UpdateEF_Info()
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

    public override void SetSelectedBackground()
    {
        background.sprite = themes[PlayerPrefs.GetInt("Theme")];
    }

    public override void SetSelectedTubes()
    {
        //for (int i = 0; i < tubeControllers.Count; i++)
        //{
        //    tubeControllers[i].tubeSR.sprite = tubes[PlayerPrefs.GetInt("Tube")];
        //    tubeControllers[i].GetComponentInChildren<SpriteMask>().sprite = tubesMasks[PlayerPrefs.GetInt("Tube")];
        //}
    }

    public override void TubeClicked(GameObject TubeObj)
    {
        if (Time.timeScale == 0)
            return;

        TubeController tube = TubeObj.GetComponent<TubeController>();

        if (selectedTube == null && !tube.isBusy)
        {
            selectedTube = TubeObj;

            if (tube.currColors == 0)
                UpdateEF_Info();

            TubeObj.transform.position += new Vector3(0, 0.3f, 0);
        }
        else if (selectedTube == TubeObj)
        {
            TubeObj.transform.position -= new Vector3(0, 0.3f, 0);

            selectedTube = null;
        }
        else if (!addingColor)
        {
            TubeController tubeSelected = selectedTube.GetComponent<TubeController>();
            tubeSelected.PourColor(TubeObj);

            selectedTube = null;
        }

        if (!PlayerPrefs.HasKey("FirstStart"))
        {
            _tutorialController.ProgressControl(0);
        }
    }

    private IEnumerator LevelInitialized()
    {
        yield return new WaitForSeconds(2);
        islevelStart = false;
    }

    public override void SetNewDefaultPositions()
    {
        for (int i = 0; i < tubeControllers.Count; i++)
        {
            if (Tube != null && Tube.activeSelf && i == tubeControllers.Count - 1)
                return;

            if (tubeControllers[i] != null)
                tubeControllers[i].Pos = tubeControllers[i].transform.position;
        }
    }

    public override void SaveTubes()
    {
        AllTubesInfo tubes_info = new AllTubesInfo();
        for (int i = 0; i < tubesInGame.Count; i++)
        {
            TubeInfo tubeInfo = new TubeInfo();
            tubeInfo.currColors = tubeControllers[i].currColors;
            tubeInfo.isEmpty = tubeControllers[i].isEmpty;
            tubeInfo.isFull = tubeControllers[i].isFull;

            for (int j = 0; j < tubeInfo.colors.Length; ++j)
            {
                tubeInfo.colors[j] = tubeControllers[i].LiquidVolume.liquidLayers[j].color;
                tubeInfo.capacities[j] = tubeControllers[i].LiquidVolume.liquidLayers[j].currentAmount;
            }

            tubeInfo.scale = tubeControllers[i].transform.localScale;

            tubes_info.tubes.Add(tubeInfo);
        }

        tubes_info.empty = EmptyTubes;
        tubes_info.full = FullTubes;
        savedTubes.Add(tubes_info);
    }

    public override void StepBackForTubes()
    {
        int count = PlayerPrefs.GetInt("Steps");
        if (count <= 0)
            return;

        if (savedTubes.Count >= 1)
        {
            selectedTube = null;
            for (int i = 0; i < tubeControllers.Count; i++)
            {
                tubeControllers[i].transform.position = new Vector3(tubeControllers[i].transform.position.x,
                    tubeControllers[i].Pos.y, tubeControllers[i].transform.position.z);
            }

            for (int i = 0; i < tubeControllers.Count; i++)
            {
                if (tubeControllers.Count > savedTubes[savedTubes.Count - 1].tubes.Count &&
                    i == tubeControllers.Count - 1)
                    break;

                tubeControllers[i].currColors = savedTubes[savedTubes.Count - 1].tubes[i].currColors;
                tubeControllers[i].isEmpty = savedTubes[savedTubes.Count - 1].tubes[i].isEmpty;
                tubeControllers[i].isFull = savedTubes[savedTubes.Count - 1].tubes[i].isFull;

                tubeControllers[i].transform.position = new Vector3(tubeControllers[i].transform.position.x,
                    tubeControllers[i].Pos.y, tubeControllers[i].transform.position.z);
                tubeControllers[i].transform.localScale = savedTubes[savedTubes.Count - 1].tubes[i].scale;

                tubeControllers[i].NextTube = null;

                for (int a = 0; a < tubeControllers[i].LiquidVolume.liquidLayers.Length; a++)
                {
                    tubeControllers[i].LiquidVolume.liquidLayers[a].color =
                        savedTubes[savedTubes.Count - 1].tubes[i].colors[a];
                    tubeControllers[i].LiquidVolume.liquidLayers[a].amount =
                        savedTubes[savedTubes.Count - 1].tubes[i].capacities[a];
                    
                    
                    tubeControllers[i].colorsInTube[a] = savedTubes[savedTubes.Count - 1].tubes[i].colors[a];
                }

                tubeControllers[i].LiquidVolume.foamThickness = 0;
                for (int j = tubeControllers[i].LiquidVolume.liquidLayers.Length - 1; j >= 0 ; j--)
                {
                    if (savedTubes[savedTubes.Count - 1].tubes[i].capacities[j] > 0.001f)
                    {
                        tubeControllers[i].LiquidVolume.foamColor =
                            savedTubes[savedTubes.Count - 1].tubes[i].colors[j];
                        tubeControllers[i].LiquidVolume.foamThickness =
                            tubeControllers[i].FoamThickness;
                        break;
                    }
                }

                tubeControllers[i].LiquidVolume.UpdateLayers(true);

            }

            EmptyTubes = savedTubes[savedTubes.Count - 1].empty;
            FullTubes = savedTubes[savedTubes.Count - 1].full;

            savedTubes.RemoveAt(savedTubes.Count - 1);

            count--;
            PlayerPrefs.SetInt("Steps", count);
            UpdateTextSteps();
        }
    }

    public override void AddEmpty()
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

        if (!isGameOver)
        {
            confetti.transform.position = pos;
            confetti.Play();
        }

        if (PlayerPrefs.GetInt("Vibrate") == 1 && !isGameOver)
        {
            Handheld.Vibrate();
            Debug.Log("Vibrate");
        }
    }

    public override void RemoveEmpty()
    {
        EmptyTubes--;
    }

    public override void AddFull(Vector3 pos, bool isPlay)
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

    public override void RemoveFull()
    {
        FullTubes--;
    }

    private IEnumerator ReviewInfo()
    {
        _reviewManager = new ReviewManager();

        var requestFlowOperation = _reviewManager.RequestReviewFlow();
        yield return requestFlowOperation;
        if (requestFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }

        _playReviewInfo = requestFlowOperation.GetResult();

        var launchFlowOperation = _reviewManager.LaunchReviewFlow(_playReviewInfo);
        yield return launchFlowOperation;
        _playReviewInfo = null; // Reset the object
        if (launchFlowOperation.Error != ReviewErrorCode.NoError)
        {
            // Log error. For example, using requestFlowOperation.Error.ToString().
            yield break;
        }
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
    public float[] capacities = new float[4];
}

public abstract class IGameManager : MonoBehaviour
{
    public List<GameObject> tubesInGame;
    public List<TubeController> tubeControllers = new List<TubeController>();
    public AudioSource audioSource;
    [SerializeField] public bool addingColor;


    public abstract void SetLvl(int numberOffLvl);
    public abstract void DisablePurchaseButtons();
    public abstract void ChangeDifficulty();
    public abstract void UpdateTextSteps();
    public abstract void StepsPanelButton();
    public abstract void AddTube();
    public abstract void UpdateEF_Info();
    public abstract void SetSelectedBackground();
    public abstract void SetSelectedTubes();
    public abstract void TubeClicked(GameObject TubeObj);
    public abstract void SetNewDefaultPositions();
    public abstract void SaveTubes();
    public abstract void StepBackForTubes();
    public abstract void AddEmpty();
    public abstract void RemoveEmpty();
    public abstract void AddFull(Vector3 pos, bool isPlay);
    public abstract void RemoveFull();
}