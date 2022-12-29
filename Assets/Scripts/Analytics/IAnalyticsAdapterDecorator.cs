public abstract class IAnalyticsAdapterDecorator : IAnalyticsAdapter
{
    protected IAnalyticsAdapter _adapter;

    public IAnalyticsAdapterDecorator(IAnalyticsAdapter adapter)
    {
        _adapter = adapter;
    }

    public virtual void LevelCompleted(int levelNumber, bool status)
    {
        _adapter.LevelCompleted(levelNumber, status);
    }

    public virtual void LevelStarted(int levelNumber)
    {
        _adapter.LevelStarted(levelNumber);
    }

    public virtual void PlayerSessionStart(bool firstSession = false)
    {
        _adapter.PlayerSessionStart(firstSession);
    }

    public virtual void TutorialCompleted()
    {
        _adapter.TutorialCompleted();
    }

    public virtual void TutorialStarted()
    {
        _adapter.TutorialStarted();
    }
}
