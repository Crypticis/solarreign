using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "New PlayerStatsObject/PlayerStatsObject")]
public class PlayerStatsObject : ScriptableObject
{
    [Header("Stats & Proficiences")]
    public int skillpoints;
    public Level level;

    [Header("Proficiences")]
    public Level Piloting;
    public Level Missile;
    public Level Projectile;
    public Level Energy;
    public Level Trade;

    [Header("Stats")]
    public int commandLevel = 0;
    public int tacticsLevel = 0;
    public int logisticsLevel = 0;
    public int productionLevel = 0;

    [Header("Fleet Stuff")]
    public float currentInFleet;
    public float maxInFleet;

    [Header("Inventory")]
    public Inventory playerInventory;

    [Header("Currency")]
    public float currentMoney = 0f;

    [Header("Voting Stuff")]
    public float influence;
    public int votes;

    [Header("Starting Stats")]
    public int startingPiloting;
    public int startingMissile;
    public int startingProjectile;
    public int startingEnergy;
    public int startingTrade;

    public int startingCommandLevel = 0;
    public int startingTacticsLevel = 0;
    public int startingLogisticsLevel = 0;
    public int startingProductionLevel = 0;

    public SelectCharacter.CharacterType selectedCharType;

    [Header("Relations")]
    public Relation[] relations;
}
