using UnityEngine;
using TMPro;

public class ClickerController : MonoBehaviour
{

    /* This Script handles basic functionality for the clicker button and its interactions */

    public UpgradesManager upgradesManager;
    public GameData gameData; 
    public TextMeshProUGUI potionsText;

    public void Start()
    {
        upgradesManager.StartUpgradeManager();
    }
    public void Update()
    {
        potionsText.text = gameData.potions + " potions";
    }
   
    public void BrewPotion()
    {
        gameData.potions += ClickStrength();
    }

    public double ClickStrength()
    {
        return 1 + gameData.clickUpgradeLevel;
    }
}
