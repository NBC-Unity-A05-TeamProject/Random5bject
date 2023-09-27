using UnityEngine;

public class Dot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer; // 스프라이트 렌더러

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetColor(Color color)
    {
        // 색상 설정
        spriteRenderer.color = color;
    }

    public void SetLocalPosition(Vector3 localPosition)
    {
        // 로컬 위치 설정
        transform.localPosition = localPosition;
    }

    public void SetActive(bool active)
    {
        // 활성/비활성 상태 설정
        gameObject.SetActive(active);
    }
}
