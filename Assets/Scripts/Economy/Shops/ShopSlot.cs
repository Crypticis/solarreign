using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

class ShopSlot : MonoBehaviour
{
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

    public Shop shopContainingSlot;
    public ItemSlot itemInSlot;

    public void Start()
    {
        // Set shopslot item sprite and name
        icon.sprite = itemInSlot.item.sprite;
        nameText.text = itemInSlot.item.name;

    }
    public void Update()
    {
        if (buyButtonIsDown)
        {
            incrementalBuyTimer += Time.unscaledDeltaTime;
            buyTimer += Time.unscaledDeltaTime;

            if (incrementalBuyTimer >= .1f && buyTimer >= 1)
            {
                shopContainingSlot.Buy(itemInSlot.item, 1);
                incrementalBuyTimer = 0;
            }
        }

        if (sellButtonIsDown)
        {
            incrementalSellTimer += Time.unscaledDeltaTime;
            sellTimer += Time.unscaledDeltaTime;

            if (incrementalSellTimer >= .1f && sellTimer >= 1)
            {
                shopContainingSlot.Sell(itemInSlot.item, 1);
                incrementalSellTimer = 0;
            }
        }
    }
    #region UI Methods
    public void EnableBuyButton()
    {
        buyButton.interactable = true;
    }

    public void DisableBuyButton()
    {
        buyButton.interactable = false;
    }
    public void EnableSellButton()
    {
        sellButton.interactable = true;
    }

    public void DisableSellButton()
    {
        sellButton.interactable = false;
    }
    void Buy()
    {
        shopContainingSlot.Buy(itemInSlot.item, 1);
    }

    void Sell()
    {
        shopContainingSlot.Sell(itemInSlot.item, 1);
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
