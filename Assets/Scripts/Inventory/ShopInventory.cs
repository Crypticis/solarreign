using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Shop")]
public class ShopInventory : Inventory
{
    public override void AddItem(Item _item, int _amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == _item);

        if (itemMatch != null)
        {
            itemMatch.amount += _amount;
        }
    }
    public override void RemoveItem(Item _item, int _amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == _item);

        if (itemMatch != null)
        {
            if (itemMatch.amount - _amount >= 0)
                itemMatch.amount -= _amount;
        }
    }
}
