using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    ObjectPoolingManager manager;
    [SerializeField]
    public Transform[] wayPos;
    private float spawnInterval = 1.0f;
    private int spawnedEnemies = 0;
    private int maxEnemies = 10;

    private void Start()
    {
        manager = ObjectPoolingManager.instance;
    }
    void FixedUpdate()
    {
        if (spawnedEnemies < maxEnemies)
        {
            spawnInterval -= Time.deltaTime;
            if (spawnInterval <= 0)
            {
                GameObject thisEnemy = manager.GetFromPool("Enemy_normal");
                Spawn(thisEnemy);
                spawnInterval = 2f;
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
