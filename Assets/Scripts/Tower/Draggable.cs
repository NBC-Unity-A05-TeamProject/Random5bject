using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] public int from;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 originPositions;
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.7f;

    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta;
        //Debug.Log("OnDrag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        rectTransform.anchoredPosition = originPositions;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
    }

    public void SetOriginalPosition(Vector3 newPositions)
    {
        originPositions = newPositions;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        originPositions = rectTransform.anchoredPosition;
    }
    void Update()
    {
        
    }
}
