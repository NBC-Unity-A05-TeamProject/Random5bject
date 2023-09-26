using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateTowerBtn : MonoBehaviour
{
    public TowerSlotBG towerSlotBG;

    private void Start()
    {
    }

    public void OnCreateTowerButtonClick()
    {
        if (towerSlotBG != null)
        {
            towerSlotBG.ActivateRandomTowerSlot();
        }
    }
}
