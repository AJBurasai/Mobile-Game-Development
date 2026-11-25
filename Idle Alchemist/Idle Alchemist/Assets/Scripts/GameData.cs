using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    /* This script holds most public variables and things related to game data*/

    [Header("Potions and upgrade levels")]
    public double potions;
    public double clickUpgradeLevel;        // current level for click upgrade, increases cost per level
    public double orbUpgradeLevel;

    [Header("Stats")]
    public int totalUpgradesPurchased;
    public double totalPotionsMade;
    public double totalCauldronTaps;
    

    public GameData()
    {
        potions = 0;
        clickUpgradeLevel = 0;
        totalUpgradesPurchased = 0;
        totalPotionsMade = 0;
        totalCauldronTaps = 0;
        orbUpgradeLevel = 0;
    }
}
