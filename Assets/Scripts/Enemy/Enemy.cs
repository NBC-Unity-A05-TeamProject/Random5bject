using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    ObjectPoolingManager manager = ObjectPoolingManager.instance;
    EnemySpawner enemySpawner;
    private Rigidbody2D rigidbody;
    private Movement movement;
    private Transform[] wayPoints;
    private int hp;
    private int damage;
    private int currentIndex = 0;
    private int wayPointNum = 0;
    public bool isSqawn = false;

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enemySpawner = GetComponent<EnemySpawner>();
        movement = GetComponent<Movement>();
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
