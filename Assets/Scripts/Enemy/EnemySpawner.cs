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
    private float sqawnTime;

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
            GameObject thisEnemy = manager.GetFromPool("Enemy_normal");
            Spawn(thisEnemy);
            sqawnTime = NextSpawnTime();
        }
    }
    private void Spawn(GameObject thisEnemy)
    {
        thisEnemy.SetActive(true);
        Enemy param = thisEnemy.GetComponent<Enemy>();
        param.SetPosition(wayPos);
        //param.Sqawned();
    }
    private float NextSpawnTime()
    {
        return sqawnTime = 1f;
    }

}
