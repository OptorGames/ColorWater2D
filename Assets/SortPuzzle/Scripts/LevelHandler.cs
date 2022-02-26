using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static int currentLevel;
    public GameObject[] Levels;

    public HudHandler HUD;

    private void Start()
    {
        Levels[currentLevel].SetActive(true);
    }
}
