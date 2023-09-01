using UnityEngine;

public class InternetConnectionDataChecker : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
    }
    private const int MaxLevelsToPlayWithoutInternet = 2;
    public static int StartedFromLevel { get; private set; } = 0;
    public void SetLevelOnStart(int level) => StartedFromLevel = level;
    public static bool IsOnline() => Application.internetReachability != NetworkReachability.NotReachable;
    public static bool IsNextLevelAvailable(int nextLevel)
    {
        if (IsOnline())
            return true;
        else if (nextLevel - StartedFromLevel > MaxLevelsToPlayWithoutInternet)
            return false;
        return true;
    }
}
