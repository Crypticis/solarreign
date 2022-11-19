using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Shop shop;
    public ItemSlot itemsInSlot;

    public Button buyButton;
    public Button sellButton;

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
        // Set shopslot item sprite and name
        icon.sprite = itemsInSlot.item.sprite;
        nameText.text = itemsInSlot.item.name;
    }
    public void Update()
    {
        if (buyButtonIsDown)
        {
            incrementalBuyTimer += Time.unscaledDeltaTime;
            buyTimer += Time.unscaledDeltaTime;

            if (incrementalBuyTimer >= .1f && buyTimer >= 1)
            {
                shop.Buy(itemsInSlot.item, 1);
                incrementalBuyTimer = 0;
            }
        }

        if (sellButtonIsDown)
        {
            incrementalSellTimer += Time.unscaledDeltaTime;
            sellTimer += Time.unscaledDeltaTime;

            if (incrementalSellTimer >= .1f && sellTimer >= 1)
            {
                shop.Sell(itemsInSlot.item, 1);
                incrementalSellTimer = 0;
            }
        }
    }

    #region UI Methods
    void Buy()
    {
        shop.Buy(itemsInSlot.item, 1);
    }

    void Sell()
    {
        shop.Sell(itemsInSlot.item, 1);
    }

    void BuyButtonDown()
    {
        buyButtonIsDown = true;
    }

    void BuyButtonUp()
    {
        buyButtonIsDown = false;
        buyTimer = 0;
    }

    void SellButtonDown()
    {
        sellButtonIsDown = true;
    }

    void SellButtonUp()
    {
        sellButtonIsDown = false;
        sellTimer = 0;
    }
    #endregion
}
