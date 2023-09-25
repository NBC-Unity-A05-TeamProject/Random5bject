using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Droppable : MonoBehaviour, IDropHandler
{
    [SerializeField] private int to;
    public void OnDrop(PointerEventData eventData)
    {
        Draggable dragged = eventData.pointerDrag.GetComponent<Draggable>();

        switch (dragged.from)
        {
            case 1:
                switch (to)
                {
                    case 1:
                        Destroy(dragged);
                        break;
                }
                break;
        }

        Debug.Log(string.Format("dragged {0} to {1}", dragged.from, to));
        dragged.SetOriginalPosition(GetComponent<RectTransform>().anchoredPosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
