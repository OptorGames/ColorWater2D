using System.Collections.Generic;
using System.Linq;
using ForTutorial;
using LiquidVolumeFX;
using UnityEngine;

public class VideoSpawnController : ISpawnController
{
    public int gridX;
    public float verticalOffset;
    //public int level { get; set; }
    public int spawnCount;
    public float spacing = 1f;
    public GameObject tube;
    public GameObject emptyTube;
    public GameObject rewardedTube;
    public IGameManager GM;
    public Vector3 origin;
    public List<Flask> Flasks = new List<Flask>();

    private string[] colors =
    {
        "#98FB98", "#FFFFFF", "#FF0000", "#8B0000", "#FF1493", "#8B4513", "#FA8072", "#FFFF00", "#BDB76B", "#DDA0DD",
        "#8B008B", "#808080", "#00FF00", "#008000", "#00FFFF", "#0000FF", "#000080", "#000000"
    };

    public int numberOfEmptyTube = 2;
    private int usedColb;
    private int difficulty = 0;
    private List<UsedColor> usedColors = new List<UsedColor>();
    [SerializeField] private List<TubeController> coloredTubes = new List<TubeController>();
    [SerializeField] private TutorialController _tutorialController;
    public static ISpawnController spawnController = null;
    public static bool firstStart = true;

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
        spawnCount = 3;
        usedColb = 3;

        if (usedColb > colors.Length)
            usedColb = colors.Length;

        while (usedColors.Count < usedColb - 1)
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
        level = 2;
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
        //if (PlayerPrefs.HasKey("FirstStart"))
        //{
            var flask = Flasks.Find(x => !x.GameObject.activeInHierarchy);
            flask?.GameObject.SetActive(true);

            if (numberOfEmptyTube == 0)
            {
                coloredTubes.Add(flask?.GameObject.GetComponent<TubeController>());
            }
            else
            {
                Instantiate(emptyTube, positionToSpawn, rotationToSpawn);
                numberOfEmptyTube--;
            }
        //}
        //else
        //{
        //    ////TODO: fix tutorial
        //    _tutorialController.TutorialTubes.Add(Instantiate(_tutorialController.TubeForTutorial, positionToSpawn, rotationToSpawn));
        //    positionToSpawn.x += 1.2f;
        //    positionToSpawn.y -= 20;
        //    _tutorialController.TutorialArrows.Add(Instantiate(_tutorialController.ArrowPoint, positionToSpawn, rotationToSpawn));
        //}
    }

    private void FillTube()
    {
        //int colorID;

        //for (int i = 0; i < coloredTubes.Count; i++)
        //{
        //    for (int j = 0; j < coloredTubes[i].LiquidVolume.liquidLayers.Length; j++)
        //    {
        //        var freeColors = usedColors.FindAll(x => x.colorCount < 4);
        //        colorID = Random.Range(0, freeColors.Count);

        //        ColorUtility.TryParseHtmlString(colors[freeColors[colorID].colorID], out newColor);
        //        coloredTubes[i].LiquidVolume.liquidLayers[j].color = newColor;
        //        coloredTubes[i].LiquidVolume.liquidLayers[j].amount = coloredTubes[i].ColorLayerAmount;
        //        freeColors[colorID].colorCount++;

        //        coloredTubes[i].LiquidVolume.foamColor = newColor;
        //    }

        //    coloredTubes[i].LiquidVolume.UpdateLayers();
        //}

        Color newColor1 = Color.white;
        Color newColor2 = Color.white;


        ColorUtility.TryParseHtmlString("#0099ff", out newColor1); //blue
        ColorUtility.TryParseHtmlString("#f5ff30", out newColor2); //yellow

        coloredTubes[0].LiquidVolume.liquidLayers[0].color = newColor2;
        coloredTubes[0].LiquidVolume.liquidLayers[1].color = newColor1;
        coloredTubes[0].LiquidVolume.liquidLayers[0].amount = coloredTubes[0].ColorLayerAmount;
        coloredTubes[0].LiquidVolume.liquidLayers[1].amount = coloredTubes[0].ColorLayerAmount;
        coloredTubes[0].LiquidVolume.foamColor = newColor1;
        coloredTubes[0].LiquidVolume.UpdateLayers(true);

        coloredTubes[1].LiquidVolume.liquidLayers[0].color = newColor1;
        coloredTubes[1].LiquidVolume.liquidLayers[1].color = newColor2;
        coloredTubes[1].LiquidVolume.liquidLayers[2].color = newColor2;
        coloredTubes[1].LiquidVolume.liquidLayers[3].color = newColor2;
        coloredTubes[1].LiquidVolume.liquidLayers[0].amount = coloredTubes[1].ColorLayerAmount;
        coloredTubes[1].LiquidVolume.liquidLayers[1].amount = coloredTubes[1].ColorLayerAmount;
        coloredTubes[1].LiquidVolume.liquidLayers[2].amount = coloredTubes[1].ColorLayerAmount;
        coloredTubes[1].LiquidVolume.liquidLayers[3].amount = coloredTubes[1].ColorLayerAmount;
        coloredTubes[1].LiquidVolume.foamColor = newColor2;
        coloredTubes[1].LiquidVolume.UpdateLayers(true);

        coloredTubes[2].LiquidVolume.liquidLayers[0].color = newColor1;
        coloredTubes[2].LiquidVolume.liquidLayers[1].color = newColor1;
        coloredTubes[2].LiquidVolume.liquidLayers[0].amount = coloredTubes[2].ColorLayerAmount;
        coloredTubes[2].LiquidVolume.liquidLayers[1].amount = coloredTubes[2].ColorLayerAmount;
        coloredTubes[2].LiquidVolume.foamColor = newColor1;
        coloredTubes[2].LiquidVolume.UpdateLayers(true);

    }
}

//public class UsedColor
//{
//    public int colorID { get; set; }
//    public int colorCount { get; set; }
//}

//[System.Serializable]
//public class Flask
//{
//    public GameObject GameObject;
//    public LiquidVolume LiquidVolume;
//}