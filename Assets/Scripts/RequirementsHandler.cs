using UnityEngine;
using UnityEngine.UI;

public class RequirementsHandler : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI counter;
    [SerializeField] Button getRequirementBtn;
    [SerializeField] Animator popupAnim;
    [SerializeField] Image requirementPopUp;
    [SerializeField] Sprite[] requirements;
    [SerializeField] Image[] requirementHintIcons;
    [SerializeField] GameObject nextButton;

    public static bool ShownSuccessfully = false;
    private void Awake()
    {
        nextButton.SetActive(false);
    }
    public Sprite GetRequirement(int index)
    {
        requirementHintIcons[index].sprite = requirements[index];

        return requirements[index];
    }
    public void DisableGetRequirementButton()
    {
        if (getRequirementBtn.interactable)
            getRequirementBtn.interactable = false;
    }
    public void EnableShownSuccessfully()
    {
        ShownSuccessfully = true;

        nextButton.SetActive(true);
        getRequirementBtn.interactable = false;
    }
    private void OnDisable()
    {
        ShownSuccessfully = false;
    }
}