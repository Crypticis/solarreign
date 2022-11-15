using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory System/Item")]
public class Item : ScriptableObject
{
    public string Name;
    public ItemType type;
    public bool stackable;
    public float defaultPrice;

    public Sprite sprite;
    public int ID;

    //public bool equipped;
}

public enum ItemType
{
    crew,
    spaceMetal,
    ice,
    water,
    scrap,
    food,
    rawOres,
    gems,
    organics,
    gas,
    weapons,
    defenses,
    materials,
}
