using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;
    public Tooltip tooltip;

    public Transform item;

    public void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if(item == null)
        {
            Hide();
        }

        if(item)
            if(item.gameObject.activeInHierarchy == false)
            {
                item = null;
                Hide();
            }
    }

    public static void Show(string content, string header = "")
    {
        instance.tooltip.SetText(content, header);
        instance.tooltip.gameObject.SetActive(true);
    }

    public static void Hide()
    {
        instance.tooltip.gameObject.SetActive(false);
    }
}
