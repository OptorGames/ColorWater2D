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
    public Vector3 origin;
    //private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.gray, Color.grey, Color.cyan };
    private string[] colors = { "#FA8072", "#DC143C", "#FF0000", "#8B0000", "#FFC0CB", "#C71585", "#FF4500", "#FF8C00", "#FFD700", "#FFFF00", "#FFE4B5", "#F0E68C", "#BDB76B", "#DDA0DD", "#BA55D3", "#8B008B", "#4682B4", "#8B4513", "#808080", "#C0C0C0", "#00FF00", "#008000", "#008000", "#0000FF", "#000080" };
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
            usedColb = 20;

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
        if (spawnGrid > 20)
            spawnGrid = 20;

        for (int i = 0; i < spawnGrid; i++)
        {
            print("i: " + i);
            if (spawnedCount == gridX)
            {
                y -= 2.5f;
                spawnedCount = 0;
            }

            Vector3 spawnPosition = new Vector3(spawnedCount * spacing, y * spacing, 0) + origin;
            PickAndSpawn(spawnPosition, Quaternion.identity);
            spawnedCount++;
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

    public void PrintDublicates_Count(int[] n)
    {
        int max = n.Max();
        
    }
}

public class UsedColor
{
    public int colorID { get; set; }
    public int colorCount { get; set; }
}
