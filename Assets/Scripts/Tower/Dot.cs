using UnityEngine;

public class Dot : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // ���� ������ ����
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }

    // ���� ��ġ�� ����
    public void SetLocalPosition(Vector3 localPosition)
    {
        transform.localPosition = localPosition;
    }

    // ���� Ȱ��/��Ȱ�� ���¸� ����
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
