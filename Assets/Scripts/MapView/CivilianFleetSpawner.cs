using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CivilianFleetSpawner : MonoBehaviour
{
    public GameObject[] potentialDestinations;
    public GameObject[] civilianPrefabs;
    SettlementInfo info;
    public Transform spawnPoint;

    //public DestinationType destinationType;

    float timer = 0f;
    float maxDelay = 240f;
    float minDelay = 120f;

    void Start()
    {
        info = GetComponent<SettlementInfo>();
        timer = Random.Range(0f, 240f);

        potentialDestinations = FindGates().Concat(FindSettlements()).ToArray();

        for (int i = 0; i < 5; i++)
        {
            SpawnCivilian();
        }
    }

    public void Update()
    {

        //Timer

        timer -= Time.deltaTime;

        if (timer <= 0)
            timer = 0;

        // Spawning

        if (timer <= 0)
        {
            SpawnCivilian();
            timer = Random.Range(minDelay, maxDelay);
        }
    }

    public void SpawnCivilian()
    {
        var dest = potentialDestinations[Random.Range(0, potentialDestinations.Length)].transform;

        GameObject civilian = Instantiate(civilianPrefabs[Random.Range(0, civilianPrefabs.Length)], Vector3.Lerp(this.transform.position, dest.transform.position, Random.Range(0.0f, 1.0f)), Quaternion.identity);

        if (info)
            civilian.GetComponent<FleetFaction>().faction = info.faction;

        if (GetComponent<SettlementTrader>())
            civilian.GetComponent<CivilianCommanderAI>().originSettlement = transform;

        //civilian.GetComponent<CivilianCommanderAI>().destinationType = destinationType;

        civilian.GetComponent<CivilianCommanderAI>().destination = dest;

        civilian.GetComponent<UniqueNPC>().ID = Random.Range(1000, 99999);

        civilian.GetComponent<UniqueNPC>().npc = ScriptableObject.CreateInstance("NPC") as NPC;

        //civilian.GetComponent<UniqueNPC>().npc.Name = (info.faction.name + " Civilian Fleet");
    }

    GameObject[] FindGates()
    {
        var temp = GameObject.FindGameObjectsWithTag("Warp Gate");
        return temp;
    }

    GameObject[] FindSettlements()
    {
        var temp = GameObject.FindGameObjectsWithTag("Settlement");
        return temp;
    }

}
