using UnityEngine;
using UnitySteer.Behaviors;

public class CivilianFleetSpawner : MonoBehaviour
{
    //public GameObject[] potentialDestinations;
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

       // potentialDestinations = FindGates().Concat(FindSettlements()).ToArray();

        for (int i = 0; i < 5; i++)
        {
            Invoke("SpawnCivilian", 1f);
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

    //public void SpawnCivilian()
    //{
    //    var dest = potentialDestinations[Random.Range(0, potentialDestinations.Length)].transform;

    //    GameObject civilian = Instantiate(civilianPrefabs[Random.Range(0, civilianPrefabs.Length)], Vector3.Lerp(this.transform.position, dest.transform.position, Random.Range(0.0f, 1.0f)), Quaternion.identity);

    //    if (info)
    //        civilian.GetComponent<FleetFaction>().faction = info.faction;

    //    if (GetComponent<Shop>())
    //        civilian.GetComponent<CivilianCommanderAI>().originSettlement = transform;

    //    //civilian.GetComponent<CivilianCommanderAI>().destinationType = destinationType;

    //    civilian.GetComponent<CivilianCommanderAI>().destination = dest;

    //    civilian.GetComponent<UniqueNPC>().ID = Random.Range(1000, 99999);

    //    civilian.GetComponent<UniqueNPC>().npc = ScriptableObject.CreateInstance("NPC") as NPC;

    //    //civilian.GetComponent<UniqueNPC>().npc.Name = (info.faction.name + " Civilian Fleet");
    //}


    //Spawns and sets scripts on the ships to be the desired values to fly around the POI.
    public void SpawnCivilian()
    {
        GameObject civilian = Instantiate(civilianPrefabs[Random.Range(0, civilianPrefabs.Length)], spawnPoint.position, Quaternion.identity);

        if (info)
            civilian.GetComponent<FleetFaction>().faction = info.faction;

        var tether = civilian.GetComponent<SteerForTether>();
        tether.enabled = true;
        tether.TetherPosition = this.transform.position;
        tether.MaximumDistance = 200f;

        civilian.GetComponent<SteerForWander>().enabled = true;

        civilian.TryGetComponent(out SteerForMinimumSpeed speed);
        {
            speed.enabled = true;
            speed.MinimumSpeed = civilian.GetComponent<AutonomousVehicle>().MaxSpeed;
        }
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
