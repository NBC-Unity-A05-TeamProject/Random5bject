using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower : MonoBehaviour
{
    public TowerData[] towerData;
    private TowerData selectedTowerData;

    public void Init()
    {
        if (towerData != null && towerData.Length > 0)
        {
            int randomTowerIndex = Random.Range(0, towerData.Length);
            selectedTowerData = towerData[randomTowerIndex];

            string towerName = selectedTowerData.towerName;
            gameObject.name = towerName;

            Image towerImage = GetComponent<Image>();

            if (towerImage != null && selectedTowerData.sprite)
            {
                towerImage.sprite = selectedTowerData.sprite;
            }
            gameObject.SetActive(true);
            float attackDamage = selectedTowerData.towerAtkDamage;
            float attackSpeed = selectedTowerData.towerAtkSpeed;
        }
        else
        {
            Debug.Log("데이터 없음");
        }
    }
}
