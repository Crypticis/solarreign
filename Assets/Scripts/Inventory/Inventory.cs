using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemSlot> itemSlots = new();
    [SerializeField] internal InventoryType type;

    public void AddItem(Item item, int amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == item);

        if (itemMatch != null)
        {
            itemMatch.amount += amount;
        }
        else
        {
            ItemSlot temp = new();
            temp.item = item;
            temp.amount = amount;
            itemSlots.Add(temp);
        }
    }

    public void RemoveItem(Item item, int amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == item);

        if (itemMatch != null)
        {
            switch (type)
            {
                case InventoryType.player:
                    if (itemMatch.amount - amount >= 0)
                    {
                        itemMatch.amount -= amount;
                        if (itemMatch.amount == 0)
                            itemSlots.Remove(itemMatch);
                    }
                    break;
                case InventoryType.shop:
                    if (itemMatch.amount - amount >= 0)
                        itemMatch.amount -= amount;
                    break;
            }
        }
    }

    public bool CheckIfInInventory(Item item)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == item);

        if (itemMatch != null)
        {
            return true;
        }
        return false;
    }

    public int CheckAmountInInventory(Item item)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == item);

        if (itemMatch != null)
        {
            return itemMatch.amount;
        }

        return 0;
    }


    [ContextMenu("Reset")]
    public void ClearInventory() => itemSlots.Clear();
}

public enum InventoryType
{
    player,
    shop,
    settlement
}
