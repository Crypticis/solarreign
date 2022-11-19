using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Shop")]
public class ShopInventory : Inventory
{
    void OnEnable()
    {
        type = InventoryType.shop;
    }
}
