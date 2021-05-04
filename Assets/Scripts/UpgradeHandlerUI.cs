using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeHandlerUI : MonoBehaviour
{
    [SerializeField] GameObject tapToStartGO;
    [SerializeField] GameObject upgradeGO;

    [SerializeField] PotUI[] potsUI;

    private void Start()
    {
        if(LevelUpgradeHandler.ShowPlantPotUpgrade && !LevelUpgradeHandler.PlantPotIsShown)
        {
            EnablePotUpgrade();
        }
        else if (LevelUpgradeHandler.PlantPotIsShown)
        {
            DisablePotUpgrade();
        }
    }

    void EnablePotUpgrade()
    {
        tapToStartGO.SetActive(false);
        upgradeGO.SetActive(true);
    }
    void DisablePotUpgrade()
    {
        upgradeGO.SetActive(false);
        tapToStartGO.SetActive(true);

        for (int i = 0; i < potsUI.Length; i++)
        {
            potsUI[i].SetPotInPlace();
        }
    }
}
