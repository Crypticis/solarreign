using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Shop : MonoBehaviour
{
    public Inventory shopInventory;
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

        if (shopInventory != null)
        {
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
    }

    // Update Shop on buy or sell to reperesent shop inventory and player inventory contents
    private void UpdateShop()
    {
        ItemSlot itemMatch = new();
        // Loop through all shop slots that are a part of shopUI
        for (int i = 0; i < shopUI.childCount; i++)
        {
            var slot = shopUI.GetChild(i);

            if (slot.TryGetComponent<ShopSlot>(out ShopSlot slotinfo))
            {
                // Check if item exists in player inventory and retrieve it
                itemMatch = StatManager.instance.playerInventory.itemSlots.FirstOrDefault(p => p.item == slotinfo.itemsInSlot.item);

                // Check if item was found in inventory and if so activate sell button
                slotinfo.sellButton.interactable = itemMatch != null;

                // Check if player has more money than current price of item and shop has supply
                slotinfo.buyButton.interactable = (StatManager.instance.currentMoney >= slotinfo.itemsInSlot.item.currentPrice) && (slotinfo.itemsInSlot.amount > 0);
            }
            slotinfo.amountText.text = slotinfo.itemsInSlot.amount.ToString("0");
            slotinfo.priceText.text = slotinfo.itemsInSlot.item.currentPrice.ToString("$0.00");
            if (itemMatch != null)
                slotinfo.ownedAmountText.text = itemMatch.amount.ToString("0");
            else
                slotinfo.ownedAmountText.text = "0";
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

    public void SortByName()
    {
        List<ShopSlot> shopUiChildren = new();

        for (int i = 0; i < shopUI.childCount; i++)
            if (shopUI.GetChild(i).TryGetComponent<ShopSlot>(out ShopSlot slot))
             {
                shopUiChildren.Add(slot);
             }
        
        List<ShopSlot> sortedByName = shopUiChildren.OrderBy(p => p.itemsInSlot.item.name).ToList();
        for (int i = 0; i < sortedByName.Count; i++)
        {
            sortedByName[i].transform.SetSiblingIndex(i);
        }

    }
    public void SortByPrice()
    {
        List<ShopSlot> shopUiChildren = new();

        for (int i = 0; i < shopUI.childCount; i++)
            if (shopUI.GetChild(i).TryGetComponent<ShopSlot>(out ShopSlot slot))
            {
                shopUiChildren.Add(slot);
            }

        List<ShopSlot> sortedByName = shopUiChildren.OrderBy(p => p.itemsInSlot.item.currentPrice).ToList();
        for (int i = 0; i < sortedByName.Count; i++)
        {
            sortedByName[i].transform.SetSiblingIndex(i);
        }
    }
    public void SortByAmount()
    {
        List<ShopSlot> shopUiChildren = new();

        for (int i = 0; i < shopUI.childCount; i++)
            if (shopUI.GetChild(i).TryGetComponent<ShopSlot>(out ShopSlot slot))
            {
                shopUiChildren.Add(slot);
            }

        List<ShopSlot> sortedByName = shopUiChildren.OrderBy(p => p.itemsInSlot.amount).ToList();
        for (int i = 0; i < sortedByName.Count; i++)
        {
            sortedByName[i].transform.SetSiblingIndex(i);
        }
    }


}