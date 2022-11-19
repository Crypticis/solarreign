using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementInfo : MonoBehaviour
{
    public Inventory settlementInventory;
    public SettlementType type;
    public string Name;
    public Faction faction;
    public NPC npc;
}

public enum SettlementType
{
    general,
    science,
    industry,
    trade,
    military,
}
