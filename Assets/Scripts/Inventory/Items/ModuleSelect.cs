using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModuleSelect : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Item item;
    public Canvas canvas;
    private RectTransform rectTransform;
    public Vector3 oldPosition;

    public Transform oldParent;

    public bool overSlot = false;

    public Image icon;
    public TMP_Text nameText;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetupModule()
    {
        icon.sprite = item.sprite;
        nameText.text = item.Name;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        oldParent = this.transform.parent;

        this.GetComponent<CanvasGroup>().blocksRaycasts = false;
        this.GetComponent<CanvasGroup>().alpha = .75f;

        transform.SetParent(canvas.transform);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!overSlot)
        {
            transform.SetParent(oldParent);
            this.GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnDrop(PointerEventData eventData)
    {

    }
}
