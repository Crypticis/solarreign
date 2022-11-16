using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Inventory System/Recipe")]
public class Recipe : ScriptableObject
{
    public RequiredItem[] requiredItems;

    public ProducedItem[] producedItems;

    [System.Serializable]
    public struct RequiredItem
    {
        public Item requiredItem;
        public int amount;
    }

    [System.Serializable]
    public struct ProducedItem
    {
        public Item producedItem;
        public bool amountIsRange;
        public int min, max;
        public int amount;
    }
}
