using UnityEngine;

public class SimpleDraggable: MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging;
    private Vector3 originalPosition;

    private Tower mergeTarget = null;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(mousePosition.x + offset.x, mousePosition.y + offset.y, transform.position.z);
        }
        else if (mergeTarget != null)
        {
            MergeTowers();
            mergeTarget = null;
        }
    }

    void OnMouseDown()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
        isDragging = true;
        GetComponent<Collider2D>().isTrigger = true;
    }

    void OnMouseUp()
    {
        isDragging = false;
        GetComponent<Collider2D>().isTrigger = false; 
        GetComponent<Tower>().UpgradeTower();
        if (mergeTarget == null) 
        {
            transform.position = originalPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Tower otherTower = collision.gameObject.GetComponent<Tower>();
        if (otherTower != null && CanMergeWith(otherTower))
        {
            mergeTarget = otherTower;
        }
    }

    private bool CanMergeWith(Tower other)
    {
        Tower thisTower = GetComponent<Tower>();

        return thisTower.selectedTowerData.towerName == other.selectedTowerData.towerName && thisTower.level == other.level;
    }

    private void MergeTowers()
    {
    }
}