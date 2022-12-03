using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

class Shop : MonoBehaviour
{
    [SerializeField] ShopInventory supply;
    public ShopInventory Supply { get; }

    [SerializeField] GameObject slotPrefab;
    [SerializeField] Transform shopUI;
    ShopSlot[] shopSlots;

    // This happens everytime the shop it opened up
    public void CreateShopUI()
    {
        GameObject slot;

        for (int i = 0; i < shopUI.childCount; i++)
        {
            Destroy(shopUI.GetChild(i).gameObject);
        }

        if (Supply != null)
        {
            for (int i = 0; i < Supply.itemSlots.Count; i++)
            {
                slot = Instantiate(slotPrefab, shopUI.transform);

                if (slot.TryGetComponent(out ShopSlot info))
                {
                    info.itemInSlot = Supply.itemSlots[i];
                    info.shop = this;
                }
            }

            // Update shop once slots are created
            UpdateShop();
        }
    }
    /// <summary>
    /// Updates shop ui to reflect changes after transactions and on shop cui creation.
    /// </summary>
    public void UpdateShop()
    {
        ItemSlot itemMatch = new();

        // Loop through all shop slots that are a part of shopUI
        for (int i = 0; i < shopUI.childCount; i++)
        {
            var shopSlot = shopUI.GetChild(i);

            if (shopSlot.TryGetComponent(out ShopSlot slotinfo))
            {
                // Check if item exists in player inventory and retrieve it
                itemMatch = StatManager.instance.playerInventory.itemSlots.FirstOrDefault(p => p.item == slotinfo.itemInSlot.item);

                // Check if item was found in inventory and if so activate sell button
                if (itemMatch != null)
                {
                    slotinfo.EnableSellButton();
                }
                else
                {
                    slotinfo.DisableSellButton();
                }

                // Check if player has more money than current price of item and shop has supply
                if ((StatManager.instance.currentMoney >= slotinfo.itemInSlot.item.currentPrice) && (slotinfo.itemInSlot.amount > 0))
                {
                    slotinfo.EnableBuyButton();
                }
                else
                {
                    slotinfo.DisableBuyButton();
                }
            }

            slotinfo.amountText.text = slotinfo.itemInSlot.amount.ToString("0");
            slotinfo.priceText.text = slotinfo.itemInSlot.item.currentPrice.ToString("$0.00");
          
            if (itemMatch != null)
            {
                slotinfo.ownedAmountText.text = itemMatch.amount.ToString("0");
            }
            else
            {
                slotinfo.ownedAmountText.text = "0";
            } 
        }
        SortByNameDescending();
    }
    /// <summary>
    /// Handles buy transactions in the shop
    /// </summary>
    /// <param name="item">Item being bought</param>
    /// <param name="amount">Amount of item</param>
    public void Buy(Item item, int amount)
    {
        if (StatManager.instance.currentMoney >= item.currentPrice)
        {
            Supply.RemoveItem(item, amount);
            StatManager.instance.playerInventory.AddItem(item, amount);
            StatManager.instance.Trade.AddExp(1);
            StatManager.instance.currentMoney -= item.currentPrice;
            UpdateShop();
        }
    }
    /// <summary>
    /// Handles sell transactions in the shop
    /// </summary>
    /// <param name="item">Item being sold</param>
    /// <param name="amount">Amount of item</param>
    public void Sell(Item item, int amount)
    {
        // Retrieve item slot in player inventory where the item is the same as the one in shop
        var itemSlotPlayerInventory = StatManager.instance.playerInventory.itemSlots.FirstOrDefault(itemslot => itemslot.item == item);

        // Check if player has item in inventory
        if (itemSlotPlayerInventory != null && itemSlotPlayerInventory.amount > 0)
        {
            Supply.AddItem(item, amount);
            StatManager.instance.playerInventory.RemoveItem(item, amount);
            StatManager.instance.Trade.AddExp(1);
            StatManager.instance.currentMoney += item.currentPrice;
            UpdateShop();
        }
    }
    /// <summary>
    /// Sorts shop items by name of items in alphabetical order
    /// </summary>
    public void SortByNameDescending()
    {
        List<ShopSlot> shopUiChildren = new();

        for (int i = 0; i < shopUI.childCount; i++)
            if (shopUI.GetChild(i).TryGetComponent<ShopSlot>(out ShopSlot slot))
             {
                shopUiChildren.Add(slot);
             }
        
        List<ShopSlot> sortedByName = shopUiChildren.OrderBy(p => p.itemInSlot.item.name).ToList();
        for (int i = 0; i < sortedByName.Count; i++)
        {
            sortedByName[i].transform.SetSiblingIndex(i);
        }

    }
    /// <summary>
    /// Sorts shop items by price descending
    /// </summary>
    public void SortByPriceDescending()
    {
        List<ShopSlot> shopUiChildren = new();

        for (int i = 0; i < shopUI.childCount; i++)
            if (shopUI.GetChild(i).TryGetComponent<ShopSlot>(out ShopSlot slot))
            {
                shopUiChildren.Add(slot);
            }

        List<ShopSlot> sortedByPrice = shopUiChildren.OrderBy(p => p.itemInSlot.item.currentPrice).ToList();
        for (int i = 0; i < sortedByPrice.Count; i++)
        {
            sortedByPrice[i].transform.SetSiblingIndex(i);
        }

        sortedByPrice.Reverse();
    }
    /// <summary>
    /// Sorts shop items by amount descending
    /// </summary>
    public void SortByAmountDescending()
    {
        List<ShopSlot> shopUiChildren = new();

        for (int i = 0; i < shopUI.childCount; i++)
            if (shopUI.GetChild(i).TryGetComponent<ShopSlot>(out ShopSlot slot))
            {
                shopUiChildren.Add(slot);
            }

        List<ShopSlot> sortedByAmount = shopUiChildren.OrderBy(p => p.itemInSlot.amount).ToList();
        for (int i = 0; i < sortedByAmount.Count; i++)
        {
            sortedByAmount[i].transform.SetSiblingIndex(i);
        }
    }
}