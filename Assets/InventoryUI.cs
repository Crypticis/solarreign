using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public List<GameObject> slots = new List<GameObject>();
    public GameObject inventoryPrefab;
    public Transform inventoryUI;

    private void OnEnable()
    {
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i]);
        }

        slots = new List<GameObject>();

        for (int i = 0; i < inventory.itemSlots.Count; i++)
        {
            GameObject slot = Instantiate(inventoryPrefab, inventoryUI.transform);
            slot.GetComponent<InventorySlot>().item = inventory.itemSlots[i].item;
            slot.GetComponent<InventorySlot>().amount = inventory.itemSlots[i].amount;

            slots.Add(slot);
        }
    }
}
