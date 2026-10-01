using UnityEngine;
using TMPro;

public class ClickerController : MonoBehaviour
{

    /* This Script handles basic functionality for the clicker button and its interactions */

    public GameData gameData; 
    public TextMeshProUGUI potionsText;

    public void Update()
    {
        potionsText.text = gameData.potions + " potions";
    }
   
    public void BrewPotion()
    {
        gameData.potions += 1;
    }
}
