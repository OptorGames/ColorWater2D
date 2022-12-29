using DevToDev.Analytics;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

public class DTDAnalyticsAdapter : IAnalyticsAdapterDecorator
{
    //private const string KEY = "cefd4506-9eb6-0f22-adee-e4d6a58b2ce9";
    public DTDAnalyticsAdapter(IAnalyticsAdapter adapter) : base(adapter)
    {
        //DTDAnalytics.Initialize(KEY);
    }

    public override void LevelCompleted(int levelNumber, bool status)
    {
        base.LevelCompleted(levelNumber, status);

        var parameters = new DTDCustomEventParameters();

        parameters.Add("Level_number", levelNumber.ToString());
        parameters.Add("Level_status", status.ToString());


        DTDAnalytics.CustomEvent($"level_completed_{levelNumber}", parameters);
    }

    public override void LevelStarted(int levelNumber)
    {
        base.LevelStarted(levelNumber);

        var parameters = new DTDCustomEventParameters();

        parameters.Add("Level_number", levelNumber.ToString());

        DTDAnalytics.CustomEvent($"level_started_{levelNumber}", parameters);
    }

    public override void PlayerSessionStart(bool firstSession = false)
    {
        base.PlayerSessionStart(firstSession);

        var data = new Dictionary<string, string>();

        data.Add("First_session", firstSession.ToString());

        LogEvent(data);
    }

    public override void TutorialCompleted()
    {
        base.TutorialCompleted();

        LogEvent();
    }

    public override void TutorialStarted()
    {
        base.TutorialStarted();

        LogEvent();
    }

    private void LogEvent(Dictionary<string, string> data = null, [CallerMemberName] string methodName = "default")
    {
        var eventName = Regex.Replace(methodName, @"\B[A-Z]", m => "_" + m.ToString().ToLower()).ToLower();

        if (data == null)
        {
            DTDAnalytics.CustomEvent(eventName);
        }
        else
        {
            var parameters = new DTDCustomEventParameters();

            foreach (KeyValuePair<string, string> entry in data)
            {
                parameters.Add(entry.Key, entry.Value);
            }

            DTDAnalytics.CustomEvent(eventName, parameters);
        }
    }
}
