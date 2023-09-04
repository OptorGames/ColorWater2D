using System.Collections.Generic;
using System.Linq;
using ForTutorial;
using LiquidVolumeFX;
using UnityEngine;

public class SpawnController : ISpawnController
{
    private const int numberOfAdTubes = 1;
    private const int MaxTubesInRow = 6;

    [SerializeField] private LevelSO levelSO;
    [SerializeField] private Transform flasksParent;
    [SerializeField] private float[] offsets;


    public int gridX;
    public float verticalOffset;
    //public int level { get; set; }
    public float spacing = 1f;
    public GameObject tube;
    public GameObject emptyTube;
    public GameObject rewardedTube;
    public GameManager GM;
    public Vector3 origin;
    public Flasks Flasks;
    public int activatedFlasks;

    private int TotalTubes;

    private bool _lastSpawnedFromFirstRow = false;

    public override int ActivatedFlasks
    {
        get => activatedFlasks;
        set => activatedFlasks = value;
    }

    [field: SerializeField] public List<Color> Colors { get; private set; }

    public int numberOfEmptyTube = 2;
    private int defaultNumberOfEmptyTube = 2;
    protected int difficulty = 0;
    protected List<UsedColor> usedColors = new List<UsedColor>();
    [SerializeField] protected List<TubeController> coloredTubes = new List<TubeController>();
    [SerializeField] protected TutorialController _tutorialController;
    public static ISpawnController spawnController = null;
    public static bool firstStart = true;

    [SerializeField] protected Color tutorialColor;

    private void Start()
    {
        if (spawnController == null)
        {
            spawnController = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    public override int GetDifficulty()
    {
        if (firstStart)
        {
            difficulty = 0;
            PlayerPrefs.SetInt("Difficulty_", difficulty);
            return difficulty;
        }
        else
            return difficulty = PlayerPrefs.GetInt("Difficulty_", 0);
    }

    public override void NotFirstLoad()
    {
        firstStart = false;
    }

    public override void SpawnObject()
    {
        numberOfEmptyTube = defaultNumberOfEmptyTube;
        difficulty = PlayerPrefs.GetInt("Difficulty_", 0);
        ChooseDifficulty();
        AdTubes = PlayerPrefs.HasKey("FirstStart") ? numberOfAdTubes : 0;

        TotalTubes = FilledTubesAmount + EmptyTubesAmount + AdTubes;

        if (FilledTubesAmount > Colors.Count)
            FilledTubesAmount = Colors.Count;


        var random = new System.Random();

        while (usedColors.Count < FilledTubesAmount)
        {
            //int colorID = Random.Range(0, colors.Length);
            int colorID = random.Next(Colors.Count);
            if (CheckOnExist(colorID))
                usedColors.Add(new UsedColor { colorID = colorID, colorCount = 0 });
        }


        SpawnGrid();
    }

    private bool CheckOnExist(int colID)
    {
        if (usedColors.Exists(x => x.colorID == colID))
            return false;
        else
            return true;
    }

    private void ChooseDifficulty()
    {
        if (difficulty == 0)
            ChooseTubesNumber(level);
        else
            EndlessMode();
    }

    private void ChooseTubesNumber(int level_ID)
    {
        var levelData = levelSO.GetLevelSettings(level_ID);
        FilledTubesAmount = levelData.FilledTubesAmount;
        EmptyTubesAmount = levelData.EmptyTubesAmount;
        IsHiddenColors = levelData.WithQuestionMark;
    }

    private void EndlessMode()
    {
        switch (difficulty)
        {
            case 1:
                level = Random.Range(3, 6);
                break;
            case 2:
                level = Random.Range(5, 8);
                break;
            case 3:
                level = Random.Range(7, 10);
                break;
        }
    }

    private void SpawnGrid()
    {
        coloredTubes = new List<TubeController>();
        int spawnedCount = 0;
        float y = 0;
        int spawnGrid = FilledTubesAmount + EmptyTubesAmount + AdTubes;
        if (spawnGrid > Colors.Count)
            spawnGrid = Colors.Count;
        activatedFlasks = 0;
        SetCenter();
        for (int i = 0; i < spawnGrid; i++)
        {
            Vector3 spawnPosition = new Vector3(spawnedCount * spacing, y * spacing, 0) + origin;
            PickAndSpawn(spawnPosition, Quaternion.identity);
            spawnedCount++;

            if (spawnedCount == gridX)
            {
                y -= verticalOffset;
                spawnedCount = 0;
            }
        }

        if (PlayerPrefs.HasKey("FirstStart"))
        {
            FillTubes();
        }
        else
        {
            FillTutorialTubes();
        }

        SetCenterPosition();

        var spawnedFlasks = Flasks.FlasksList.Where(x => x.GameObject.activeInHierarchy).ToList();
        Flasks.UpdateShelfVisibility();
        foreach (var flask in spawnedFlasks)
        {
            GM.AddTube(flask.GameObject);
        }
        if (PlayerPrefs.HasKey("FirstStart"))
            GM.tubeControllers.Last().SetIsOpenedByAd(true);
    }

    private void SetCenter()
    {
        int offsetIndex = (TotalTubes / 2) - 1;
        float offset = offsets[offsetIndex];
        flasksParent.position += new Vector3(offset, 0, 0);
    }

    public override void SetCenterPosition()
    {
        GameObject[] tubes = GameObject.FindGameObjectsWithTag("Tube");

        if (tubes.Length < 9)
            origin = new Vector3(origin.x, 0f, origin.z);

        else if (tubes.Length >= 9 & tubes.Length <= 12)
            origin = new Vector3(origin.x, 4.4f, origin.z);

        int spawnedCount = 0;
        float y = 0;

        for (int i = 0; i < tubes.Length; i++)
        {
            Vector3 spawnPosition = new Vector3(spawnedCount * spacing, y * spacing, 0) + origin;
            tubes[i].transform.position = spawnPosition;
            spawnedCount++;

            if (spawnedCount == gridX)
            {
                y -= verticalOffset;
                spawnedCount = 0;
            }
        }

        GM.SetNewDefaultPositions();
    }

    private void PickAndSpawn(Vector3 positionToSpawn, Quaternion rotationToSpawn)
    {
        bool activateOnFirstRow = activatedFlasks < TotalTubes / 2;

        int flaskIndex = 0;
        if (activateOnFirstRow)
            flaskIndex = Flasks.FlasksList.IndexOf(Flasks.FlasksList.Take(MaxTubesInRow).FirstOrDefault(x => !x.GameObject.activeInHierarchy));
        else
            flaskIndex = Flasks.FlasksList.IndexOf(Flasks.FlasksList.TakeLast(MaxTubesInRow).FirstOrDefault(x => !x.GameObject.activeInHierarchy));

        var flask = Flasks.FlasksList[flaskIndex];
        flask?.GameObject.SetActive(true);
        flask?.SetIsHiddenColor(IsHiddenColors);
        activatedFlasks++;

        if (PlayerPrefs.HasKey("FirstStart"))
        {
            var tube = flask?.GameObject.GetComponent<TubeController>();
            tube.SetFlask(flask);
            if (activatedFlasks <= FilledTubesAmount)
            {
                coloredTubes.Add(tube);
            }
            else
            {
                numberOfEmptyTube--;
            }
        }
        else
        {
            _tutorialController.TutorialTubes.Add(flask.GameObject);
            coloredTubes.Add(flask?.GameObject.GetComponent<TubeController>());
            _tutorialController.TutorialArrows.Add(Instantiate(_tutorialController.ArrowPoint, flask.GameObject.transform.GetChild(1).position, Quaternion.identity));
        }

        _lastSpawnedFromFirstRow = !_lastSpawnedFromFirstRow;
    }

    private void FillTubes()
    {
        foreach (UsedColor usedColor in usedColors)
        {
            usedColor.colorCount = 0;
        }

        int colorIndex;
        Color newColor = Color.white;

        for (int i = 0; i < coloredTubes.Count; i++)
        {
            for (int j = 0; j < coloredTubes[i].LiquidVolume.liquidLayers.Length; j++)
            {
                var freeColors = usedColors.FindAll(x => x.colorCount < 4);
                colorIndex = Random.Range(0, freeColors.Count);

                newColor = Colors[freeColors[colorIndex].colorID];
                coloredTubes[i].LiquidVolume.liquidLayers[j].color = newColor;
                coloredTubes[i].LiquidVolume.liquidLayers[j].amount = coloredTubes[i].ColorLayerAmount;
                usedColors.First(x => x.colorID == freeColors[colorIndex].colorID).colorCount++;

                coloredTubes[i].LiquidVolume.foamColor = newColor;
                coloredTubes[i].LiquidVolume.foamThickness = coloredTubes[i].FoamThickness;
            }

            coloredTubes[i].LiquidVolume.UpdateLayers(true);
        }

        CheckFilledTubes();
    }

    private void FillTutorialTubes()
    {
        for (int i = 0; i < coloredTubes.Count; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                coloredTubes[i].LiquidVolume.liquidLayers[j].color = tutorialColor;
                coloredTubes[i].LiquidVolume.liquidLayers[j].amount = coloredTubes[i].ColorLayerAmount;
                coloredTubes[i].LiquidVolume.foamColor = tutorialColor;
            }

            coloredTubes[i].LiquidVolume.UpdateLayers(true);
        }
    }

    private void CheckFilledTubes()
    {
        for (int i = 0; i < coloredTubes.Count; i++)
        {
            if (coloredTubes[i].LiquidVolume.liquidLayers.Length <= 0)
            {
                continue;
            }

            int sameColorsCount = 0;

            for (int j = 0; j < coloredTubes[i].LiquidVolume.liquidLayers.Length; j++)
            {
                if (j > 0 && coloredTubes[i].LiquidVolume.liquidLayers[j].currentAmount > 0.01f
                    && coloredTubes[i].LiquidVolume.liquidLayers[j - 1].currentAmount > 0.01f
                    && coloredTubes[i].LiquidVolume.liquidLayers[j - 1].color ==
                       coloredTubes[i].LiquidVolume.liquidLayers[j].color)
                {
                    ++sameColorsCount;
                }
            }

            if (sameColorsCount == coloredTubes[i].LiquidVolume.liquidLayers.Length - 1)
            {
                FillTubes();
                return;
            }
        }
    }

    public void RefillTubes(List<TubeInfo> tubes)
    {
        int refilledTubes = 0;
        foreach (TubeInfo info in tubes)
        {
            bool activateOnFirstRow = refilledTubes < TotalTubes / 2;

            int flaskIndex = 0;
            if (activateOnFirstRow)
                flaskIndex = Flasks.FlasksList.IndexOf(Flasks.FlasksList.Take(MaxTubesInRow).FirstOrDefault(x => !x.GameObject.activeInHierarchy));
            else
                flaskIndex = Flasks.FlasksList.IndexOf(Flasks.FlasksList.TakeLast(MaxTubesInRow).FirstOrDefault(x => !x.GameObject.activeInHierarchy));

            var flask = Flasks.FlasksList[flaskIndex];
            flask?.GameObject.SetActive(true);
            flask?.SetIsHiddenColor(IsHiddenColors);

            var controller = flask.GameObject.GetComponent<TubeController>();
            controller.isEmpty = info.isEmpty;
            controller.isFull = info.isFull;
            controller.currColors = info.currColors;


            for (int i = 0; i < info.colors.Length; ++i)
            {
                flask.LiquidVolume.liquidLayers[i].color = info.colors[i];
                flask.LiquidVolume.liquidLayers[i].amount = info.capacities[i];

                if (info.capacities[i] > 0.01)
                {
                    flask.LiquidVolume.foamColor = info.colors[i];
                }
            }

            refilledTubes++;
            flask.LiquidVolume.UpdateLayers(true);
        }
    }

    public override void AddAdditionalTube()
    {
        var flask = Flasks.FlasksList.Find(x => !x.GameObject.activeInHierarchy);
        var controller = flask.GameObject.GetComponent<TubeController>();
        controller.isEmpty = true;
        flask?.GameObject.SetActive(true);
        flask.LiquidVolume.foamThickness = 0f;

        flask.LiquidVolume.UpdateLayers(true);

        if (!Flasks.FlasksList.Exists(x => !x.GameObject.activeInHierarchy))
        {
            GM.buttonsManager.DisableAdditionalTubeButton();
        }
    }


}

public class UsedColor
{
    public int colorID { get; set; }
    public int colorCount { get; set; }
}

[System.Serializable]
public class Flask
{
    public GameObject GameObject;
    public LiquidVolume LiquidVolume;
    public bool IsHiddenColor { get; private set; }

    public void SetIsHiddenColor(bool value) => IsHiddenColor = value;
}

public abstract class ISpawnController : MonoBehaviour
{
    public int level { get; set; }
    public int FilledTubesAmount { get; protected set; }
    public int EmptyTubesAmount { get; protected set; }
    public bool IsHiddenColors { get; protected set; }
    public int AdTubes { get; protected set; }
    public abstract int GetDifficulty();
    public abstract void NotFirstLoad();
    public abstract void SpawnObject();
    public abstract void SetCenterPosition();
    public abstract void AddAdditionalTube();
    public abstract int ActivatedFlasks { get; set; }
}