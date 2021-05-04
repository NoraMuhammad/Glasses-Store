using UnityEngine;
using UnityEngine.UI;

public class PopupHandler : MonoBehaviour
{
    [SerializeField] RequirementsHandler requirementsHandler;
    [SerializeField] Image popup1;
    [SerializeField] Image popup2;
    [SerializeField] Image popup3;

    public void AssignPopup1()
    {
        popup1.sprite = requirementsHandler.GetRequirement(0);
        popup1.gameObject.SetActive(true);
    }
    public void AssignPopup2()
    {
        popup2.sprite = requirementsHandler.GetRequirement(1);
        popup2.gameObject.SetActive(true);
    }
    public void AssignPopup3()
    {
        popup3.sprite = requirementsHandler.GetRequirement(2);
        popup3.gameObject.SetActive(true);

        requirementsHandler.EnableShownSuccessfully();
    }
    public void DisableTakeOrderButton()
    {
        requirementsHandler.DisableGetRequirementButton();
    }
}
