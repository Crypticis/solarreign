using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoundOnHover : MonoBehaviour, IPointerEnterHandler
{
    public string hoverSound;
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.instance.Play(hoverSound);
    }
}
