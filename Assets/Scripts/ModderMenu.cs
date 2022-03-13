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
                    PlayerPrefs.SetInt("CurrentLevel", int.Parse(levelID) - 1);
                    GM.HUD.Restart();
                }
            }

            if (GUILayout.Button("Clear Level"))
            {
                PlayerPrefs.DeleteKey("CurrentLevel");
                GM.HUD.Restart();
            }

            GUILayout.Label("v0.1");

            GUILayout.EndVertical();
            GUILayout.EndArea();
        }
    }
}
