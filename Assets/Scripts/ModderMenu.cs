using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModderMenu : MonoBehaviour
{
    public GameManager GM;
    private bool ModMenu = false;

    private void Start()
    {
        InvokeRepeating(nameof(ShowAndHideMenu), 3f, 3f);
    }

    private void ShowAndHideMenu()
    {
        if (Input.GetKey(KeyCode.M))
        {
            if (ModMenu)
                ModMenu = false;
            else
                ModMenu = true;
        }
    }

    public string levelID;

    private void OnGUI()
    {
        if (ModMenu)
        {
            GUILayout.BeginArea(new Rect(50, 10, 110, 130));
            GUILayout.BeginVertical();

            GUILayout.Label("Enter Level");
            levelID = GUILayout.TextField(levelID, 3);
            if (GUILayout.Button("Set&Load Level"))
            {
                if (int.Parse(levelID) > 0)
                {
                    SetLevel(int.Parse(levelID));
                    GM.HUD.Restart();
                }
            }

            if (GUILayout.Button("Clear Level"))
            {

                DeleteLevel();
                GM.HUD.Restart();
            }

            GUILayout.Label("v0.2.1");

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }

    private void SetLevel(int level)
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty_", 0);

        switch (difficulty)
        {
            case 0:
                PlayerPrefs.SetInt("CurrentLevel_OFF", level - 1);
                break;
            case 1:
                PlayerPrefs.SetInt("CurrentLevel_Easy", level - 1);
                break;
            case 2:
                PlayerPrefs.SetInt("CurrentLevel_Medium", level - 1);
                break;
            case 3:
                PlayerPrefs.SetInt("CurrentLevel_Hard", level - 1);
                break;
            case 4:
                PlayerPrefs.SetInt("CurrentLevel_Extreme", level - 1);
                break;
        }
    }

    private void DeleteLevel()
    {
        int difficulty = PlayerPrefs.GetInt("Difficulty_", 0);

        switch (difficulty)
        {
            case 0:
                PlayerPrefs.DeleteKey("CurrentLevel_OFF");
                break;
            case 1:
                PlayerPrefs.DeleteKey("CurrentLevel_Easy");
                break;
            case 2:
                PlayerPrefs.DeleteKey("CurrentLevel_Medium");
                break;
            case 3:
                PlayerPrefs.DeleteKey("CurrentLevel_Hard");
                break;
            case 4:
                PlayerPrefs.DeleteKey("CurrentLevel_Extreme");
                break;
        }
    }
}
