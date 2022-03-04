using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButtons : MonoBehaviour
{

    public UpgradeType upgradeType;
    private TextMeshProUGUI costText;
    private TextMeshProUGUI statText;

    // Start is called before the first frame update
    void Start()
    {
        costText = transform.parent.GetChild(3).GetComponent<TextMeshProUGUI>();
        statText = transform.parent.GetChild(4).GetComponent<TextMeshProUGUI>();
        ChangeText();
        if (!Manager.buttons.ContainsKey(upgradeType))
        {
            Manager.buttons.Add(upgradeType, this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change(int sign)
    {
        Stat upgrade = Manager.stats[upgradeType];
        if (Manager.money - upgrade.ValUpgradeCost >= 0)
        {
            float newVal = upgrade.Val + (upgrade.ValChangeAmount * sign);
            if (newVal > upgrade.ValMax || newVal < upgrade.ValMin)
            {
                return;
            }
            int buyCost = upgrade.ValUpgradeCost;
            if(sign < 0)
            {
                buyCost = -(buyCost - upgrade.ValUpgradeCostIncrease);
            }
            Manager.Buy(buyCost);
            upgrade.Val += upgrade.ValChangeAmount * sign;
            upgrade.ValUpgradeCost += upgrade.ValUpgradeCostIncrease * sign;
            ChangeText();
        }
    }

    public void ChangeText()
    {
        costText.text = "Cost: " + Manager.stats[upgradeType].ValUpgradeCost.ToString();
        statText.text = "Stat: " + Manager.stats[upgradeType].Val.ToString();
    }

}
