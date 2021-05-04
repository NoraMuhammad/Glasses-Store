using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelUpgradeHandler : MonoBehaviour
{
    static LevelUpgradeHandler handler;
    public static LevelUpgradeHandler Instance => handler;
    [SerializeField] GameObject upgradeHint;
    [SerializeField] GameObject deactiveCamera;
    [SerializeField] GameObject enableCamera;

    [SerializeField] GameObject upgradeUI;
    [SerializeField] GameObject upgradeButton;

    public static bool ShowPlantPotUpgrade;
    public static bool PlantPotIsShown;

    [Seriablizable]
    public enum UpgradeType
    {
        PlantPot,
        CandyJar
    }
    public static string currentUpgrade;
    void Awake()
    {
        if(handler == null)
            handler = this;

        currentUpgrade = PlayerPrefs.GetString(GameStrings.PlantPotUpgrade);

        if (currentUpgrade == UpgradeType.PlantPot.ToString())
            ShowPlantPotUpgrade = true;
        else
            ShowPlantPotUpgrade = false;

        if (PlayerPrefs.GetInt(GameStrings.PlantPotShown) == 0)
            PlantPotIsShown = false;
        else
            PlantPotIsShown = true;


        if (upgradeHint)
            upgradeHint.SetActive(false);
    }
    public void SetPlantPotUpgrade()
    {
        PlantPotUpgradeIsDone();
    }
    public void PlantPotUpgradeIsDone()
    {
        Upgrade(UpgradeType.PlantPot);
    }
    public void ShowUpgradeSequence()
    {
        Invoke("ShowUpgrade", 0.5f);
    }
    void ShowUpgrade()
    {
        deactiveCamera.SetActive(false);
        enableCamera.SetActive(true);

        Invoke("ShowUI", 1.5f);
    }
    void ShowUI()
    {
        upgradeHint.SetActive(true);

        upgradeUI.SetActive(true);
        upgradeButton.SetActive(true);

        PlayerPrefs.SetInt(GameStrings.PlantPotShown, 1);
    }
    public void Upgrade(UpgradeType upgrade)
    {
        PlayerPrefs.SetString(GameStrings.PlantPotUpgrade, upgrade.ToString());
    }
    
    public Dictionary<string, object> GetUnlockedContent()
    {
        Dictionary<string, object> choosenUpgrade = new Dictionary<string, object>();
        choosenUpgrade[UpgradeType.PlantPot.ToString()] = PotUI.currentPlacedModel;
        
        return choosenUpgrade;
    }
}
