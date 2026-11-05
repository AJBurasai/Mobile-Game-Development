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

    public void Start()
    {
        clickUpgradeBaseCost = 20;
        clickUpgradeCostMult = 1.5;
    }

    public double Cost()
    {
        return clickUpgradeBaseCost * Math.Pow(clickUpgradeCostMult, clickerController.gameData.clickUpgradeLevel);
    }
}
