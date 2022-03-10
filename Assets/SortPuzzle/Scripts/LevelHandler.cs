using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject[] Levels;

    public HudHandler HUD;
    public SpawnController sc;

    private void Start()
    {
        //Levels[PlayerPrefs.GetInt("CurrentGameLevel")].SetActive(true);
        sc.level = PlayerPrefs.GetInt("CurrentGameLevel");
        sc.SpawnObject();
    }
}
