using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPoolingManager;

public class EnemySpawner : MonoBehaviour
{
    ObjectPoolingManager manager;
    private EnemyData[] enemyData;
    private EnemyData selectedEnemyData;
    [SerializeField]
    public Transform[] wayPos;
    private float spawnInterval = 1.0f;
    private int spawnedEnemies = 0;
    private int maxEnemies = 10;
    private int currentStage = 1;

    private void Start()
    {
        manager = ObjectPoolingManager.instance;
    }
    void Update()
    {
        if (spawnedEnemies < maxEnemies)
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0)
            {
                GameObject thisEnemy = manager.GetFromPool("Enemy_normal");
                Spawn(thisEnemy);
                spawnInterval = 1.0f;
            }
        }
    }

    private void Spawn(GameObject thisEnemy)
    {
        Enemy param = thisEnemy.GetComponent<Enemy>();
        param.SetData(thisEnemy);
        thisEnemy.SetActive(true);
        param.SetPosition(wayPos);
        spawnedEnemies++;
    }

    public void ResetSpawn()
    {
        spawnedEnemies = 0;
        maxEnemies = 10;
    }

}
