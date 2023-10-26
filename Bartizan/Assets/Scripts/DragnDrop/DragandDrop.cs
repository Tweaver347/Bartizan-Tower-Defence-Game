using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragandDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransfrom;
    private CanvasGroup canvasGroup;

    public RectTransform returnPosition;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject towerPrefab;

    private void Awake()
    {
        rectTransfrom = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = .5f;
        canvasGroup.blocksRaycasts = false;
        
    }
    //Drags Object along mouse cusor
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransfrom.anchoredPosition += eventData.delta / canvas.scaleFactor;

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        rectTransfrom.anchoredPosition = returnPosition.anchoredPosition;
        
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
    }

    public void onPlace()
    {
        // duplicate tower in UI Location
        Instantiate(towerPrefab, returnPosition.anchoredPosition, Quaternion.identity);
        // Turn on Tower via Tower Type Script

    }

}



