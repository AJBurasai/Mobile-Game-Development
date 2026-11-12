using UnityEngine;
using TMPro;

public class ClickerController : MonoBehaviour
{

    /* This Script handles basic functionality for the clicker button and its interactions */

    public UpgradesManager upgradesManager;
    public GameData gameData; 
    public TextMeshProUGUI potionsText;

    public void Update()        // update ui on every frame
    {
        potionsText.text = gameData.potions + " potions";
    }
   
    public void BrewPotion()        // make potion when cauldron clicked based on upgrade strength
    {
        gameData.potions += ClickStrength();
        gameData.totalCauldronTaps++;
        gameData.totalPotionsMade += ClickStrength();
    }

    public double ClickStrength()       // return value used for calculating how many potions to add to running total
    {
        return 1 + gameData.clickUpgradeLevel;
    }
}
