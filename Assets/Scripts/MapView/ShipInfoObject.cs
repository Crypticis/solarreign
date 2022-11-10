using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Info Object", menuName = "Module System/Ship")]
public class ShipInfoObject : ScriptableObject
{
    [Header("Equipment")]

    public Weapon[] weapons;

    public Defense[] defenses;

    [Header("Default Equipment")]

    public Weapon[] defaultWeapons;

    public Defense[] defaultDefenses;

    [ContextMenu("Reset Items")]
    public void Reset()
    {
        weapons = defaultWeapons;
        defenses = defaultDefenses;
    }
}
