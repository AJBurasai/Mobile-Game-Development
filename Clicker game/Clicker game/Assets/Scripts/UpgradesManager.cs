using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UpgradesManager : MonoBehaviour
{
    /* this script handles data and cost values for upgrades*/

    public enum UpgradeType { Click = 0, Orb = 1 }      // stores the types of upgrades, easy to ammend later when adding more, each button calls the required int for function

    public ClickerController clickerController;

    public double clickUpgradeBaseCost;     // base cost for click upgrade
    public double clickUpgradeCostMult;      // cost multiplyer for click upgrade
    public double orbUpgradeBaseCost;
    public double orbUpgradeCostMult;

    [Header("Upgrades")]
    public Upgrades clickUpgrade;
    public Upgrades orbUpgrade;

    void Start()
    {
        StartUpgradeManager();
    }

    public void StartUpgradeManager()           // runs on game start, sets default values, make sure these get saved 
    {
        clickUpgradeBaseCost = 20;
        clickUpgradeCostMult = 1.5;
        orbUpgradeBaseCost = 100;
        orbUpgradeCostMult = 1.8;
        UpdateClickUpgradeUI();
    }

    private double GetLevel(UpgradeType type)      // lookup functions to figure out which upgrade level to increase
    {
        switch (type)
        {
            case UpgradeType.Click: return clickerController.gameData.clickUpgradeLevel;
            case UpgradeType.Orb: return clickerController.gameData.orbUpgradeLevel;
            default: return 0;
        }
    }

    private void IncreaseLevel(UpgradeType type)        // ^^^^      
    {
        switch (type)
        {
            case UpgradeType.Click: clickerController.gameData.clickUpgradeLevel++; break;
            case UpgradeType.Orb: clickerController.gameData.orbUpgradeLevel++; break;
        }
    }

    private double GetBaseCost(UpgradeType type)           // lookup function for finding which upgrade base cost and mult to increase
    {
        return type == UpgradeType.Click ? clickUpgradeBaseCost : orbUpgradeBaseCost;
    }

    private double GetCostMult(UpgradeType type)        // ^^^^
    {
        return type == UpgradeType.Click ? clickUpgradeCostMult : orbUpgradeCostMult;
    }

    public double Cost()        // increases cost when upgrade is bought
    {
        return Math.Ceiling(clickUpgradeBaseCost * Math.Pow(clickUpgradeCostMult, clickerController.gameData.clickUpgradeLevel));
    }

    public double Cost(UpgradeType type)        // cost calc
    {
        return Math.Ceiling(GetBaseCost(type) * Math.Pow(GetCostMult(type), GetLevel(type)));
    }

    public bool CanAfford(UpgradeType type)     // check to see if player can afford upgrade
    {
        return clickerController.gameData.potions >= Cost(type);
    }

    public void BuyUpgrade(UpgradeType type)            // buy upgrade, increase cost and upgrade level
    {
        if (CanAfford(type))
        {
            clickerController.gameData.potions -= Cost(type);
            IncreaseLevel(type);
            clickerController.gameData.totalUpgradesPurchased++;
            // add haptic feedback here
        }

        UpdateClickUpgradeUI();     // update ui when something is bought
    }

    public void BuyUpgradeButton(int typeIndex)
    {
        BuyUpgrade((UpgradeType)typeIndex);
    }

    public void UpdateClickUpgradeUI()      // update appropriate ui elements when called
    {
        clickUpgrade.clickUpgradeLevelText.text = "Level: " + clickerController.gameData.clickUpgradeLevel.ToString();
        clickUpgrade.clickUpgradeNameText.text = "+1 Potions per tap";
        clickUpgrade.clickUpgradeCostText.text = "Cost: " + Cost(UpgradeType.Click) + " potions";

        clickUpgrade.orbUpgradeLevelText.text = "Level: " + clickerController.gameData.orbUpgradeLevel.ToString();
        clickUpgrade.orbUpgradeNameText.text = "+1 Potions per second";
        clickUpgrade.orbUpgradeCostText.text = "Cost: " + Cost(UpgradeType.Orb) + " potions";
    }
}