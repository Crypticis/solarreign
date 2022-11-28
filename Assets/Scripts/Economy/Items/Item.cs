using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public ItemType type;
    public bool stackable;
    public float defaultPrice;
    public float currentPrice;

    public Sprite sprite;
    public int ID;
    void OnEnable()
    {
        Name = name;
        currentPrice = defaultPrice;
    }
}

public enum ItemType
{
    crew,
    metal,
    ice,
    water,
    scrap,
    food,
    rawOres,
    organics,
    gas,
    weapons,
    defenses,
    materials,
    crystals,
    data,
    relic,
    utility
}
