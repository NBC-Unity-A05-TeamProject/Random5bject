using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAtk : MonoBehaviour
{
    public Tower selectedTower;

    private int price;
    private int level;
    [SerializeField] private TextMeshProUGUI levelTxt;
    [SerializeField] private TextMeshProUGUI priceTxt;
    [SerializeField] private Button upgradeButton;

    void Start()
    {
        price = 100;
        level = 0;
    }

    public void OnUpgradeButtonClick()
    {
        if (selectedTower == null)
        {
            return;
        }

        bool goldSpent = PlayerManager.Instance.SpendGold(price);

        if (goldSpent)
        {
            level++;
            price *= level;

            TowerManager.instance.AddLevelTower(selectedTower, level);
            TowerManager.instance.UpgradeTowerAtk();
            UpgradeUI();
        }
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
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
