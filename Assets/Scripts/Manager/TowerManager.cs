using System;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager instance;
    public Tower selectedTower;

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

        int randomIndex = UnityEngine.Random.Range(0, TowerSpawnManager.instance.towerPrefabs.Length);
        GameObject newTowerObject = Instantiate(TowerSpawnManager.instance.towerPrefabs[randomIndex], newTowerPosition, Quaternion.identity, parentTransform);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.TowerMerge);

        Tower newTower = newTowerObject.GetComponent<Tower>();

        if (newTower != null)
        {
            TowerData selectedTowerData = newTowerObject.GetComponent<Tower>().towerData;
            newTower.towerData = selectedTowerData;

            int upgradesNeeded = Math.Max(tower1.level, tower2.level);

            for (int i = 0; i < upgradesNeeded && i < 6; i++)
                newTower.UpgradeTower();
        }
    }

    public void SelectTower(Tower tower)
    {
        selectedTower = tower;
    }
}
