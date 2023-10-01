using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;

    public Transform towerSpawnPosition;

    private void Awake()
    {
        instance = this;
    }

    public void MergeTowers(Tower tower1, Tower tower2)
    {
        if (tower1.transform.parent == null)
            return;

        Transform parentTransform = tower2.transform.parent;
        Vector3 newTowerPosition = parentTransform.position;

        Destroy(tower1.gameObject);
        Destroy(tower2.gameObject);

        GameObject newTowerObject = Instantiate(TowerSpawnManager.instance.towerPrefab, newTowerPosition, Quaternion.identity, parentTransform);

        Tower newTower = newTowerObject.GetComponent<Tower>();

        if (newTower != null)
        {
            int index = Array.IndexOf(newTower.towerData, tower1.selectedTowerData);

            if (index >= 0)
            {
                newTower.selectedTowerData = tower1.selectedTowerData;

                Debug.Log("Before Upgrade - TowerManager 1: " + tower1.level);
                Debug.Log("Before Upgrade - TowerManager 2: " + tower2.level);

                int upgradesNeeded = Math.Max(tower1.level, tower2.level);

                for (int i = 0; i < upgradesNeeded && i < 6; i++)
                    newTower.UpgradeTower();

                Debug.Log("After Upgrade - TowerManager New : " + newTower.level);
            }
        }
    }
}
