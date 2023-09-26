using UnityEngine;

public class CreateTowerBtn : MonoBehaviour
{
    public TowerSlotBG towerSlotBG;

    public void OnCreateTowerButtonClick()
    {
        if (towerSlotBG != null)
        {
            towerSlotBG.ActivateRandomTowerSlot();
        }
    }
}
