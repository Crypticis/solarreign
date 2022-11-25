using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe/Refinery Recipe")]
class ProcessingRecipe : ScriptableObject
{
    [SerializeField] Item oreRequired;
    [SerializeField] ProducedMetal[] producedMetals;
    Item OreRequired { get => oreRequired; }
    ProducedMetal[] ProducedMetals { get => producedMetals; set => producedMetals = value; }

    [System.Serializable]
    struct ProducedMetal
    {
        public Item producedItem;
        public int amount;
    }
}
