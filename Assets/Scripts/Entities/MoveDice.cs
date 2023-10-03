using UnityEngine;

public class MoveDice : MonoBehaviour
{
    private float minForce = 3f;
    private float maxForce = 7f;
    private Rigidbody2D rb;
    private float minRotation = 0f;
    private float maxRotation = 180f;

    private Animator animator;
    private bool isSpawn = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        float rotationAmount = Random.Range(minRotation, maxRotation);
        transform.Rotate(0f, 0f, rotationAmount);

        animator = GetComponent<Animator>();
    }

    void OnMouseDown()
    {
        ApplyRandomForce();
    }
    void ApplyRandomForce()
    {
        rb.velocity = Vector2.zero;

        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        float randomForce = Random.Range(minForce, maxForce);

        rb.AddForce(randomDirection * randomForce, ForceMode2D.Impulse);
    }

    public void TransitionToNextAnimation()
    {
        animator.SetBool("isSpawn", true);
    }
}
