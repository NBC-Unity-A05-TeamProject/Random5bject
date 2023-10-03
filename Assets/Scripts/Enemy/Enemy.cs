using System.Collections;
using TMPro;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private EnemyData[] enemyData;
    private EnemyData selectedEnemyData;

    private SpriteRenderer spriteRenderer;

    private LevelManager levelManager;
    ObjectPoolingManager manager = ObjectPoolingManager.instance;
    EnemySpawner enemySpawner;
    private Rigidbody2D rigidbody;
    private Movement movement;
    private Transform[] wayPoints;
    [SerializeField]
    private TextMeshProUGUI enemyHpText;
    private int hp;
    private int damage;
    private int currentIndex = 0;
    private int wayPointNum = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enemySpawner = GetComponent<EnemySpawner>();
        movement = GetComponent<Movement>();
    }
    private void Start()
    {
        
    }
    public void SetPosition(Transform[] wayPoints)
    {
        if (wayPoints == null || wayPoints.Length == 0)
            return;

        wayPointNum = wayPoints.Length;
        this.wayPoints = new Transform[wayPointNum];
        this.wayPoints = wayPoints;
        currentIndex = 0;
        transform.position = wayPoints[currentIndex].position;
        StartCoroutine("MovePath");
    }
    IEnumerator MovePath()
    {
        NextMoveTo();

        while(true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement.MoveSpeed) 
            {
                NextMoveTo();
            }
            yield return null;
        }
        
    }

    private void NextMoveTo()
    {
        if(currentIndex < wayPointNum - 1)
        {
            transform.position = wayPoints[currentIndex].position;
            currentIndex++;
            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            movement.MoveTo(direction);
        }
    }
    public void SetData(GameObject thisEnemy)
    {
        int randomEnemyIndex = 0;
        int rand = Random.Range(0, 101);
        if(rand <= 88)
        {
            randomEnemyIndex = 0;
        }
        else if(rand < 98 && rand > 88)
        {
            randomEnemyIndex = 1;
        }
        else
        {
            randomEnemyIndex = 2;
        }
        selectedEnemyData = enemyData[randomEnemyIndex];

        string enemyName = selectedEnemyData.enemyName;
        gameObject.name = enemyName;

        float enemySpeed = selectedEnemyData.enemySpeed;
        movement.moveSpeed = enemySpeed;

        hp = selectedEnemyData.enemyMaxHp;
        enemyHpText.text = hp.ToString();

        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && selectedEnemyData.sprite != null)
        {
            spriteRenderer.sprite = selectedEnemyData.sprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp -= damage;

        if (collision.gameObject.CompareTag("end"))
        {
            PlayerManager.Instance.DecreaseLife(1);
            wayPointNum = 0;
            this.gameObject.SetActive(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
        }

        if (hp <= 0)
        {
            levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.EnemyKilled();
            }
            this.gameObject.SetActive(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Dead);
            PlayerManager.Instance.score += 100;
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        enemyHpText.text = hp.ToString();

        if (hp <= 0)
        {
            this.gameObject.SetActive(false);
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Hit);
        }
    }
}
