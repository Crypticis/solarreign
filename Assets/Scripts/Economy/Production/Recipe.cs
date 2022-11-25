using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe/Recipe")]
class Recipe : ScriptableObject
{
    // Leaving public just in case there are skills / tools to use less for a recipe or create more produced items
    [SerializeField] private RequiredItem[] requiredItems;
    [SerializeField] private ProducedItem[] producedItems;
    internal RequiredItem[] RequiredItems { get => requiredItems; set => requiredItems = value; }
    internal ProducedItem[] ProducedItems { get => producedItems; set => producedItems = value; }

    [System.Serializable]
    internal struct RequiredItem
    {
        public Item requiredItem;
        public int amount;
    }

    [System.Serializable]
    internal struct ProducedItem
    {
        public Item producedItem;
        public bool amountIsRange;
        public int min, max;
        public int amount;
    }
}
