using System.Collections.Generic;
using UnityEngine;

public class TowerSpawnManager : MonoBehaviour
{
    public static TowerSpawnManager instance;
    public GameObject towerPrefab; // 타워 프리팹에 대한 참조
    public GameObject spawnPointPrefab;
    private List<Transform> spawnPoints = new List<Transform>();
    public float startX = 0f; // 시작 x 좌표
    public float startY = 0f; // 시작 y 좌표

    void Start()
    {
        GenerateSpawnPoints(5, 3); // 가로 3, 세로 5의 격자 패턴으로 스폰 포인트를 생성합니다.
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

    private void GenerateSpawnPoints(int width, int height)
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
