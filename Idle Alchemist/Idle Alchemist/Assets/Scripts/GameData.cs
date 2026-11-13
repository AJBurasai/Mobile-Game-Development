using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameData : MonoBehaviour
{
    /* This script holds most public variables and things related to game data*/

    public double potions;
    public double clickUpgradeLevel;        // current level for click upgrade, increases cost per level
    public int totalUpgradesPurchased;
    public double totalPotionsMade;
    public double totalCauldronTaps;
    public double orbUpgradeLevel;

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
