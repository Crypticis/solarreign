using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot Table", menuName = "Loot/Loot Table")]
public class LootTable : ScriptableObject
{
    [SerializeField]
    private int rolls; // How many times loot table will be rolled.

    [System.Serializable]
    private struct Items
    {
        public Item item;
        public float chance;
        public int minAmt, maxAmt;
    }

    [SerializeField]
    private Items[] items;

    public void RollTableToInventory(Inventory inventory)
    {
        // Init amount of items to add and roll vars
        int amountToAdd = 0;
        float roll;

        // Check if item array has items
        if (!items.IsNullOrEmpty())
        {
            // Generate values to fill return array for use ingame 
            for (int i = 0; i < items.Length; i++)
            {
                for (int j = 0; j < rolls; j++)
                {
                    // Roll for item drop
                    roll = Random.Range(0, 100);
                    if (roll < items[i].chance)
                    {
                        amountToAdd += Random.Range(items[i].minAmt, items[i].maxAmt);
                    }
                }

                // Add item and reset amount to add to zero for next item
                if (amountToAdd > 0)
                {
                    inventory.AddItem(items[i].item, amountToAdd);
                    amountToAdd = 0;
                }
            }
        }
    }
}
