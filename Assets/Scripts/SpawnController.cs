using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public int gridX;
    public float verticalOffset;
    public int level;
    public int spawnCount;
    public float spacing = 1f;
    public GameObject tube;
    public GameObject emptyTube;
    public GameObject rewardedTube;
    public GameManager GM;
    public Vector3 origin;
    private string[] colors = { "#98FB98", "#FFFFFF", "#FF0000", "#8B0000", "#FF1493", "#8B4513", "#FA8072", "#FFFF00", "#BDB76B", "#DDA0DD", "#8B008B", "#808080", "#00FF00", "#008000", "#00FFFF", "#0000FF", "#000080", "#000000" };
    private int numberOfEmptyTube = 2;
    private int usedColb;
    private int diffuculty = 0;
    private List<UsedColor> usedColors = new List<UsedColor>();

    public void SpawnObject()
    {
        diffuculty = PlayerPrefs.GetInt("Difficulty_", 0);
        ChooseDifficulty();
        spawnCount = level + numberOfEmptyTube;
        usedColb = spawnCount - numberOfEmptyTube;

        if (usedColb > colors.Length)
            usedColb = colors.Length;

        while (usedColors.Count < usedColb)
        {
            int colorID = Random.Range(0, colors.Length);
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
        if (diffuculty == 0)
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
        else if (level_ID >= 8 & level_ID <= 10)
            level = 5;
        else if (level_ID >= 11 & level_ID <= 18)
            level = 6;
        else if (level_ID >= 19 & level_ID <= 30)
            level = 7;
        else if (level_ID >= 31 & level_ID <= 55)
            level = 8;
        else if (level_ID >= 56 & level_ID <= 80)
            level = 9;
        else if (level_ID >= 81 & level_ID <= 150)
            level = 10;
        else if (level_ID >= 151 & level_ID <= 250)
            level = 11;
        else if (level_ID >= 251 & level_ID <= 350)
            level = 12;
        else if (level_ID >= 351 & level_ID <= 450)
            level = 13;
        else if (level_ID >= 451 & level_ID <= 551)
            level = 14;
        else if (level_ID >= 552)
            level = 15;

    }

    private void EndlessMode()
    {
        switch (diffuculty)
        {
            case 1:
                level = Random.Range(5, 8);
                break;
            case 2:
                level = Random.Range(7, 11);
                break;
            case 3:
                level = Random.Range(11, 15);
                break;
            case 4:
                level = 15;
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

            if (i == spawnGrid - 1)
            {
                spawnPosition = new Vector3(spawnedCount * spacing, y * spacing, 0) + origin;
                GameObject spawnedRewTube = Instantiate(rewardedTube, spawnPosition, Quaternion.identity);
                GM.Tube = spawnedRewTube;
                spawnedRewTube.SetActive(false);
            }
        }

        SetCenterPosition();
    }

    public void SetCenterPosition()
    {
        GameObject[] tubes = GameObject.FindGameObjectsWithTag("Tube");

        switch (tubes.Length)
        {
            case 4:
                origin = new Vector3(-2f, origin.y, origin.z);
                break;
            case 5:
                origin = new Vector3(-4.5f, origin.y, origin.z);
                break;

            default:
                origin = new Vector3(-6.5f, origin.y, origin.z);
                break;
        }

        if (tubes.Length < 6)
            origin = new Vector3(origin.x, -5f, origin.z);

        else if (tubes.Length > 6 & tubes.Length < 12)
            origin = new Vector3(origin.x, -1.5f, origin.z);

        else if (tubes.Length > 12)
            origin = new Vector3(origin.x, 9f, origin.z);

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
        if (numberOfEmptyTube == 0)
        {
            GameObject clone = Instantiate(tube, positionToSpawn, rotationToSpawn);
            TubeController tc = clone.GetComponent<TubeController>();

            int i = 0;
            int colorID = 0;
            Color newColor = Color.white;
            while (i < tc.ColorObjects_Renderers.Length)
            {
                colorID = Random.Range(0, usedColors.Count);

                if (usedColors[colorID].colorCount <= 3)
                {
                    ColorUtility.TryParseHtmlString(colors[usedColors[colorID].colorID], out newColor);
                    tc.ColorObjects_Renderers[i].color = newColor;
                    usedColors[colorID].colorCount++;
                    i++;
                }

                if (tc.ColorObjects_Renderers.All(x => x.color == newColor))
                {
                    usedColors[colorID].colorCount--;
                    i--;
                }
            }
        }
        else
        {
            GameObject clone = Instantiate(emptyTube, positionToSpawn, rotationToSpawn);
            numberOfEmptyTube--;
        }
    }
}

public class UsedColor
{
    public int colorID { get; set; }
    public int colorCount { get; set; }
}
