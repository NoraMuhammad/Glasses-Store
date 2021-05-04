using SO;
using SO.Events;
using UnityEngine;

public class LensColor : Scoreable
{
    [SerializeField] Material patternMaterial;
    [SerializeField] Material initialMaterial;

    [SerializeField] EventSO OnChoosenFrameChanges;
    [SerializeField] GameObject NextButton;
    VariableSO<GameObject> choosenFrame;

    public static int lensColorFinalScore;

    public void ApplySelectedLensColor()
    {
        choosenFrame = (VariableSO<GameObject>)OnChoosenFrameChanges.Value;

        if (choosenFrame)
        {
            if (patternMaterial)
            {
                choosenFrame.Value.transform.Find("glasses").GetComponent<MeshRenderer>().material = patternMaterial;
            }

            CalculateScore();
        }

        if (!NextButton.activeInHierarchy)
            NextButton.SetActive(true);
    }

    public void ResetPattern()
    {
        if (choosenFrame)
        {
            choosenFrame.Value.transform.Find("glasses").GetComponent<MeshRenderer>().material = initialMaterial;
            lensColorFinalScore = 0;
        }
    }
    public static void CalculateScore()
    {
        if (lensColorFinalScore != 0)
        {
            if (lensColorFinalScore < currentColoringGrade)
                lensColorFinalScore = currentColoringGrade;
        }
        else
        {
            lensColorFinalScore = currentColoringGrade;
        }
    }
}
