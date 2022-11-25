using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settlement Object", menuName = "Settlement/New Settlement Object")]
public class SettlementObject : ScriptableObject
{
    public string stationName;
    public Faction faction;
    public NPC npcOwner;

    public StationType stationType;

    public Inventory inventory;

    public float loyalty = 100f;
    public float energy;
    public float security = 70f;
    public float prosperity;
    public float population;
    public float taxes;
    public float upkeep;
    public float taxRate = 0.015f;
    public float upkeepRate;
    public float housing;
    public bool isPlayerOwned = false;
    public float energyProduction = 1000f;

    public int[] buildingLevels = new int[5];
    public float[] buildTimers = new float[5];
    public bool[] upgrading = new bool[5];

    public float ShipPriceModifierPerSettlementType(StationType stationType)
    {
        switch (stationType)
        {
            case StationType.standard:

                return 1.0f;
            case StationType.military:

                return 1.2f;
            case StationType.industry:

                return 0.95f;
            case StationType.science:

                return 1.1f;
            case StationType.trade:

                return 0.9f;
            default:
                return 1.0f;
        }
    }
}

public enum StationType
{
    standard,
    military,
    industry,
    science,
    trade
}
