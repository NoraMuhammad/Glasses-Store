using UnityEngine;
using UnityEngine.UI;

public class SelectableUI : MonoBehaviour
{
    [SerializeField] GameObject tutorialHand;
    [SerializeField] GameObject tutorialHand1;
    [SerializeField] GameObject onGlassTutorialHand;
    [SerializeField] Image[] ColorsImage;
    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
    }
    public void SelectUI(bool disableTutorial = false)
    {
        image.enabled = true;

        for (int i = 0; i < ColorsImage.Length; i++)
        {
            if (ColorsImage[i] != image)
                ColorsImage[i].enabled = false;
        }

        if(!TutorialHandler.TutorialEnded)
        {
            if (tutorialHand && tutorialHand.activeInHierarchy && !disableTutorial)
            {
                tutorialHand.SetActive(false);

                if (tutorialHand1 && !tutorialHand1.activeInHierarchy)
                    tutorialHand1.SetActive(true);
            }
        }

        if(GetComponent<DragableUI>() && onGlassTutorialHand && !onGlassTutorialHand.activeInHierarchy)
        {
            onGlassTutorialHand.SetActive(true);
        }

        if(GetComponent<Scoreable>())
        {
            GetComponent<Scoreable>().SetCurrentGrade();
        }
    }
    public void ShowTutorialHand()
    {
        if(!TutorialHandler.TutorialEnded)
        {
            tutorialHand.SetActive(true);
        }
    }
}