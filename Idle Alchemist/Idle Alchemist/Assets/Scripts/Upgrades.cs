using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class Upgrades : MonoBehaviour
{
    /* this script handles the text and labels for upgrades */

    [Header("Click upgrade")]
    public TMP_Text clickUpgradeLevelText;
    public TMP_Text clickUpgradeNameText;
    public TMP_Text clickUpgradeCostText;

    [Header("Orb upgrade")]
    public TMP_Text orbUpgradeLevelText;
    public TMP_Text orbUpgradeNameText;
    public TMP_Text orbUpgradeCostText;
}
