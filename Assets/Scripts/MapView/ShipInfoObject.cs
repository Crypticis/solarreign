using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ship Info Object", menuName = "Module System/Ship")]
public class ShipInfoObject : ScriptableObject
{
    [Header("Information")]
    public string Name;
    [TextArea]
    public string description;
    public Faction faction;
    public int ID;
    public SkillRequirement[] skillRequirements;
    public ShipType shipType;
    public ModuleSize moduleSize;

    [Header("Shop Information")]
    public Sprite sprite;

    public float baseCost;

    public bool isOwned = false;

    [Header("Equipment")]

    public Weapon[] weapons;

    public Defense[] defenses;

    public Utility[] Utilities;

    [Header("Default Equipment")]

    public Weapon[] defaultWeapons;

    public Defense[] defaultDefenses;

    public Utility[] defaultUtilities;

    [ContextMenu("Reset Items")]
    public void Reset()
    {
        weapons = defaultWeapons;
        defenses = defaultDefenses;
    }

    [System.Serializable]
    public struct SkillRequirement
    {
        public Skill skill;
        public int skillLevel;
    }
}
