using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModuleSlot : MonoBehaviour, IDropHandler
{
    public ModuleType moduleType;
    public ModuleSize moduleSize;
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
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(eventData.pointerDrag.GetComponent<ModuleSelectWeapon>() && moduleType == ModuleType.weapon && eventData.pointerDrag.GetComponent<ModuleSelectWeapon>().item.moduleSize == moduleSize)
            {
                eventData.pointerDrag.GetComponent<ModuleSelectWeapon>().overSlot = true;
                Select(eventData.pointerDrag.GetComponent<ModuleSelectWeapon>().item);
                mechanicUI.UpdateInventoryModules();
            }
            else if (eventData.pointerDrag.GetComponent<ModuleSelectDefense>() && moduleType == ModuleType.defense && eventData.pointerDrag.GetComponent<ModuleSelectDefense>().item.moduleSize == moduleSize)
            {
                eventData.pointerDrag.GetComponent<ModuleSelectDefense>().overSlot = true;
                Select(eventData.pointerDrag.GetComponent<ModuleSelectDefense>().item);
                mechanicUI.UpdateInventoryModules();
            }
            else if (eventData.pointerDrag.GetComponent<ModuleSelectUtility>() && moduleType == ModuleType.utility && eventData.pointerDrag.GetComponent<ModuleSelectUtility>().item.moduleSize == moduleSize)
            {
                eventData.pointerDrag.GetComponent<ModuleSelectUtility>().overSlot = true;
                Select(eventData.pointerDrag.GetComponent<ModuleSelectUtility>().item);
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
        defense,
        utility
    }
}
