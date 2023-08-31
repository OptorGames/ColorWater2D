using Firebase.Analytics;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Debug = UnityEngine.Debug;

public class FirebaseAnalyticsAdapter : IAnalyticsAdapter
{
    public void LevelCompleted(int levelNumber, bool status)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
            $"level_completed_{levelNumber}",
            new Parameter("Level_number", levelNumber.ToString()),
            new Parameter("Level_status", status.ToString()));
    }

    public void LevelStarted(int levelNumber)
    {
        Debug.LogError("Firebase level_started");
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
            $"level_started_{levelNumber}",
            new Parameter("Level_number", levelNumber.ToString()));
    }

    public void PlayerSessionStart(bool firstSession = false)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
        "first_session",
        new Parameter("First_session", firstSession.ToString()));
    }

    public void TutorialCompleted()
    {
        LogSimpleEvent();
    }

    public void TutorialStarted()
    {
        LogSimpleEvent();
    }

    private void LogSimpleEvent([CallerMemberName] string methodName = "default")
    {
        var text = Regex.Replace(methodName, @"\B[A-Z]", m => "_" + m.ToString().ToLower()).ToLower();

        FirebaseAnalytics.LogEvent(text);
    }
}
