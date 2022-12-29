public static class GameEvents
{
    public delegate void LevelComplete(int level);
    public static event LevelComplete OnLevelComplete;

    public delegate void FirebaseLoaded();
    public static event FirebaseLoaded OnFirebaseLoaded;

    public static void RaiseOnLevelComplete(int level)
    {
        OnLevelComplete?.Invoke(level);
    }

    public static void RaiseOnFirebaseLoaded()
    {
        OnFirebaseLoaded?.Invoke();
    }
}
