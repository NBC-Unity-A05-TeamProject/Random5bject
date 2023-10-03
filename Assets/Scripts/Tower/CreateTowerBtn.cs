using UnityEngine;

public class CreateTowerBtn : MonoBehaviour
{
    public void OnCreateTowerButtonClick()
    {
        bool goldSpent = PlayerManager.Instance.SpendGold(100);
        if (goldSpent)
        {
            TowerSpawnManager.instance.SpawnRandomTower();
            AudioManager.instance.PlaySfx(AudioManager.Sfx.TowerCreate);
        }
    }
}
