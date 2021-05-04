using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHandler : MonoBehaviour
{
    [SerializeField] GameObject dragGO;
    public static bool TutorialEnded;
    private void Awake()
    {
        if (PlayerPrefs.GetInt(GameStrings.TutorialString) == 0)
            TutorialEnded = false;
        else
            TutorialEnded = true;
    }
    public void EndTutorial()
    {
        PlayerPrefs.SetInt(GameStrings.TutorialString, 1);
        TutorialEnded = true;
    }
    public void ShowDrag()
    {
        if(!TutorialEnded)
        {
            dragGO.SetActive(true);
        }
    }
}
