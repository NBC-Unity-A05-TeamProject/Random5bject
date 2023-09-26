using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ObjectPoolingManager;

public class EnemySpawner : MonoBehaviour
{
    private float sqawnTime;
    ObjectPoolingManager manager;

    private void Start()
    {
        manager = ObjectPoolingManager.instance;
        sqawnTime = NextSpawnTime();
    }
    void FixedUpdate()
    {
        sqawnTime -= Time.deltaTime;
        if (sqawnTime < 0)
        {
            GameObject thisEnemy = manager.GetFromPool("Enemy");
            Spawn(thisEnemy);
            sqawnTime = NextSpawnTime();
        }
    }
    private void Spawn(GameObject thisEnemy)
    {
        thisEnemy.SetActive(true);
        Enemy param = thisEnemy.GetComponent<Enemy>();
        param.SetPosition();
        param.Sqawned();
    }
    private float NextSpawnTime()
    {
        return sqawnTime = 1f;
    }

}
