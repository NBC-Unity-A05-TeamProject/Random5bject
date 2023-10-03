using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingManager : MonoBehaviour
{
    [System.Serializable]
    public struct Pool
    {
        public GameObject prefab;
        public int capacity;
    }
    public static ObjectPoolingManager instance;
    Enemy enemy;
    public List<Pool> pools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        this.poolDictionary = new Dictionary<string, List<GameObject>>();

        Pool poolInfo;
        for (int i = 0; i < pools.Count; i++)
        {
            poolInfo = pools[i];
            poolDictionary.Add(poolInfo.prefab.name, new List<GameObject>());
            FillPool(poolInfo.prefab, poolInfo.capacity);
        }
    }
    private void FillPool(GameObject gameObject, int capacity)
    {
        List<GameObject> pool = poolDictionary[gameObject.name];
        for (int i = 0; i < capacity; i++)
        {
            GameObject newObject = Instantiate(gameObject);
            newObject.SetActive(false);
            pool.Add(newObject);
        }
    }
    public GameObject GetFromPool(string name)
    {
        List<GameObject> pool;
        Pool poolInfo;

        if (poolDictionary.TryGetValue(name, out pool))
        {
            poolInfo = pools.Find(info => info.prefab.name == name);
            for (int i = 0; i < poolInfo.capacity; i++)
            {
                if (pool[i].activeInHierarchy == false)
                {
                    return pool[i];
                }
            }
        }

        return null;
    }
}
