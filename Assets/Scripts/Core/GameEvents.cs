using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void LevelComplete(int level);
    public static event LevelComplete OnLevelComplete;

    public static void RaiseOnLevelComplete(int level)
    {
        OnLevelComplete?.Invoke(level);
    }
}
