using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementInfo : MonoBehaviour
{
    public string Name;
    public Faction faction;
    public NPC npc;
    public SettlementType type;
}

public enum SettlementType
{
    general,
    science,
    industry,
    trade,
    military,
}