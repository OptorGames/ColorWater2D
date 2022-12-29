using System.Collections.Generic;

public interface IAnalyticsAdapter
{
    void PlayerSessionStart(bool firstSession = false);
    void LevelStarted(int levelNumber);
    void LevelCompleted(int levelNumber, bool status);
    void TutorialStarted();
    void TutorialCompleted();
}