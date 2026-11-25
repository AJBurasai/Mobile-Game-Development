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

    [Header ("Upgrades")]
    public Upgrades clickUpgrade;


    void Start()        // temporary fix to a problem in the clicker controller script
    {
        StartUpgradeManager();
    }

    public void StartUpgradeManager()           // runs on game start, sets default values, make sure these get saved 
    {
        clickUpgradeBaseCost = 20;
        clickUpgradeCostMult = 1.5;
        UpdateClickUpgradeUI();
    }

    public double Cost()        // increases cost when upgrade is bought
    {
        return Math.Ceiling(clickUpgradeBaseCost * Math.Pow(clickUpgradeCostMult, clickerController.gameData.clickUpgradeLevel));       
    }

    public void UpdateClickUpgradeUI()      // update ui elements when called
    {
        clickUpgrade.LevelText.text = "Level: " + clickerController.gameData.clickUpgradeLevel.ToString();
        clickUpgrade.NameText.text = "+1 Potions per tap";
        clickUpgrade.CostText.text = "Cost: " + Cost() + " potions";
    }

    public void BuyUpgrade()            // buy upgrade, increase cost and upgrade level
    {
        if (clickerController.gameData.potions >= Cost())
        {
            clickerController.gameData.potions -= Cost();
            clickerController.gameData.clickUpgradeLevel +=1;
        }

        UpdateClickUpgradeUI();     // update ui when something is bought
    }
}
