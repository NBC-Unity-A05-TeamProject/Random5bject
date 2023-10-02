using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(TowerSpawnManager.instance != null)
        {
            TowerSpawnManager.instance.GenerateSpawnPoints(5, 3);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
