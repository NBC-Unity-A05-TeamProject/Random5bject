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

    // 타워를 마우스로 드래그하면 호출
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

    // 마우스 클릭 시 호출
    private void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        isDragging = true;

        GetComponent<Collider2D>().isTrigger = true;

        transform.SetAsLastSibling();
    }

    // 마우스 버튼을 놓았을 때 호출
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

    // 다른 타워와의 충돌 감지 시 호출
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tower otherTower = collision.gameObject.GetComponent<Tower>();

        if (otherTower != null && CanMergeWith(otherTower))
            mergeTarget = otherTower;
    }

    // 두 타워를 병합
    private bool CanMergeWith(Tower other)
    {
        Tower thisTower = GetComponent<Tower>();

        return thisTower.towerData.name == other.towerData.name && thisTower.level == other.level;

    }

    // 다른 타워와 병합 가능한지 확인
    private void MergeTowers()
    {
        if (mergeTarget != null)
        {
            TowerManager.instance.MergeTowers(GetComponent<Tower>(), mergeTarget);
            mergeTarget = null;
        }
    }
}
