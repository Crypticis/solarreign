using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementTrader : MonoBehaviour
{
    public Inventory inventory;
    //public List<GameObject> slots = new List<GameObject>();
    public GameObject slotPrefab;

    public ShopItem[] shopItems;

    //public ItemDatabaseObject database;

    public Transform shopUI;

    public Item item;

    public void Start()
    {
        for (int i = 0; i < shopItems.Length; i++)
        {
            shopItems[i].currentPrice = shopItems[i].item.defaultPrice * (1 + Random.Range(.1f, .5f));
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("123");
            inventory.AddItem(item, 1);
        }

        if (Input.GetKeyDown(KeyCode.N))
        {
            UpdateSlots();
        }
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < shopUI.childCount; i++)
        {
            Destroy(shopUI.GetChild(i).gameObject);
        }

        //slots = new List<GameObject>();

        for (int i = 0; i < shopItems.Length; i++)
        {
            GameObject slot = Instantiate(slotPrefab, shopUI.transform);
            slot.GetComponent<ShopSlot>().trader = this;

            //slots.Add(slot);

            for (int j = 0; j < inventory.itemSlots.Count; j++)
            {
                if(inventory.itemSlots[j].item == shopItems[i].item)
                {
                    slot.GetComponent<ShopSlot>().item = inventory.itemSlots[j].item;
                    slot.GetComponent<ShopSlot>().amount = inventory.itemSlots[j].amount;
                    break;
                } 
                else
                {
                    slot.GetComponent<ShopSlot>().item = shopItems[i].item;
                    slot.GetComponent<ShopSlot>().amount = 0;
                }
            }
        }
    }

    [System.Serializable]
    public struct ShopItem
    {
        public Item item;
        public float currentPrice;
    }
}
