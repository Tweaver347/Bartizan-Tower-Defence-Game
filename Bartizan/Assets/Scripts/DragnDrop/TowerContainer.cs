using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerContainer : MonoBehaviour, IDropHandler
{
    private bool hasTower;
    private void Awake()
    {
        hasTower = false;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null || hasTower == false)
        {
            hasTower = true;
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<CanvasGroup>().alpha = 1f;
            eventData.pointerDrag.GetComponent<CanvasGroup>().blocksRaycasts = true;
            eventData.pointerDrag.GetComponent<DragandDrop>().enabled = false;
            eventData.pointerDrag.GetComponent<DragandDrop>().onPlace();

        }
    }
}
