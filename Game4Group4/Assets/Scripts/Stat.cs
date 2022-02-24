using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stat
{
    private float val;
    private float valChangeAmount;
    private float valMax;
    private float valMin;
    private int valUpgradeCost;
    private int valUpgradeCostIncrease;

    public Stat(float iVal, float iValChangeAmount, float iValMax, float iValMin, int iValUpgradeCost, int iValUpgradeCostIncrease)
    {
        val = iVal;
        valChangeAmount = iValChangeAmount;
        valMax = iValMax;
        valMin = iValMin;
        valUpgradeCost = iValUpgradeCost;
        valUpgradeCostIncrease = iValUpgradeCostIncrease;
    }

    public float Val
    {
        get { return val; }
        set { val = value; }
    }

    public float ValChangeAmount
    {
        get { return valChangeAmount; }
        set { valChangeAmount = value; }
    }
    public float ValMax
    {
        get { return valMax; }
        set { valMax = value; }
    }

    public float ValMin
    {
        get { return valMin; }
        set { valMin = value; }
    }

    public int ValUpgradeCost
    {
        get { return valUpgradeCost; }
        set { valUpgradeCost = value; }
    }

    public int ValUpgradeCostIncrease
    {
        get { return valUpgradeCostIncrease; }
        set { valUpgradeCostIncrease = value; }
    }
}
