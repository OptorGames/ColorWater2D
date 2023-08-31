using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/Level Settings", order = 1)]
public class LevelSO : ScriptableObject
{
    [SerializeField] private List<LevelSettings> levelSettings;

    public LevelSettings GetLevelSettings(int level)
    {
        var result = levelSettings.FirstOrDefault(x => x.Level == level);
        if (result == null)
        {
            var lastLevels = levelSettings.TakeLast(10).ToList();
            return lastLevels[Random.Range(0, lastLevels.Count())];
        }
        return result;
    }
}
