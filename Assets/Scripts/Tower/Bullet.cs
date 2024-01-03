using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Transform targetEnemy;
    private Vector3 direction;

    private const string EnemyTag = "Enemy";

    public Tower tower;

    public float damage;

    // 총알의 목표를 설정하고 총알을 목표로 이동시키는 코루틴 시작
    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy;
        direction = (targetEnemy.position - transform.position).normalized;

        StartCoroutine(MoveTowardsTarget());
    }

    // 총알을 목표로 이동시키는 코루틴
    private IEnumerator MoveTowardsTarget()
    {
        while (true)
        {
            if (targetEnemy == null || !targetEnemy.gameObject.activeInHierarchy)
            {
                gameObject.SetActive(false);
                yield break;
            }

            direction = (targetEnemy.position - transform.position).normalized;
            transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }
    }

    // 총알이 적과 충돌했을 때 호출되는 메서드

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage((int)tower.currentAtkDamage);
                AudioManager.instance.PlaySfx(AudioManager.Sfx.Bullet);

                gameObject.SetActive(false);
            }
            gameObject.SetActive(false);
        }
    }

}
