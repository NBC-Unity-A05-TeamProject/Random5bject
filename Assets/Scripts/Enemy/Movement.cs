using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private EnemyData[] enemyData;

    public float moveSpeed = 5f;
    private Vector3 moveDirection = Vector3.zero;

    public float MoveSpeed => moveSpeed;
    
    private void Update()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
