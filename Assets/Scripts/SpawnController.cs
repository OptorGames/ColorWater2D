using System.Collections.Generic;
using System.Linq;
using ForTutorial;
using LiquidVolumeFX;
using UnityEngine;

public class SpawnController : ISpawnController
{
    public int gridX;
    public float verticalOffset;
    //public int level { get; set; }
    public int spawnCount;
    public float spacing = 1f;
    public GameObject tube;
    public GameObject emptyTube;
    public GameObject rewardedTube;
    public GameManager GM;
    public Vector3 origin;
    public List<Flask> Flasks = new List<Flask>();

    protected string[] colors =
    {
        "#98FB98", "#FFFFFF", "#FF0000", "#8B0000", "#FF1493", "#8B4513", "#FA8072", "#FFFF00", "#BDB76B", "#DDA0DD",
        "#8B008B", "#808080", "#00FF00", "#008000", "#00FFFF", "#0000FF", "#000080", "#000000"
    };

    public int numberOfEmptyTube = 2;
    protected int usedColb;
    protected int difficulty = 0;
    protected List<UsedColor> usedColors = new List<UsedColor>();
    [SerializeField] protected List<TubeController> coloredTubes = new List<TubeController>();
    [SerializeField] protected TutorialController _tutorialController;
    public static ISpawnController spawnController = null;
    public static bool firstStart = true;
    
    [SerializeField] protected Color tutorialColor = new Color(0.9176471f, 0.2235294f, 0.7463644f);

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
        difficulty = PlayerPrefs.GetInt("Difficulty_", 0);
        ChooseDifficulty();
        spawnCount = level + numberOfEmptyTube;
        usedColb = spawnCount - numberOfEmptyTube;

        if (usedColb > colors.Length)
            usedColb = colors.Length;

        while (usedColors.Count < usedColb)
        {
            int colorID = Random.Range(0, colors.Length);
            if (CheckOnExist(colorID))
                usedColors.Add(new UsedColor {colorID = colorID, colorCount = 0});
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
        if (level_ID == 1)
            level = 2;
        else if (level_ID >= 2 & level_ID <= 4)
            level = 3;
        else if (level_ID >= 5 & level_ID <= 7)
            level = 4;
        else if (level_ID >= 8 & level_ID <= 11)
            level = 5;
        else if (level_ID >= 12 & level_ID <= 21)
            level = Random.Range(4, 7);
        else if (level_ID >= 22 & level_ID <= 50)
            level = Random.Range(4, 8);
        else if (level_ID >= 51 & level_ID <= 100)
            level = Random.Range(5, 8);
        else if (level_ID >= 101 & level_ID <= 300)
            level = Random.Range(5, 9);
        else if (level_ID >= 301)
            level = Random.Range(5, 10);
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
        int spawnedCount = 0;
        float y = 0;
        int spawnGrid = (usedColb + numberOfEmptyTube);
        if (spawnGrid > 17)
            spawnGrid = 17;
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

        FillTube();
        SetCenterPosition();
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
        var flask = Flasks.Find(x => !x.GameObject.activeInHierarchy);
        flask?.GameObject.SetActive(true);

        if (PlayerPrefs.HasKey("FirstStart"))
        {
            if (numberOfEmptyTube == 0)
            {
                coloredTubes.Add(flask?.GameObject.GetComponent<TubeController>());
            }
            else
            {
                //Instantiate(emptyTube, positionToSpawn, rotationToSpawn);
                numberOfEmptyTube--;
            }
        }
        else
        {
            _tutorialController.TutorialTubes.Add(flask.GameObject);
            coloredTubes.Add(flask?.GameObject.GetComponent<TubeController>());
            _tutorialController.TutorialArrows.Add(Instantiate(_tutorialController.ArrowPoint, flask.GameObject.transform.GetChild(1).position, Quaternion.identity));
        }
    }

    private void FillTube()
    {

        if (PlayerPrefs.HasKey("FirstStart"))
        {
            int colorID;
            Color newColor = Color.white;

            for (int i = 0; i < coloredTubes.Count; i++)
            {
                for (int j = 0; j < coloredTubes[i].LiquidVolume.liquidLayers.Length; j++)
                {
                    var freeColors = usedColors.FindAll(x => x.colorCount < 4);
                    colorID = Random.Range(0, freeColors.Count);

                    ColorUtility.TryParseHtmlString(colors[freeColors[colorID].colorID], out newColor);
                    coloredTubes[i].LiquidVolume.liquidLayers[j].color = newColor;
                    coloredTubes[i].LiquidVolume.liquidLayers[j].amount = coloredTubes[i].ColorLayerAmount;
                    freeColors[colorID].colorCount++;

                    coloredTubes[i].LiquidVolume.foamColor = newColor;
                    coloredTubes[i].LiquidVolume.foamThickness = coloredTubes[i].FoamThickness;
                }

                coloredTubes[i].LiquidVolume.UpdateLayers(true);
            }
        }
        else
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
}

public abstract class ISpawnController : MonoBehaviour
{
    public int level { get; set; }
    public abstract int GetDifficulty();
    public abstract void NotFirstLoad();
    public abstract void SpawnObject();
    public abstract void SetCenterPosition();
}