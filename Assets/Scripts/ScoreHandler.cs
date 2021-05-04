using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] MoneyUI moneyUI;
    [SerializeField] NextUnlockUI nextUnlockUI;

    [SerializeField] SelfieCaptureHandler selfieHandler;
    [SerializeField] Animator[] starsAnimators;
    int BestScore;
    private void Awake()
    {
        BestScore = 15; //5(number of customizations) * 3 (best grade)
    }
    public enum ScoreGrade
    {
        Excellent = 3,
        Good = 2,
        Fair = 1,
        None = 0
    }
    int score = 0;
    int finalScore = 0;
    int bonus = 0;
    public void IncrementScore(ScoreGrade value)
    {
        score += (int)value;
    }
    void CalculateBonus()
    {
        if(selfieHandler.GetCurrentFocus() > 2.5f) //not blurred
        {
            bonus = 5;
        }
    }
    public void CalculateFinalScore()
    {
        CalculateBonus();

        score = ColorUI.colorFinalScore + PatternChangableUI.patternFinalScore + LensColor.lensColorFinalScore + DragableUI.EventFinalScore + DragableUI.JewelryFinalScore;
        finalScore = score/* + bonus*/;

        ShowScoreOnScreen();
    }
    void ShowScoreOnScreen()
    {
        //if(finalScore <= 3)
        {
            PlayStar1();
            starsAnimators[1].GetComponent<Star>().UpdateMoney();
        }
        /*else*/ if (finalScore > 3 && finalScore <= 6)
        {
            PlayStar1();
            Invoke("PlayStar2", 0.4f);
            starsAnimators[1].GetComponent<Star>().UpdateMoney();
        }
        else if (finalScore > 6 && finalScore <= 9)
        {
            PlayStar1();
            Invoke("PlayStar2", 0.4f);
            Invoke("PlayStar3", 0.8f);
            starsAnimators[1].GetComponent<Star>().UpdateMoney();
        }
    }
    void PlayStar1()
    {
        starsAnimators[0].Play("Star Animation");
    }
    void PlayStar2()
    {
        starsAnimators[1].Play("Star Animation");
    }
    void PlayStar3()
    {
        starsAnimators[2].Play("Star Animation");
    }

    public void SaveScore()
    {
        moneyUI.SaveMoney();
        nextUnlockUI.SaveUnlockProgress();
    }
}
