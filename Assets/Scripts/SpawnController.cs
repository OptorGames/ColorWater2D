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
    private Color[] colors = { Color.red, Color.green, Color.blue, Color.yellow, Color.gray, Color.grey, Color.cyan };
    private int numberOfEmptyTube = 2;
    private List<UsedColor> usedColors = new List<UsedColor>();

    private void Start()
    {
        
    }

    public void SpawnObject()
    {
        level = level / 3;
        if (level == 0)
            level = 1;
        spawnCount = (level + 1) + numberOfEmptyTube;

        while (usedColors.Count < spawnCount - numberOfEmptyTube)
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
        int y = 0;
        if (level >= 8)
            origin = new Vector3(origin.x, 8f, origin.z);

        for (int i = 0; i < spawnCount; i++)
        {
            if (spawnedCount == gridX)
            {
                y -= 2;
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
            int prevID = 0;

            while (i < tc.ColorObjects_Renderers.Length)
            {
                int colorID = Random.Range(0, usedColors.Count);

                if (usedColors[colorID].colorCount <= 3 & prevID != colorID)
                {
                    tc.ColorObjects_Renderers[i].color = colors[usedColors[colorID].colorID];
                    usedColors[colorID].colorCount++;
                    i++;
                    prevID = colorID;
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
