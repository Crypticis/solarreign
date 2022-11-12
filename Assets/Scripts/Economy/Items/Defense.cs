using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Defense", menuName = "Inventory System/Defense")]
public class Defense : Item
{
    public DefenseType defenseType;
    public ModuleSize moduleSize;

    public int hitpoints;
}
