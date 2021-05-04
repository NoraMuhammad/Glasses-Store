using SO.Events;
using UnityEngine;
using UnityEngine.UI;

public class LoadingSceneBar : MonoBehaviour
{
    [SerializeField] EventSO onGameStarted;
    [SerializeField] LevelHandler levelHandler;
    [SerializeField] Slider slider;
    private void Awake()
    {
        onGameStarted.Raise();
    }
    public void LoadScene()
    {
        levelHandler.GoToNextLevel();
    }
}
