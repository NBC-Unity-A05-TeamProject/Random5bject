using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Ÿ�� �⺻ ������
    public TowerData towerData;

    // Ÿ�� ���� ����
    public float currentAtkDamage;
    public float currentAtkSpeed;
    public int level = 1;

    // Ÿ�� ���� ���� �����հ� ����
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float range = 5f;

    // Ÿ�� �ð��� ���
    [SerializeField] private GameObject dotPrefab;
    private List<Dot> dots = new List<Dot>();
    private SpriteRenderer spriteRenderer;

    // Ÿ�� �� Ž�� �� ���� Ÿ�̹� ����
    private const string EnemyTag = "Enemy";
    private float nextFireTime = 0f;

    // Ÿ�� ������ �� ��ġ ����
    private Dictionary<int, Vector2[]> dotPositions;

    private void Awake()
    {
        InitializeDotPositions();
    }

    private void Start()
    {
        InitializeTower();
        StartCoroutine(FireEnemies());
    }

    // Ÿ���� ������ ������Ʈ�� ����
    private void InitializeTower()
    {
        gameObject.name = towerData.towerName;
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && towerData.sprite != null)
        {
            spriteRenderer.sprite = towerData.sprite;
        }

        GenerateDots();
        UpdateDots();
    }

    // ���� �ֱ������� �߰��ϰ� �Ѿ��� �߻��ϴ� �ڷ�ƾ
    private IEnumerator FireEnemies()
    {
        while (true)
        {
            if (Time.time >= nextFireTime)
            {
                Transform enemyInRange = FindEnemyInRange();
                if (enemyInRange != null)
                {
                    FireBullet(enemyInRange);
                    nextFireTime = Time.time + 1f / currentAtkSpeed;
                }
            }

            yield return null;
        }
    }

    // ���� ���� �Ѿ��� �߻�
    private void FireBullet(Transform enemy)
    {
        Vector2[] positions = CalculateDotPositions(level);

        for (int i = 0; i < level; i++)
        {
            Vector3 firePointPosition = firePoint.position + (Vector3)positions[i];

            GameObject bulletGO = ObjectPoolingManager.instance.GetFromPool(bulletPrefab.name);

            if (bulletGO == null)
            {
                bulletGO = Instantiate(bulletPrefab, firePointPosition, Quaternion.identity);
            }
            else
            {
                bulletGO.transform.position = firePointPosition;
                bulletGO.transform.rotation = Quaternion.identity;
                bulletGO.SetActive(true);
            }

            Bullet bullet = bulletGO.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetTarget(enemy);
                bullet.tower = this;
                bullet.damage = currentAtkDamage;

                SpriteRenderer sr_bullet = bulletGO.GetComponent<SpriteRenderer>();
                if (sr_bullet != null)
                {
                    sr_bullet.color = towerData.dotColor;
                }
            }
        }
    }

    // Ÿ�� ������ ���� ������ ����
    private void GenerateDots()
    {
        Vector2[] positions = CalculateDotPositions(level);

        for (int i = 0; i < positions.Length; i++)
        {
            GameObject dotObject =
               Instantiate(dotPrefab);
            dotObject.transform.SetParent(transform);

            Dot dot = dotObject.GetComponent<Dot>();
            if (dot != null)
            {
                dot.SetColor(towerData.dotColor);
                dot.SetLocalPosition(positions[i]);
                dots.Add(dot);
            }
        }
    }

    // Ÿ�� ������ ���� ������ Ȱ�� ���¸� ������Ʈ
    private void UpdateDots()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].SetActive(i < level);
        }
    }

    // Ÿ�� ���׷��̵�
    public void UpgradeTower()
    {
        if (level < 6)
        {
            level++;
            UpdateDots();
        }
    }

    // Ÿ���� ���ݷ¸� ���׷��̵�
    public void UpgradeAtkDamage(int level)
    {
        currentAtkDamage = towerData.towerAtkDamage + level * 2f;
    }

    // Ÿ���� ���� �ӵ��� ���׷��̵�
    public void UpgradeAtkSpeed(int level)
    {
        currentAtkSpeed = towerData.towerAtkSpeed + (level * 0.2f);
    }

    // Ÿ�� ������ ���� �� ��ġ �����͸� �ʱ�ȭ
    private void InitializeDotPositions()
    {
        dotPositions = new Dictionary<int, Vector2[]>
        {
            [1] = new Vector2[] { Vector2.zero },
            [2] = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(0.3f, 0.3f) },
            [3] = new Vector2[] { new Vector2(-0.3f, -0.3f), Vector2.zero, new Vector2(0.3f, 0.3f) },
            [4] = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(0.3f, -0.3f), new Vector2(-0.3f, 0.3f), new Vector2(0.3f, 0.3f) },
            [5] = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(0.3f, -0.3f), Vector2.zero, new Vector2(-0.3f, 0.3f), new Vector2(0.3f, 0.3f) },
            [6] = new Vector2[] { new Vector2(-0.3f, -0.3f), new Vector2(-0.3f, 0f), new Vector2(-0.3f, 0.3f), new Vector2(0.3f, -0.3f), new Vector2(0.3f, 0f), new Vector2(0.3f, 0.3f) }
        };
    }

    // Ÿ�� ������ ���� �� ��ġ�� ���
    private Vector2[] CalculateDotPositions(int towerLevel)
    {
        if (dotPositions.TryGetValue(towerLevel, out Vector2[] positions))
        {
            return positions;
        }
        return null;
    }

    // ��Ÿ� ���� �ִ� ���� ã��
    private Transform FindEnemyInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag(EnemyTag))
            {
                return collider.transform;
            }
        }
        return null;
    }
}
