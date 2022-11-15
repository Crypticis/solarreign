using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory System/Weapon")]
public class Weapon : Item
{
    public WeaponType weaponType;
    public ModuleSize moduleSize;

    public float range;
    public float damage;
    public float firerate;
}

public enum ModuleSize
{
    small,
    medium,
    large
}
