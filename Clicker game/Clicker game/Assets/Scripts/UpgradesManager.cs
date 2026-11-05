using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UpgradesManager : MonoBehaviour
{
    /* this script handles data and cost values for upgrades*/

    public ClickerController clickerController;
    public double clickUpgradeBaseCost;     // base cost for click upgrade
    public double clickUpgradeCostMult;      // cost multiplyer for click upgrade

    public string clickUpgradeName;

    [Header ("Upgrades")]
    public Upgrades clickUpgrade;      

    public void StartUpgradeManager()
    {
        clickUpgradeName = "Potions per tap";
        clickUpgradeBaseCost = 20;
        clickUpgradeCostMult = 1.5;
        UpdateClickUpgradeUI();
    }

    public double Cost()
    {
        return clickUpgradeBaseCost * Math.Pow(clickUpgradeCostMult, clickerController.gameData.clickUpgradeLevel);
    }

    public void UpdateClickUpgradeUI()
    {
        clickUpgrade.LevelText.text = clickerController.gameData.clickUpgradeLevel.ToString();
        clickUpgrade.CostText.text = "Cost: " + Cost() + " potions";
        clickUpgrade.NameText.text = "+1 " + clickUpgradeName;
    }

    public void BuyUpgrade()
    {
        if (clickerController.gameData.potions >= Cost())
        {
            clickerController.gameData.potions -= Cost();
            clickerController.gameData.clickUpgradeLevel +=1;
        }

        UpdateClickUpgradeUI();
    }
}
