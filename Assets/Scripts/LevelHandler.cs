using Facebook.Unity;
using SO.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour
{
    [SerializeField] ScoreHandler scoreHandler;
    [SerializeField] EventSO OnLevelStart;
    [SerializeField] TMPro.TextMeshProUGUI dayText;
    public static bool LevelIsStarted;
    public static int lastSavedScene;

    private void Awake()
    {
        lastSavedScene = PlayerPrefs.GetInt(GameStrings.SceneIndexString);

        if (dayText)
            dayText.text = "Day " + (PlayerPrefs.GetInt(GameStrings.DayIndexString) + 1);
    }
    private void Start()
    {
        LevelIsStarted = false;
    }
    public void StartLevel()
    {
        OnLevelStart.Raise();
        LevelIsStarted = true;
    }
    public void LoadNextLevel()
    {


        GoToNextLevel();
    }
    public AsyncOperation GoToNextLevel()
    {
        int sceneIndex;

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {

            sceneIndex = PlayerPrefs.GetInt(GameStrings.SceneIndexString);

            if (sceneIndex == 0)
                sceneIndex++;
            else if (sceneIndex == SceneManager.sceneCountInBuildSettings)
                sceneIndex = 1;

            return SceneManager.LoadSceneAsync(sceneIndex);
        }
        else
        {
            if (PlayerPrefs.GetInt(GameStrings.DayIndexString) >= SceneManager.sceneCountInBuildSettings - 1)
            {
                sceneIndex = (SceneManager.GetActiveScene().buildIndex + Random.Range(1, SceneManager.sceneCountInBuildSettings)) % SceneManager.sceneCountInBuildSettings;
                if (sceneIndex == 0)
                    sceneIndex++;

                PlayerPrefs.SetInt(GameStrings.DayIndexString, PlayerPrefs.GetInt(GameStrings.DayIndexString) + 1);
                PlayerPrefs.SetInt(GameStrings.SceneIndexString, sceneIndex);
                PlayerPrefs.Save();
            }
            else
            {
                PlayerPrefs.SetInt(GameStrings.DayIndexString, PlayerPrefs.GetInt(GameStrings.DayIndexString) + 1);
                PlayerPrefs.SetInt(GameStrings.SceneIndexString, SceneManager.GetActiveScene().buildIndex + 1);
                PlayerPrefs.Save();

                sceneIndex = PlayerPrefs.GetInt(GameStrings.SceneIndexString);

            }

            if (sceneIndex == 0)
                sceneIndex++;
            else if (sceneIndex == SceneManager.sceneCountInBuildSettings)
                sceneIndex = 1;

            PlayerPrefs.SetInt(GameStrings.SceneIndexString, sceneIndex);
            PlayerPrefs.Save();

            return SceneManager.LoadSceneAsync(sceneIndex);
        }
    }
    private void OnDisable()
    {
        PlayerPrefs.SetInt(GameStrings.SceneIndexString, SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.Save();
    }
    // Unity will call OnApplicationPause(false) when an app is resumed
    // from the background
    void OnApplicationPause(bool pauseStatus)
    {
        // Check the pauseStatus to see if we are in the foreground
        // or background
        if (!pauseStatus)
        {
            //app resume
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
            else
            {
                //Handle FB.Init
                FB.Init(() => {
                    FB.ActivateApp();
                });
            }
        }
    }
}
