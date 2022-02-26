using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JsonSaver : MonoBehaviour
{
    private Data data = new Data();
    private SaveData saveData = new SaveData();

    private string path;

    private MenuManager manager;

    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "menu")
        {
            manager = GetComponent<MenuManager>();
            Debug.Log("Load Data from SaveJson");
            Load();
        }
        else LoadPath();
    }

    public void LoadPath()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        path = Application.persistentDataPath + "/data.json";
#endif
#if !UNITY_EDITOR && UNITY_IOS
        path = Application.persistentDataPath + "/data.json";
#endif
#if UNITY_EDITOR
        path = Application.streamingAssetsPath + "/data.json";
#endif
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);

            int charsCount = text.Length;
            byte[] bytes = new byte[charsCount / 2];

            for (int i = 0; i < charsCount; i += 2)
                bytes[i / 2] = Convert.ToByte(text.Substring(i, 2), 16);

            text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            saveData = JsonUtility.FromJson<SaveData>(text);
        }
    }

    public void Load()
    {
#if !UNITY_EDITOR && UNITY_ANDROID
        path = Application.persistentDataPath + "/data.json";
#endif
#if !UNITY_EDITOR && UNITY_IOS
        path = Application.persistentDataPath + "/data.json";
#endif
#if UNITY_EDITOR
        path = Application.streamingAssetsPath + "/data.json";
#endif
        if (File.Exists(path))
        {
            string text = File.ReadAllText(path);

            int charsCount = text.Length;
            byte[] bytes = new byte[charsCount / 2];

            for (int i = 0; i < charsCount; i += 2)
                bytes[i / 2] = Convert.ToByte(text.Substring(i, 2), 16);

            text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);

            saveData = JsonUtility.FromJson<SaveData>(text);

            for (int i = 0; i < saveData.progress.Length; i++)
            {
                string[] progress = saveData.progress[i].Split('=');
                Collection collection = new Collection();

                collection.image_progress1 = ushort.Parse(progress[0]);
                collection.image_progress2 = ushort.Parse(progress[1]);
                collection.image_progress3 = ushort.Parse(progress[2]);
                collection.image_progress4 = ushort.Parse(progress[3]);
                collection.isCompleted = saveData.isCompleted[i];
                data.collections.Add(collection);
            }

            manager.SetColletionsInfo(data.collections);
        }
        else
        {
            manager.SortCollections();
        }
    }

    public void Save(Collection[] collections)
    {
        Array.Resize(ref saveData.progress, collections.Length);
        Array.Resize(ref saveData.isCompleted, collections.Length);

        for (int i = 0; i < collections.Length; i++)
        {
            string str = collections[i].image_progress1.ToString() + '=' +
                collections[i].image_progress2.ToString() + '=' +
                collections[i].image_progress3.ToString() + '=' +
                collections[i].image_progress4.ToString();

            saveData.progress[i] = str;
            saveData.isCompleted[i] = collections[i].isCompleted;
        }

        byte[] bytes = Encoding.UTF8.GetBytes(JsonUtility.ToJson(saveData));
        string hex = BitConverter.ToString(bytes);

        File.WriteAllText(path, hex.Replace("-", ""));
    }
}

public class Data
{
    public List<Collection> collections = new List<Collection>();
}

public class SaveData
{
    public string[] progress;
    public bool[] isCompleted;
}