using System.Collections;
using System.Collections.Generic;
//using UnityEditor;
using UnityEngine;
using UnitySteer.Behaviors;

public class SpaceStation : MonoBehaviour
{
    public SettlementObject settlementObject;

    float resourceTimeRate = 15f;
    float resourceTimer;

    public Building[] buildings;

    public SettlementFleet settlementFleet;

    public float priceToPurchase;

    SettlementInfo info;

    public float[] defaultTimers = new float[5];

    //float time;

    public void Start()
    {
        //Building

        defaultTimers[0] = 60f;
        defaultTimers[1] = 120f;
        defaultTimers[2] = 180f;
        defaultTimers[3] = 240f;
        defaultTimers[4] = 300f;

        for (int i = 0; i < settlementObject.buildingLevels.Length; i++)
        {
            settlementObject.buildingLevels[i] = 1;
        }

        // Get Components

        settlementFleet = GetComponent<SettlementFleet>();
        info = GetComponent<SettlementInfo>();
        priceToPurchase = 30000 + settlementObject.prosperity;
    }

    private void OnDestroy()
    {
        //time = Time.time;
    }

    void Update()
    {
        resourceTimer += Time.deltaTime;

        if(resourceTimer >= resourceTimeRate)
        {
            resourceTimer = 0f;
            CalculateResources();
        }

        //respawnTimer += Time.deltaTime;

        //if (respawnTimer >= respawnTimeRate)
        //{
        //    respawnTimer = 0f;
        //    RefillRecruits();
        //}

        settlementObject.security = Mathf.Clamp(settlementObject.security, -100, 100);

        // Building

        settlementObject.buildTimers[0] -= Time.deltaTime;
        settlementObject.buildTimers[1] -= Time.deltaTime;
        settlementObject.buildTimers[2] -= Time.deltaTime;
        settlementObject.buildTimers[3] -= Time.deltaTime;
        settlementObject.buildTimers[4] -= Time.deltaTime;

        for (int i = 0; i < settlementObject.buildTimers.Length; i++)
        {
            if (settlementObject.buildTimers[i] <= 0)
            {
                settlementObject.buildTimers[i] = 0;
            }
        }

        for (int i = 0; i < settlementObject.upgrading.Length; i++)
        {
            if(settlementObject.upgrading[i] == true)
            {
                if(settlementObject.buildTimers[i] <= 0)
                {
                    settlementObject.buildingLevels[i]++;
                    settlementObject.upgrading[i] = false;
                }
            }
        }
    }

    void CalculateResources()
    {
        // Increase Settlement loyalty Based on Level of Capitol
        settlementObject.loyalty += (settlementObject.buildingLevels[0]);

        // Increase Settlement Fleet Limit Based on Level of Hangar
        settlementFleet.maxInFleet = 15 + (settlementObject.buildingLevels[1] * 10);

        // Add prosperity for each level of shops
        settlementObject.prosperity += (settlementObject.buildingLevels[2]);

        // Add energy based on power planet level
        settlementObject.energy += (settlementObject.buildingLevels[3] * 1000);

        // Add population based on habitat level
        settlementObject.population += (settlementObject.buildingLevels[4]);

        // Add security for fleet members.
        settlementObject.security += (settlementFleet.fleet.Count * .1f);

        if(settlementObject.security >= 70)
        {
            settlementObject.prosperity++;
        } else
        {
            settlementObject.prosperity--;
        }

        if (settlementObject.population > settlementObject.energy)
        {
            settlementObject.security--;
            settlementObject.prosperity--;
            settlementObject.population -= Mathf.Abs(settlementObject.population - settlementObject.energy);
        }

        settlementObject.taxes = (settlementObject.population * settlementObject.taxRate * settlementObject.prosperity);
        settlementObject.energy -= settlementObject.population;

        if (!settlementObject.isPlayerOwned)
        {
            if (settlementObject.npcOwner)
            {
                if(info.faction.leader.leaderTrait == NPC.LeaderTrait.economist)
                {
                    settlementObject.npcOwner.money += (settlementObject.taxes * 1.05f);
                }
                else
                {
                    settlementObject.npcOwner.money += (settlementObject.taxes);
                }
            }
            else
            {
                this.settlementObject.faction.resources += (settlementObject.taxes);
            }
        } 
        else
        {
            if (info.faction.leader.leaderTrait == NPC.LeaderTrait.economist)
            {
                StatManager.instance.currentMoney += (settlementObject.taxes * 1.05f);
            }
            else
            {
                StatManager.instance.currentMoney += (settlementObject.taxes);
            }
        }

        settlementObject.security = Mathf.Clamp(settlementObject.security, -100, 100);
        settlementObject.loyalty = Mathf.Clamp(settlementObject.security, -100, 100);

        priceToPurchase = 30000 + settlementObject.prosperity;
    }

    public struct BuildingSlot
    {
        public int buildingLevel;
        public float buildTimers;
        public bool upgrading;
    }

    [ContextMenu("Update Info")]
    public void UpdateInfo()
    {
        settlementObject.name = GetComponent<SettlementInfo>().Name + " Settlement";
        settlementObject.stationName = GetComponent<SettlementInfo>().Name;

        settlementObject.faction = GetComponent<SettlementInfo>().faction;

        settlementObject.inventory = GetComponent<Shop>().settlementInventory;

        settlementObject.npcOwner = GetComponent<SettlementInfo>().npc;
    }

    [ContextMenu("Create Asset")]
    public void CreateAsset()
    {
        SettlementObject asset = ScriptableObject.CreateInstance<SettlementObject>();

        string name = GetComponent<SettlementInfo>().Name;
        //AssetDatabase.CreateAsset(asset, "Assets/Scripts/Settlements/Settlement Objects/" + name + " Settlement" + ".asset");
        //AssetDatabase.SaveAssets();

        settlementObject = asset;

        settlementObject.stationName = GetComponent<SettlementInfo>().Name;

        settlementObject.faction = GetComponent<SettlementInfo>().faction;

        settlementObject.inventory = GetComponent<Shop>().settlementInventory;

        settlementObject.npcOwner = GetComponent<SettlementInfo>().npc;
    }
}
