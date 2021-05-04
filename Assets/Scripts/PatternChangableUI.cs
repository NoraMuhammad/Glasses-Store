using SO;
using SO.Events;
using UnityEngine;

public class PatternChangableUI : Scoreable
{
    Texture patternTexture;
    [SerializeField] Texture initialPattern;
    [SerializeField] EventSO OnChoosenFrameChanges;
    [SerializeField] GameObject NextButton;
    VariableSO<GameObject> choosenFrame;

    public static int patternFinalScore;

    public void ApplySelectedPattern()
    {
        choosenFrame = (VariableSO<GameObject>)OnChoosenFrameChanges.Value;

        if(choosenFrame)
        {
            if (gameObject.CompareTag("Dots"))
                patternTexture = choosenFrame.Value.GetComponent<Glasses>().dotsTexture;
            else if (gameObject.CompareTag("Lines"))
                patternTexture = choosenFrame.Value.GetComponent<Glasses>().linessTexture;

            if (choosenFrame.Value.transform.Find("frame").GetComponent<MeshRenderer>().materials.Length > 1 && patternTexture)
            {
                choosenFrame.Value.transform.Find("frame").GetComponent<MeshRenderer>().materials[1].SetTexture("_MainTex", patternTexture);
            }

            CalculateScore();
        }

        if (!NextButton.activeInHierarchy)
            NextButton.SetActive(true);
    }
    public void ResetPattern()
    {
        if(choosenFrame)
        {
            if (choosenFrame.Value.transform.Find("frame").GetComponent<MeshRenderer>().materials.Length > 1)
                choosenFrame.Value.transform.Find("frame").GetComponent<MeshRenderer>().materials[1].SetTexture("_MainTex", initialPattern);
            patternFinalScore = 0;
        }
    }

    public static void CalculateScore()
    {
        if (patternFinalScore != 0)
        {
            if (patternFinalScore < currentColoringGrade)
                patternFinalScore = currentColoringGrade;
        }
        else
        {
            patternFinalScore = currentColoringGrade;
        }
    }
}
