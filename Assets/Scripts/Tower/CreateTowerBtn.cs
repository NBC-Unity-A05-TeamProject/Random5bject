using UnityEngine;

public class CreateTowerBtn : MonoBehaviour
{
    public void OnCreateTowerButtonClick()
    {
        bool goldSpent = PlayerManager.Instance.SpendGold(100);
        AudioManager.instance.PlaySfx(AudioManager.Sfx.ButtonClick);
        if (goldSpent)
        {
            TowerSpawnManager.instance.SpawnRandomTower();
            AudioManager.instance.PlaySfx(AudioManager.Sfx.TowerCreate);
        }
    }
}
