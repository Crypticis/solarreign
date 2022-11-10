using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public int amount;

    public Item item;

    public TMP_Text amountText;
    public TMP_Text nameText;

    public Image icon;

    public void Start()
    {
        icon.sprite = item.sprite;
    }

    public void Update()
    {
        amountText.text = amount.ToString("0");
        nameText.text = item.name;
    }
}
