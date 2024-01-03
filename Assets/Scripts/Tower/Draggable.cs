using System.Collections;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging;
    private Vector3 originalPosition;

    private Tower mergeTarget = null;

    private void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(MoveTower());
    }

    // Ÿ���� ���콺�� �巡���ϸ� ȣ��
    private IEnumerator MoveTower()
    {
        while (true)
        {
            if (isDragging)
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
            }

            yield return null;
        }
    }

    // ���콺 Ŭ�� �� ȣ��
    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        isDragging = true;

        GetComponent<Collider2D>().isTrigger = true;

        transform.SetAsLastSibling();
    }

    // ���콺 ��ư�� ������ �� ȣ��
    private void OnMouseUp()
    {
        isDragging = false;

        GetComponent<Collider2D>().isTrigger = false;

        if (mergeTarget != null)
        {
            MergeTowers();
            return; 
        }

        transform.position = originalPosition;
    }

    // �ٸ� Ÿ������ �浹 ���� �� ȣ��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tower otherTower = collision.gameObject.GetComponent<Tower>();

        if (otherTower != null && CanMergeWith(otherTower))
            mergeTarget = otherTower;
    }

    // �� Ÿ���� ����
    private bool CanMergeWith(Tower other)
    {
        Tower thisTower = GetComponent<Tower>();

        return thisTower.towerData.name == other.towerData.name && thisTower.level == other.level;

    }

    // �ٸ� Ÿ���� ���� �������� Ȯ��
    private void MergeTowers()
    {
        if (mergeTarget != null)
        {
            TowerManager.instance.MergeTowers(GetComponent<Tower>(), mergeTarget);
            mergeTarget = null;
        }
    }
}
