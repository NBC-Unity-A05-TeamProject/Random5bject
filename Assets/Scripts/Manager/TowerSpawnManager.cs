using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnManager : MonoBehaviour
{
    public static TowerSpawnManager instance;
    public GameObject towerPrefab; 
    public GameObject spawnPointPrefab;
    private List<Transform> spawnPoints = new List<Transform>();
    public float startX = 0f; 
    public float startY = 0f; 

    void Start()
    {
        //GenerateSpawnPoints(5, 3); 
    }
    private void Awake()
    {
        instance = this;
    }

    public void SpawnRandomTower()
    {
        List<Transform> emptySpawnPoints = new List<Transform>();
        foreach (Transform spawnPoint in spawnPoints)
        {
            if (spawnPoint.childCount == 0)
            {
                emptySpawnPoints.Add(spawnPoint);
            }
        }
        if (emptySpawnPoints.Count == 0) return;

        Transform randomEmptySpawnPoint = emptySpawnPoints[Random.Range(0, emptySpawnPoints.Count)];

        Instantiate(towerPrefab, randomEmptySpawnPoint.position, Quaternion.identity, randomEmptySpawnPoint);
    }

    public void GenerateSpawnPoints(int width, int height)
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(startX + x * 1.5f, startY + y * 1.5f, 0);
                GameObject spawnPointInstance = Instantiate(spawnPointPrefab, position, Quaternion.identity);
                spawnPoints.Add(spawnPointInstance.transform);
            }
        }
    }

}
