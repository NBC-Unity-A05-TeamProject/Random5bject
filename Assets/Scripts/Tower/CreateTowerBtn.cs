using UnityEngine;

public class CreateTowerBtn : MonoBehaviour
{

    public void OnCreateTowerButtonClick()
    {
        
        TowerSpawnManager.instance.SpawnRandomTower();
    }
}
