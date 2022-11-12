using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Inventory shopInventory;
    public Inventory playerInventory;
    public float price;
    public int amount;

    public Button buyButton;
    public Button sellButton;

    public SettlementTrader trader;

    public Item item;

    public TMP_Text amountText;
    public TMP_Text priceText;
    public TMP_Text nameText;
    public TMP_Text ownedAmountText;

    public Image icon;

    public float buyTimer = 0;
    public float incrementalBuyTimer = 0;
    public float sellTimer = 0;
    public float incrementalSellTimer = 0;
    public bool buyButtonIsDown, sellButtonIsDown;

    public void Start()
    {
        shopInventory = trader.inventory;
        icon.sprite = item.sprite;
        nameText.text = item.name;

        SlotCheck();
    }

    public void Update()
    {
        for (int i = 0; i < trader.shopItems.Length; i++)
        {
            if (item == trader.shopItems[i].item && price != trader.shopItems[i].currentPrice)
            {
                price = trader.shopItems[i].currentPrice;
            }
        }

        amountText.text = amount.ToString("0");
        priceText.text = price.ToString("$0.00");

        if(playerInventory.itemSlots.Count <= 0)
        {
            ownedAmountText.text = "0";
        }

        for (int i = 0; i < playerInventory.itemSlots.Count; i++)
        {
            if (playerInventory.itemSlots[i].item == item)
            {
                ownedAmountText.text = playerInventory.itemSlots[i].amount.ToString();
                break;
            }

            ownedAmountText.text = "0";

        }

        if (buyButtonIsDown)
        {
            incrementalBuyTimer += Time.unscaledDeltaTime;
            buyTimer += Time.unscaledDeltaTime;

            if (incrementalBuyTimer >= .1f && buyTimer >= 1)
            {
                Buy();
                incrementalBuyTimer = 0;
            }
        }

        if (sellButtonIsDown)
        {
            incrementalSellTimer += Time.unscaledDeltaTime;
            sellTimer += Time.unscaledDeltaTime;

            if (incrementalSellTimer >= .1f && sellTimer >= 1)
            {
                Sell();
                incrementalSellTimer = 0;
            }
        }
    }

    public void Buy()
    {
        if (StatManager.instance.playerStatsObject.currentMoney >= price && amount > 0)
        {
            var temp = new ItemSlot();

            for (int i = 0; i < shopInventory.itemSlots.Count; i++)
            {
                if(shopInventory.itemSlots[i].item == item)
                {
                    temp = shopInventory.itemSlots[i];
                }
            }

            shopInventory.RemoveItem(item, 1);
            amount = temp.amount;

            StatManager.instance.playerStatsObject.currentMoney -= price;

            for (int i = 0; i < trader.shopItems.Length; i++)
            {
                if(item == trader.shopItems[i].item)
                {
                    trader.shopItems[i].currentPrice *= 1.011f;
                    trader.shopItems[i].currentPrice -= ((StatManager.instance.playerStatsObject.Trade.currentLevel * .1f) * trader.shopItems[i].currentPrice);
                }
            }

            playerInventory.AddItem(item, 1);

            StatManager.instance.playerStatsObject.Trade.AddExp(1);

            SlotCheck();
        }
    }

    public void Sell()
    {
        var temp2 = new ItemSlot();

        for (int i = 0; i < playerInventory.itemSlots.Count; i++)
        {
            if (playerInventory.itemSlots[i].item == item)
            {
                temp2 = playerInventory.itemSlots[i];
            }
        }

        if (temp2.amount > 0)
        {
            shopInventory.AddItem(item, 1);

            var temp = new ItemSlot();

            for (int i = 0; i < shopInventory.itemSlots.Count; i++)
            {
                if (shopInventory.itemSlots[i].item == item)
                {
                    temp = shopInventory.itemSlots[i];
                }
            }

            //temp.amount++;
            amount = temp.amount;

            playerInventory.RemoveItem(item, 1);

            StatManager.instance.playerStatsObject.currentMoney += price;

            for (int i = 0; i < trader.shopItems.Length; i++)
            {
                if (item == trader.shopItems[i].item)
                {
                    if (trader.shopItems[i].currentPrice > trader.shopItems[i].item.defaultPrice * .1f)
                    {
                        trader.shopItems[i].currentPrice *= .99f;
                    }

                    trader.shopItems[i].currentPrice += ((StatManager.instance.playerStatsObject.Trade.currentLevel * .1f) * trader.shopItems[i].currentPrice);
                }
            }

            StatManager.instance.playerStatsObject.Trade.AddExp(1);

            SlotCheck();
        }
    }

    public void SlotCheck()
    {
        if(playerInventory.itemSlots.Count > 0)
        {
            for (int i = 0; i < playerInventory.itemSlots.Count; i++)
            {
                if (playerInventory.itemSlots[i].item == item)
                {
                    sellButton.interactable = true;
                    break;
                }
                else
                {
                    sellButton.interactable = false;
                }
            }
        } 
        else
        {
            sellButton.interactable = false;
        }

        if (amount > 0 && StatManager.instance.playerStatsObject.currentMoney >= price)
        {
            buyButton.interactable = true;
        } 
        else
        {
            buyButton.interactable = false;
        }
    }

    public void BuyButtonDown()
    {
        buyButtonIsDown = true;
    }

    public void BuyButtonUp()
    {
        buyButtonIsDown = false;
        buyTimer = 0;
    }

    public void SellButtonDown()
    {
        sellButtonIsDown = true;
    }

    public void SellButtonUp()
    {
        sellButtonIsDown = false;
        sellTimer = 0;
    }
}
