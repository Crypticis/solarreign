using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class Inventory : ScriptableObject
{
    public List<ItemSlot> itemSlots = new List<ItemSlot>();
    [SerializeField] private InventoryType type;

    public virtual void AddItem(Item item, int amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == item);

        if (itemMatch != null)
        {
            itemMatch.amount += amount;
        }
        else
        {
            var temp = new ItemSlot();
            temp.item = item;
            temp.amount = amount;
            itemSlots.Add(temp);
        }
    }

    public virtual void RemoveItem(Item item, int amount)
    {
        var itemMatch = itemSlots.FirstOrDefault(itemToCheck => itemToCheck.item == item);

        if (itemMatch != null)
        {
            switch (type)
            {
                case InventoryType.player:
                    itemMatch.amount -= amount;
                    if (itemMatch.amount == 0)
                        itemSlots.Remove(itemMatch);
                    break;
                case InventoryType.shop:
                    if (itemMatch.amount - amount >= 0)
                        itemMatch.amount -= amount;
                    break;
                default:
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

    public IEnumerable<ItemSlot> SortAlphabeticallyByName()
    {
        IEnumerable<ItemSlot> sortedByName = itemSlots.OrderBy(p => p.item.name);
        return sortedByName;
    }

    public IEnumerable<ItemSlot> SortByAmountDescendning()
    {
        IEnumerable<ItemSlot> sortedByAmountDescending = from num in itemSlots
                                                         orderby num descending
                                                         select num;
        return sortedByAmountDescending;
    }

    public IEnumerable<ItemSlot> SortByAmountAscending()
    {
        IEnumerable<ItemSlot> sortedByAmountAscending = from num in itemSlots
                                                        orderby num ascending
                                                        select num;
        return sortedByAmountAscending;
    }

    [ContextMenu("Reset")]
    public void ClearInventory()
    {
        itemSlots.Clear();
    }
}

public enum InventoryType
{
    player,
    shop
}
