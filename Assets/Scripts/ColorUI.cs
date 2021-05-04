using UnityEngine;
using UnityEngine.UI;

public class ColorUI : Scoreable
{
    [SerializeField] ColorSO colorSO;
    [SerializeField] Image colorImageUI;
    [SerializeField] Es.InkPainter.Sample.MousePainter painter;
    [SerializeField] Image paintSprayUI;

    public static int colorFinalScore;
    private void Awake()
    {
        colorImageUI.color = colorSO.color;
    }
    public void EnableColor()
    {
        painter.CHANGEBrushColor(colorSO.color);
        paintSprayUI.color = colorSO.color;
    }
    public void ResetPaint()
    {
        painter.ResetPaint();
        currentColoringGrade = 0;
    }
    public static void CalculateScore()
    {
        if (colorFinalScore != 0)
        {
            if (colorFinalScore < currentColoringGrade)
                colorFinalScore = currentColoringGrade;
        }
        else
        {
            colorFinalScore = currentColoringGrade;
        }
    }
}
