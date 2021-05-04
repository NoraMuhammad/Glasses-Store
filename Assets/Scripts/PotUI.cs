using UnityEngine;

public class PotUI : MonoBehaviour
{
    [SerializeField] GameObject model;
    [SerializeField] GameObject currentModel;
    public static GameObject currentPlacedModel;
    public void SetPotInPlace()
    {
        if (LevelUpgradeHandler.ShowPlantPotUpgrade)
        {
            if(PlayerPrefs.GetInt(GameStrings.UpgradedPotIndexString) == transform.GetSiblingIndex())
            {
                UpdateUpgrade();
            }
        }
    }
    public void UpdateUpgrade()
    {
        currentModel.SetActive(false);
        if(currentPlacedModel)
            currentPlacedModel.SetActive(false);
        currentPlacedModel = Instantiate(model, currentModel.transform.position, model.transform.rotation);

        PlayerPrefs.SetInt(GameStrings.UpgradedPotIndexString, transform.GetSiblingIndex());
    }
}
