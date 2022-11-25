using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC Database", menuName = "Database/NPC Database")]
public class NPCDatabaseObject : ScriptableObject
{
    public List<NPC> npcs = new();
    public GameObject[] shipObjects;
    public GameObject[] fleetPrefabs;
    public GameObject[] interactionPrefab;
    public Dictionary<int, GameObject> GetShip;
    public Dictionary<string, GameObject> GetFleetPrefab;
    public Dictionary<string, GameObject> GetInteractionPrefab;

    public GameObject[] pilotModels;
    public Dictionary<int, GameObject> GetPilotModel;

    public Faction[] factions;
    public Dictionary<string, Faction> GetFaction;

    public ShipInfoObject[] shipinfos;

    public Item[] items;

    public Inventory[] inventories;

    public SettlementObject[] settlements;

    public SystemInfo[] systems;

    public void Awake()
    {
        UpdateID();
    }

    [ContextMenu("Update IDs and Dictionaries")]
    public void UpdateID()
    {
        GetShip = new Dictionary<int, GameObject>();
        GetFaction = new Dictionary<string, Faction>();
        GetPilotModel = new Dictionary<int, GameObject>();
        GetFleetPrefab = new Dictionary<string, GameObject>();
        GetInteractionPrefab = new Dictionary<string, GameObject>();

        for (int i = 0; i < shipObjects.Length; i++)
        {
            GetShip.Add(i, shipObjects[i]);
            shipObjects[i].GetComponent<ShipInfo>().info.ID = i;
        }

        for (int i = 0; i < fleetPrefabs.Length; i++)
        {
            GetFleetPrefab.Add(fleetPrefabs[i].name, fleetPrefabs[i]);
        }

        for (int i = 0; i < factions.Length; i++)
        {
            GetFaction.Add(factions[i].name, factions[i]);
        }

        for (int i = 0; i < pilotModels.Length; i++)
        {
            GetPilotModel.Add(i, pilotModels[i]);
        }

        for (int i = 0; i < interactionPrefab.Length; i++)
        {
            GetInteractionPrefab.Add(interactionPrefab[i].name, interactionPrefab[i]);
        }
    }

    [ContextMenu("Reset NPCs")]
    public void ResetNPC()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            npcs[i].ResetMoney();
        }
    }

    [ContextMenu("Reset Inventories")]
    public void ResetInventories()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            inventories[i].ClearInventory();
        }
    }

    [ContextMenu("Add Default Inventories")]
    public void AddDefaultInventories()
    {
        for (int i = 0; i < inventories.Length; i++)
        {
            inventories[i].ClearInventory();

            if(i != 0)
            {
                for (int j = 0; j < items.Length; j++)
                {

                    inventories[i].AddItem(items[j], Random.Range(50,125));
                }
            }
        }
    }

    [ContextMenu("Reset ship infos")]
    public void ResetShipInfo()
    {
        for (int i = 0; i < shipinfos.Length; i++)
        {
            shipinfos[i].Reset();
        }
    }

    public int GetSystemDangerRating(string systemName)
    {
        for (int i = 0; i < systems.Length; i++)
        {
            if (systemName == systems[i].name)
            {
                return systems[i].dangerRating;
            }
        }

        return -1;
    }

    public Faction GetSystemFaction(string systemName)
    {
        for (int i = 0; i < systems.Length; i++)
        {
            if (systemName == systems[i].name)
            {
                return systems[i].faction;
            }
        }

        return null;
    }

    [System.Serializable]
    public struct SystemInfo
    {
        public string name;
        public int dangerRating;
        public Faction faction;
    }
}
