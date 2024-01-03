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

    // �Ѿ��� ��ǥ�� �����ϰ� �Ѿ��� ��ǥ�� �̵���Ű�� �ڷ�ƾ ����
    public void SetTarget(Transform enemy)
    {
        targetEnemy = enemy;
        direction = (targetEnemy.position - transform.position).normalized;

        StartCoroutine(MoveTowardsTarget());
    }

    // �Ѿ��� ��ǥ�� �̵���Ű�� �ڷ�ƾ
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

    // �Ѿ��� ���� �浹���� �� ȣ��Ǵ� �޼���

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
