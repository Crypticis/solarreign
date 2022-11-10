using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnlargeButtonOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector3 originalScale;
    public float enlargedScale = 1.1f;
    public float timeToScale = .1f;

    void Awake()
    {
        originalScale = new Vector3(1, 1, 1);
    }

    public void OnEnable()
    {
        LeanTween.scale(gameObject, originalScale, timeToScale).setIgnoreTimeScale(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale * enlargedScale, timeToScale).setIgnoreTimeScale(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.scale(gameObject, originalScale, timeToScale).setIgnoreTimeScale(true);
    }
}
