using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        if(TowerSpawnManager.instance != null)
        {
            TowerSpawnManager.instance.GenerateSpawnPoints(5, 3);
        }
    }

    void Update()
    {
        
    }
}
