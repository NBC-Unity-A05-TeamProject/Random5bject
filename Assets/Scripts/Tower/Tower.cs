using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;

public class Tower : MonoBehaviour
{
    public TowerData[] towerData;
    public GameObject dotPrefab;

    public TowerData selectedTowerData;

    public float currentAtkDamage;
    public float currentAtkSpeed;
    private SpriteRenderer spriteRenderer;

    public int level = 1;
    private List<Dot> dots = new List<Dot>();

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float range = 5f;
    public float fireRate = 1f;
    private float nextFireTime = 0f;

    private const string EnemyTag = "Enemy";

    private void Start()
    {
        int randomTowerIndex = Random.Range(0, towerData.Length);
        selectedTowerData = towerData[randomTowerIndex];

        string towerName = selectedTowerData.towerName;
        gameObject.name = towerName;

        spriteRenderer = GetComponent<SpriteRenderer>();
        if(spriteRenderer != null && selectedTowerData.sprite != null)
        {
            spriteRenderer.sprite = selectedTowerData.sprite;
        }

        currentAtkDamage = selectedTowerData.towerAtkDamage;
        currentAtkSpeed = selectedTowerData.towerAtkSpeed;

        GenerateDots();
        UpdateDots();

        StartCoroutine(FireEnemies());
    }

    IEnumerator FireEnemies()
    {
        while (true)
        {
            if (Time.time >= nextFireTime)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
                Transform enemyInRange = null;

                foreach (var collider in colliders)
                {
                    if (collider.CompareTag(EnemyTag))
                    {
                        enemyInRange = collider.transform;
                        break;
                    }
                }

                if (enemyInRange != null)
                {
                    FireBullet(enemyInRange);
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }

            yield return null;
        }
    }


    void FireBullet(Transform enemy)
    {
        Vector2[] positions = CalculateDotPositions(level);

        for (int i = 0; i < level; i++)
        {
            Vector3 firePointPosition = firePoint.position + (Vector3)positions[i];

            GameObject bulletGO = Instantiate(bulletPrefab, firePointPosition, Quaternion.identity);
            Bullet bullet = bulletGO.GetComponent<Bullet>();

            if (bullet != null)
            {
                bullet.SetTarget(enemy);

                SpriteRenderer sr_bullet = bulletGO.GetComponent<SpriteRenderer>();
                if (sr_bullet != null)
                {
                    sr_bullet.color = selectedTowerData.dotColor;
                }
            }
        }
    }


    void GenerateDots()
    {
        Vector2[] positions = CalculateDotPositions(level);

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject dotObject = Instantiate(dotPrefab);
            dotObject.transform.SetParent(transform); 

            Dot dot = dotObject.GetComponent<Dot>();
            if (dot != null)
            {
                dot.SetColor(selectedTowerData.dotColor);
                dot.SetLocalPosition(positions[i]);
                dots.Add(dot);
            }
        }
    }

    void UpdateDots()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].SetActive(i < level);
        }
    }

    public void UpgradeTower()
    {
        if(level < 6)
        {
            level++;
            currentAtkDamage += selectedTowerData.upgradeAtkDamage;
            currentAtkSpeed += selectedTowerData.upgradeAtkSpeed;
            UpdateDots();
        }
    }

    private Vector2[] CalculateDotPositions(int towerLevel)
    {
        Vector2[] positions = null;

        switch (towerLevel)
        {
            case 1:
                positions = new Vector2[] { Vector2.zero };
                break;
            case 2:
                positions = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(0.3f, 0.3f) };
                break;
            case 3:
                positions = new Vector2[] { new Vector2(-0.3f, -0.3f), Vector2.zero, new Vector2(0.3f, 0.3f) };
                break;
            case 4:
                positions = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(0.3f, -0.3f), new Vector2(-0.3f, 0.3f), new Vector2(0.3f, 0.3f) };
                break;
            case 5:
                positions = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(0.3f, -0.3f), Vector2.zero, new Vector2(-0.3f, 0.3f), new Vector2(0.3f, 0.3f) };
                break;
            case 6:
                positions = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(-0.3f, 0f), new Vector2(-0.3f, 0.3f), new Vector2(0.3f, -0.3f), new Vector2(0.3f, 0f), new Vector2(0.3f, 0.3f) };
                break;
            default:
                break;
        }

        return positions;
    }
}
 