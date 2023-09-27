using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Tower tower;
    public GameObject towerPrefab; // Tower 게임 오브젝트의 프리팹

    void Start()
    {
        tower = GetComponent<Tower>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("클릭");
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f); // Z 축 값을 0으로 설정
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Tower otherTower = CanMergeWithOtherTower();

        if (otherTower != null)
            MergeWithOthertower(otherTower);
        else
            transform.position = originalPosition; // 원래 위치로 돌아갑니다.
    }

    Tower CanMergeWithOtherTower()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (var collider in colliders)
        {
            Tower otherTower = collider.GetComponent<Tower>();

            if (otherTower != null && otherTower.towerData == tower.towerData && otherTower.level == tower.level)
                return otherTower; // 합칠 수 있는 동일한 속성 및 레벨의 타워가 있다면 해당 타워를 반환합니다.
        }

        return null; // 합칠 수 있는 동일한 속성 및 레벨의 타워가 없다면 null을 반환합니다.
    }

    void MergeWithOthertower(Tower othertower)
    {
        GameObject newtowerObject = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        Tower newtowerSpawner = newtowerObject.GetComponent<Tower>();

        if (newtowerSpawner != null)
        {
            Destroy(othertower.gameObject);   // 기존위치에 있던 병합될 대상탑 삭제 
            Destroy(gameObject);  // 드래그 중인 스폰포인트의 기존탑 삭제 

            newtowerSpawner.level = tower.level + 1;   // 병합된 결과물은 원래 레벨보다 한단계 상승 
        }
    }
}
