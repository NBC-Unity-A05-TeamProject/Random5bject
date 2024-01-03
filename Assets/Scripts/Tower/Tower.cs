using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // 타워 기본 데이터
    public TowerData towerData;

    // 타워 상태 변수
    public float currentAtkDamage;
    public float currentAtkSpeed;
    public int level = 1;

    // 타워 공격 관련 프리팹과 설정
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float range = 5f;

    // 타워 시각적 요소
    [SerializeField] private GameObject dotPrefab;
    private List<Dot> dots = new List<Dot>();
    private SpriteRenderer spriteRenderer;

    // 타워 적 탐지 및 공격 타이밍 관련
    private const string EnemyTag = "Enemy";
    private float nextFireTime = 0f;

    // 타워 레벨별 점 위치 설정
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

    // 타워의 정보와 업데이트를 수행
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

    // 적을 주기적으로 발견하고 총알을 발사하는 코루틴
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

    // 적을 향해 총알을 발사
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

    // 타워 레벨에 따라 점들을 생성
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

    // 타워 레벨에 따라 점들의 활성 상태를 업데이트
    private void UpdateDots()
    {
        for (int i = 0; i < dots.Count; i++)
        {
            dots[i].SetActive(i < level);
        }
    }

    // 타워 업그레이드
    public void UpgradeTower()
    {
        if (level < 6)
        {
            level++;
            UpdateDots();
        }
    }

    // 타워의 공격력를 업그레이드
    public void UpgradeAtkDamage(int level)
    {
        currentAtkDamage = towerData.towerAtkDamage + level * 2f;
    }

    // 타워의 공격 속도를 업그레이드
    public void UpgradeAtkSpeed(int level)
    {
        currentAtkSpeed = towerData.towerAtkSpeed + (level * 0.2f);
    }

    // 타워 레벨에 따라 점 위치 데이터를 초기화
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

    // 타워 레벨에 따라 점 위치를 계산
    private Vector2[] CalculateDotPositions(int towerLevel)
    {
        if (dotPositions.TryGetValue(towerLevel, out Vector2[] positions))
        {
            return positions;
        }
        return null;
    }

    // 사거리 내에 있는 적을 찾음
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
