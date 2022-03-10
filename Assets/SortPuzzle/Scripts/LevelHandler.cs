using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public GameObject[] Levels;

    public HudHandler HUD;

    private void Start()
    {
      //Levels[PlayerPrefs.GetInt("CurrentGameLevel")].SetActive(true);
    }
}
