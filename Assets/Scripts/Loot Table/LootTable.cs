using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot/Loot Table")]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private int rolls; // How many times loot table will be rolled.

    [System.Serializable]
    private struct itemsInTable
    {
        public Item item;
        public float chance;
        public int minAmt, maxAmt;
    }

    [SerializeField]
    private itemsInTable[] items;

    public void RollTableToInventory(Inventory inventory)
    {
        // Init amount of items to add and roll vars
        int amountToAdd = 0;
        float roll;

        // Check if item array exists and has items
        if (!items.IsNullOrEmpty())
        {
            // Generate values to fill return array for use ingame 
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j < rolls; j++)
                {
                    // Roll for item drop
                    roll = Random.Range(0, 101);
                    if (roll <= items[i].chance)
                    {
                        amountToAdd += Random.Range(items[i].minAmt, items[i].maxAmt);
                    }
                }

                // Add item and reset amounttoadd to zero for next item
                if (amountToAdd > 0)
                {
                    inventory.AddItem(items[i].item, amountToAdd);
                    Ticker.Ticker.AddItem(amountToAdd + " " + items[i].item.name + " added to inventory.");
                    amountToAdd = 0;
                }
            }
        }
    }
}
