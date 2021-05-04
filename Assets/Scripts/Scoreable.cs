using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreable : MonoBehaviour
{
    public ScoreHandler.ScoreGrade choiceGrade;

    protected static int currentColoringGrade;
    private void Awake()
    {
        currentColoringGrade = 0;
    }

    public void SetCurrentGrade()
    {
        currentColoringGrade = (int)choiceGrade;
    }
}
