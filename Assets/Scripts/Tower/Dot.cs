using UnityEngine;

public class Dot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // 점의 색상을 설정
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    // 점의 위치를 설정
    public void SetLocalPosition(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
    }

    // 점의 활성/비활성 상태를 설정
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
