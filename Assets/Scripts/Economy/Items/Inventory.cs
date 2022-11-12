using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();

    public void AddItem(Item item, int amount)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if(item == itemSlots[i].item && item.stackable)
            {
                itemSlots[i].amount += amount;
                return;
            }
        }

        var temp = new ItemSlot();
        temp.item = item;
        temp.amount = amount;
        itemSlots.Add(temp);
    }

    public void RemoveItem(Item item, int amount)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (item == itemSlots[i].item)
            {
                itemSlots[i].amount -= amount;
                if(itemSlots[i].amount <= 0)
                {
                    itemSlots.RemoveAt(i);
                }
            }
        }
    }

    public bool CheckIfInInventory(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].item == item)
            {
                return true;
            }
        }

        return false;
    }

    public int CheckAmountInInventory(Item item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].item == item)
            {
                return itemSlots[i].amount;
            }
        }

        return 0;
    }

    [ContextMenu("Reset")]
    public void ClearInventory()
    {
        itemSlots.Clear();
    }
}
