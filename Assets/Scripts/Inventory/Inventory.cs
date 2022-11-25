using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemSlot> itemSlots = new();
    public virtual void AddItem(Item _item, int _amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == _item);

        if (itemMatch != null)
        {
            itemMatch.amount += _amount;
        }
        else
        {
            ItemSlot temp = new ItemSlot
            {
                item = _item,
                amount = _amount
            };
            itemSlots.Add(temp);
        }
    }

    public virtual void RemoveItem(Item _item, int _amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == _item);

        if (itemMatch != null)
        {
            if (itemMatch.amount - _amount >= 0)
            {
                itemMatch.amount -= _amount;
                if (itemMatch.amount == 0)
                    itemSlots.Remove(itemMatch);
            }
        }
    }

    public bool CheckIfInInventory(Item _item)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == _item);

        if (itemMatch != null)
        {
            return true;
        }
        return false;
    }

    public int CheckAmountInInventory(Item _item)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == _item);

        if (itemMatch != null)
        {
            return itemMatch.amount;
        }

        return 0;
    }


    [ContextMenu("Reset")]
    public void ClearInventory() => itemSlots.Clear();
}