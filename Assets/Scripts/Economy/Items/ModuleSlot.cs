using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour, IDropHandler
{
    public ModuleType moduleType;

    public Item item;

    public Image image;

    //public GameObject dropdown;

    public Inventory inventory;

    public Item previousItem;

    public MechanicUI mechanicUI;

    public GameObject moduleSelectPrefab;

    public TMP_Text itemNameText;

    public void Select(Item itemToSelect)
    {
        previousItem = item;
        item = itemToSelect;
        image.sprite = item.sprite;
        itemNameText.text = item.Name;

        for (int i = 0; i < transform.parent.childCount; i++)
        {
            if(this.transform == transform.parent.GetChild(i))
                mechanicUI.UpdateModules(i, this);
        }

        inventory.AddItem(previousItem, 1);
        inventory.RemoveItem(item, 1);

        //mechanicUI.ClearSelectModules();
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<ModuleSelect>().item.type == ItemType.weapons && moduleType == ModuleType.weapon)
            {
                eventData.pointerDrag.GetComponent<ModuleSelect>().overSlot = true;
                Select(eventData.pointerDrag.GetComponent<ModuleSelect>().item);
                mechanicUI.UpdateInventoryModules();
            }
            else if (eventData.pointerDrag.GetComponent<ModuleSelect>().item.type == ItemType.defenses && moduleType == ModuleType.defense)
            {
                eventData.pointerDrag.GetComponent<ModuleSelect>().overSlot = true;
                Select(eventData.pointerDrag.GetComponent<ModuleSelect>().item);
                mechanicUI.UpdateInventoryModules();
            }
            else
            {
                eventData.pointerDrag.GetComponent<ModuleSelect>().overSlot = false;
            }
        }
    }

    public enum ModuleType
    {
        weapon,
        defense
    }
}
