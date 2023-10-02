using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class UpgradeAtk : MonoBehaviour
{
    private int price;
    private int level;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI priceTxt;
    [SerializeField] private Button upgradeButton;

    private void Start()
    {
        price = 100;
        level = 0;
        UpgradeUI();
    }
    public void OnUpgradeAtkButtonClick()
    {
        bool goldSpent = PlayerManager.Instance.SpendGold(price);
        if (goldSpent)
        {
            level++;
            price *= level;
            UpgradeUI();
        }
    }

    private void UpgradeUI()
    {
        levelTxt.text = "LV. " + level;
        priceTxt.text = price.ToString();
        if (level >= 5)
        {
            upgradeButton.interactable = false;
            priceTxt.text = "Max";
        }
        else
        {
            upgradeButton.interactable = true;
        }
    }
}
