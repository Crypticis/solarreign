using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public static LTDescr delay;
    [Multiline()]
    public string content;
    public string header;

    public void OnPointerEnter(PointerEventData eventData)
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            TooltipSystem.Show(content, header);
        }).setIgnoreTimeScale(true);

        TooltipSystem.instance.item = this.transform;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();

        TooltipSystem.instance.item = null;
    }

    public void OnMouseEnter()
    {
        delay = LeanTween.delayedCall(0.5f, () =>
        {
            TooltipSystem.Show(content, header);
        }).setIgnoreTimeScale(true);

        TooltipSystem.instance.item = this.transform;
    }

    public void OnMouseExit()
    {
        LeanTween.cancel(delay.uniqueId);
        TooltipSystem.Hide();

        TooltipSystem.instance.item = null;
    }

}
