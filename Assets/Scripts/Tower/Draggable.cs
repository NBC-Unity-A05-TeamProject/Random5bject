using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 originalPosition;
    private Tower tower;
    public GameObject towerPrefab;
    void Start()
    {
        tower = GetComponent<Tower>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Å¬¸¯");
        originalPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Camera.main.ScreenToWorldPoint(eventData.position);
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Tower otherTower = CanMergeWithOtherTower();

        if (otherTower != null)
            MergeWithOthertower(otherTower);
        else
            transform.position = originalPosition;
    }

    Tower CanMergeWithOtherTower()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

        foreach (var collider in colliders)
        {
            Tower otherTower = collider.GetComponent<Tower>();

            if (otherTower != null && otherTower.towerData == tower.towerData && otherTower.level == tower.level)
                return otherTower;
        }

        return null;
    }

    void MergeWithOthertower(Tower othertower)
    {
        GameObject newtowerObject = Instantiate(towerPrefab, transform.position, Quaternion.identity);
        Tower newtowerSpawner = newtowerObject.GetComponent<Tower>();

        if (newtowerSpawner != null)
        {
            Destroy(othertower.gameObject);
            Destroy(gameObject);

            newtowerSpawner.level = tower.level + 1;
        }
    }
}
