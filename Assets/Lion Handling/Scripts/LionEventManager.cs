using Facebook.Unity;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LionEventManager : MonoBehaviour
{
    [SerializeField] MoneyUI playerMoney;
    [SerializeField] LevelUpgradeHandler upgradeHandler;
    public void OnTutorialComplete()
    {
        LionStudios.Analytics.Events.TutorialComplete();
        LogTutorialCompleteEvent();
    }

    public void OnLevelComplete(int levelnumber)
    {
        LionStudios.Analytics.Events.LevelComplete(levelnumber, playerMoney.GetPlayerSavedMoney());
        LogLevelCompleteEvent(levelnumber);
    }
    public void OnLevelStarted(int levelnumber)
    {
        LionStudios.Analytics.Events.LevelStarted(levelnumber, playerMoney.GetPlayerSavedMoney());
        LogLevelStartedEvent(levelnumber);
    }

    public void AllLevelsComplete()
    {
        LionStudios.Analytics.Events.AllLevelsComplete();
        LogAllLevelsCompleteEvent();
    }

    public void OnContentUnlocked()
    {
        LionStudios.Analytics.Events.ContentUnlocked(upgradeHandler.GetUnlockedContent());
        LogContentUnlockedEvent(upgradeHandler.GetUnlockedContent().Keys.First());
    }


    public void LogTutorialCompleteEvent()
    {
        FB.LogAppEvent(
            "TutorialComplete"
        );
    }
    public void LogAllLevelsCompleteEvent()
    {
        FB.LogAppEvent(
            "AllLevelsComplete"
        );
    }

    public void LogContentUnlockedEvent(string unlockedobject)
    {
        var parameters = new Dictionary<string, object>();
        parameters["unlockedobject"] = unlockedobject;
        FB.LogAppEvent(
            "ContentUnlocked"
        );
    }
    public void LogLevelStartedEvent(int levelnumber)
    {
        var parameters = new Dictionary<string, object>();
        parameters["levelnumber"] = levelnumber;
        FB.LogAppEvent(
            "LevelStarted"
        );
    }
    public void LogLevelCompleteEvent(int levelnumber)
    {
        var parameters = new Dictionary<string, object>();
        parameters["levelnumber"] = levelnumber;
        FB.LogAppEvent(
            "LevelComplete"
        );
    }
}
