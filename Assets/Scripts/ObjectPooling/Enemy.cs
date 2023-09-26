using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ObjectPoolingManager manager = ObjectPoolingManager.instance;
    EnemyManager enemyManager;
    private Rigidbody2D rigidbody;
    private int hp;
    private int damage;
    private float halfX = -6f;
    private float halfY = 3.2f;
    public bool isSqawn = false;
    [SerializeField]
    public Transform[] wayPos;
    [SerializeField] float speed = 5f;
    int wayPointNum = 0;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enemyManager = GetComponent<EnemyManager>();
    }
    void Start()
    {
        SetPosition();
    }
    private void Update()
    {
        if(isSqawn)
        {
            MovePath();
        }
    }
    public void Sqawned()
    {
        isSqawn = true;
    }
    public void SetPosition()
    {
        Vector2 defaultPosition = new Vector2(halfX,halfY);
        this.transform.position = defaultPosition;
    }
    public void MovePath()
    {
        transform.position = Vector2.MoveTowards(transform.position, wayPos[wayPointNum].transform.position, speed * Time.deltaTime);

        if (transform.position == wayPos[wayPointNum].transform.position)
            wayPointNum++;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hp -= damage;
        if(hp < 0 || collision.gameObject.CompareTag("end"))
        {
            wayPointNum = 0;
            this.gameObject.SetActive(false);
            isSqawn = false;
        }
    }
}
