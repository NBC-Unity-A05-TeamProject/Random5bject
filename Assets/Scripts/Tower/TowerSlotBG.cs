using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSlotBG : MonoBehaviour
{
    [SerializeField] private TowerSlot[] towerSlots;

    private void Start()
    {
        towerSlots = GetComponentsInChildren<TowerSlot>();
    }

    public void ActivateRandomTowerSlot()
    {
        List<TowerSlot> inactiveSlots = new List<TowerSlot>();

        foreach (TowerSlot slot in towerSlots)
        {
            GameObject tower = slot.transform.GetChild(0).gameObject;
            if (!tower.activeSelf)
            {
                inactiveSlots.Add(slot);
            }
        }
        if (inactiveSlots.Count > 0)
        {
            int randomIndex = Random.Range(0, inactiveSlots.Count);
            TowerSlot selectedSlot = inactiveSlots[randomIndex];
            GameObject tower = selectedSlot.transform.GetChild(0).gameObject;

            tower.SetActive(true);
            Tower towerScript = tower.GetComponent<Tower>();
            if (towerScript != null)
            {
                towerScript.Init();
            }
        }
    }
}
