using UnityEngine;
using TMPro;

public class ClickerController : MonoBehaviour
{

    /* This Script handles basic functionality for the clicker button and its interactions */

    public UpgradesManager upgradesManager;
    public GameData gameData; 

    public TextMeshProUGUI potionsText;
    public TextMeshProUGUI potionsPerSecText;

    private float tickTimer = 0f;       // accumulates real time between auto-generation ticks

    public void Update()        // update ui on every frame
    {
        AutoGenerate();                 // auto generate potions based on upgrades
        potionsText.text = Mathf.FloorToInt((float)gameData.potions) + " potions";
        potionsPerSecText.text = PotionsPerSecond() + " potions/sec";
    }
   
    private void AutoGenerate()
    {
        tickTimer += Time.deltaTime;

        while (tickTimer >=1f)
        {
            tickTimer -= 1f;
            double amount = PotionsPerSecond();
            gameData.potions += amount;
            gameData.totalPotionsMade += amount;
        }
    }

    public void BrewPotion()        // make potion when cauldron clicked based on upgrade strength
    {
        double strength = ClickStrength();
        gameData.potions += strength;
        gameData.totalCauldronTaps++;
        gameData.totalPotionsMade += strength;
    }

    public double ClickStrength()       // return value used for potions per click
    { 
        return 1 + gameData.clickUpgradeLevel;
    }

    public double PotionsPerSecond()        // return value used for potions per second
    {
        return gameData.orbUpgradeLevel;
    }
}
