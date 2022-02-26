using UnityEngine;

[System.Serializable]
public class Collection
{
    public string collectionName;

    public Sprite[] collectionImages;
    public ushort image_progress1 = 0;
    public ushort image_progress2 = 0;
    public ushort image_progress3 = 0;
    public ushort image_progress4 = 0;

    public bool isCompleted;

    public int id;
}
