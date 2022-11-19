using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{
    // Inventories that are accessed
    public Inventory settlementInventory;

    // SettlementShop store inventory
    public Inventory shopInventory;

    // SettlementShop ui
    public GameObject slotPrefab;
    public Transform shopUI;

    // This happens everytime the shop it opened up
    public void CreateShopSlots()
    {
        GameObject slot;

        for (int i = 0; i < shopUI.childCount; i++)
        {
            Destroy(shopUI.GetChild(i).gameObject);
        }

        for (int i = 0; i < shopInventory.itemSlots.Count; i++)
        {
            slot = Instantiate(slotPrefab, shopUI.transform);

            if (slot.TryGetComponent<ShopSlot>(out ShopSlot info))
            {
                info.shop = this;
                info.itemsInSlot = shopInventory.itemSlots[i];
            }
        }

        // Update shop once slots are created
        UpdateShop();
    }

    // Update Shop on buy or sell to reperesent shop inventory and player inventory contents
    public void UpdateShop()
    {
        ItemSlot itemMatch = new();
        // Loop through all shop slots that are a part of shopUI
       foreach (Transform slot in shopUI)
        {
            if (slot.TryGetComponent<ShopSlot>(out ShopSlot slotinfo))
            {
                // Check if item exists in player inventory and retrieve it
                itemMatch = StatManager.instance.playerInventory.itemSlots.FirstOrDefault(p => p.item == slotinfo.itemsInSlot.item);

                // Check if item was found in inventory and if so activate sell button
                slotinfo.sellButton.interactable = itemMatch != null;

                // Check if player has more money than current price of item and shop has supply
                slotinfo.buyButton.interactable = (StatManager.instance.currentMoney >= slotinfo.itemsInSlot.item.currentPrice) && (slotinfo.itemsInSlot.amount > 0);
            }

            // Update slot texts
            slotinfo.amountText.text = slotinfo.itemsInSlot.amount.ToString("0");
            slotinfo.priceText.text = slotinfo.itemsInSlot.item.currentPrice.ToString("$0.00");
            if (itemMatch != null)
                slotinfo.ownedAmountText.text = itemMatch.amount.ToString("0");
        }
        
    }

    public void Buy(Item item, int amount)
    {
        if (StatManager.instance.currentMoney >= item.currentPrice)
        {
            shopInventory.RemoveItem(item, amount);
            StatManager.instance.playerInventory.AddItem(item, amount);
            StatManager.instance.Trade.AddExp(1);
            StatManager.instance.currentMoney -= item.currentPrice;
            UpdateShop();
        }
    }

    public void Sell(Item item, int amount)
    {
        // Retrieve item slot in player inventory where the item is the same as the one in shop
        var itemSlotPlayerInventory = StatManager.instance.playerInventory.itemSlots.FirstOrDefault(itemslot => itemslot.item == item);

        // Check if player has item in inventory
        if (itemSlotPlayerInventory != null && itemSlotPlayerInventory.amount > 0)
        {
            shopInventory.AddItem(item, amount);
            StatManager.instance.playerInventory.RemoveItem(item, amount);
            StatManager.instance.Trade.AddExp(1);
            StatManager.instance.currentMoney += item.currentPrice;
            UpdateShop();
        }
    }
}