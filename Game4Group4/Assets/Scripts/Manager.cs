using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UpgradeType
{
    Handling,
    LaunchSpeed,
    Speed,
    ReverseSpeed,
    Fuel,
    BoostSpeed,
    BoostFuel,
    Parachute,
    ParachuteSize,
    Lives
}

public class Manager : MonoBehaviour
{

    public static int money = 3;
    public static int refundMoney = 0;
    private static Text moneyText;
    public static Dictionary<UpgradeType, Stat> stats;
    public static Dictionary<UpgradeType, UpgradeButtons> buttons;
    public static bool paused;
    private static bool startRun;

    // Start is called before the first frame update
    void Awake()
    {
        if (!startRun)
        {
            startRun = true;
            stats = new Dictionary<UpgradeType, Stat>();
            buttons = new Dictionary<UpgradeType, UpgradeButtons>();
            stats.Add(UpgradeType.Handling, new Stat(1f, 1f, 5f, 1f, 2, 3));
            stats.Add(UpgradeType.LaunchSpeed, new Stat(1f, 1f, 100f, 1f, 1, 1));
            stats.Add(UpgradeType.Speed, new Stat(1f, 1f, 100f, 1f, 1, 1));
            stats.Add(UpgradeType.ReverseSpeed, new Stat(1f, 10f, 5f, 1f, 1, 1));
            stats.Add(UpgradeType.Fuel, new Stat(1f, .5f, 10f, 1f, 1, 1));
            stats.Add(UpgradeType.BoostSpeed, new Stat(1f, 1f, 100f, 1f, 1, 1));
            stats.Add(UpgradeType.BoostFuel, new Stat(1f, .5f, 10f, 1f, 1, 1));
            stats.Add(UpgradeType.Parachute, new Stat(0f, 1f, 1f, 0f, 3, 0));
            stats.Add(UpgradeType.ParachuteSize, new Stat(1f, 1f, 5f, 1f, 1, 1));
            stats.Add(UpgradeType.Lives, new Stat(1f, 1f, 3f, 1f, 1, 2));
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        GameObject upgradeCanvas = GameObject.Find("UpgradeCanvas");
        if(upgradeCanvas != null)
        {
            moneyText = upgradeCanvas.transform.GetChild(0).GetComponent<Text>();
            moneyText.text = "Money: " + money.ToString();
        }
    }

    public static void Buy(int amount)
    {
        money -= amount;
        refundMoney += amount;
        moneyText.text = "Money: " + money.ToString();
    }

    public static void ResetButton()
    {
        money += refundMoney;
        refundMoney = 0;
        moneyText.text = "Money: " + money.ToString();
        foreach (KeyValuePair<UpgradeType, Stat> entry in stats)
        {
            entry.Value.ValUpgradeCost -= Mathf.RoundToInt((entry.Value.Val - entry.Value.ValMin) / entry.Value.ValChangeAmount) * entry.Value.ValUpgradeCostIncrease;
            entry.Value.Val = entry.Value.ValMin;
            buttons[entry.Key].ChangeText();
        }
    }

    public static void ConfirmButton()
    {
        refundMoney = 0;
        foreach (KeyValuePair<UpgradeType, Stat> entry in stats)
        {
            entry.Value.ValMin = entry.Value.Val;
        }
    }

    public static void AddMoney(int add)
    {
        money += add;
        if (moneyText != null)
        {
            moneyText.text = "Money: " + money.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
