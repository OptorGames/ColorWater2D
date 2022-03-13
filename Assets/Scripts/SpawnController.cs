using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public int level;
    public int spawnCount;
    public int gridX;
    public float spacing = 1f;
    public GameObject tube;
    public GameObject emptyTube;
    public GameObject rewardedTube;
    public GameManager GM;
    public Vector3 origin;
    //private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.gray, Color.grey, Color.cyan };
    private string[] colors = { "#98FB98", "#FFFFFF", "#FF0000", "#8B0000", "#FF1493", "#8B4513", "#FA8072", "#FFFF00", "#BDB76B", "#DDA0DD", "#8B008B", "#808080", "#00FF00", "#008000", "#00FFFF", "#0000FF", "#000080", "#000000" };
    private int numberOfEmptyTube = 2;
    private int usedColb;
    private List<UsedColor> usedColors = new List<UsedColor>();

    private void Start()
    {

    }

    public void SpawnObject()
    {
        if (level >= 2)
            level++;
        level = level / 3;
        spawnCount = (level + 2) + numberOfEmptyTube;
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

    private void SpawnGrid()
    {
        int spawnedCount = 0;
        float y = 0;
        if (level >= 8)
            origin = new Vector3(origin.x, 8f, origin.z);

        int spawnGrid = (usedColb + numberOfEmptyTube);
        if (spawnGrid > 18)
            spawnGrid = 18;

        for (int i = 0; i < spawnGrid; i++)
        {
            print("spawnGrid: " + spawnGrid);
            print("i: " + i);
            if (spawnedCount == gridX)
            {
                y -= 2.5f;
                spawnedCount = 0;
            }

            Vector3 spawnPosition = new Vector3(spawnedCount * spacing, y * spacing, 0) + origin;
            PickAndSpawn(spawnPosition, Quaternion.identity);
            spawnedCount++;

            if (i == spawnGrid - 1)
            {
                spawnPosition = new Vector3(spawnedCount * spacing, y * spacing, 0) + origin;
                GameObject spawnedRewTube = Instantiate(rewardedTube, spawnPosition, Quaternion.identity);
                GM.Tube = spawnedRewTube;
                spawnedRewTube.SetActive(false);

            }
        }
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
