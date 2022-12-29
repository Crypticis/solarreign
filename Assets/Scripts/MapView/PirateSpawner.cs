using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnitySteer.Behaviors;

public class PirateSpawner : MonoBehaviour
{
    public static PirateSpawner instance;

    public GameObject[] piratePrefab;
    public float defaultTimeToSpawn = 60;
    public List<GameObject> pirates = new List<GameObject>();
    public GameObject[] settlements;
    public int maxPirates = 50;

    public Transform[] spawnPoints;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        settlements = GameObject.FindGameObjectsWithTag("Settlement");

        //for (int i = 0; i < 5; i++)
       // {
        //    SpawnPirate();
        //}

        //StartCoroutine("SpawnPirate");
    }

    public IEnumerator SpawnPirate()
    {
        while (true)
        {
            if(pirates.Count < maxPirates)
            {
                GameObject pirate = Instantiate(piratePrefab[Random.Range(0, piratePrefab.Length)], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
                pirate.GetComponent<UniqueNPC>().npc = ScriptableObject.CreateInstance("NPC") as NPC;
                pirate.GetComponent<UniqueNPC>().npc.Name = "Pirate Fleet";

                pirate.GetComponent<UniqueNPC>().ID = Random.Range(1000, 99999);

                pirates.Add(pirate);
            }

            yield return new WaitForSeconds(defaultTimeToSpawn);
        }
    }

    public void SpawnPirateAt(Transform transform, string prefix)
    {
        GameObject pirate = Instantiate(piratePrefab[Random.Range(0, piratePrefab.Length)], transform.position, Quaternion.identity);
        //pirate.GetComponent<UniqueNPC>().npc = ScriptableObject.CreateInstance("NPC") as NPC;
        //pirate.GetComponent<UniqueNPC>().npc.Name = "Pirate Fleet";

        pirate.GetComponent<HUDElements>().name = prefix + " Pirate Leader";

        //pirate.GetComponent<UniqueNPC>().ID = Random.Range(1000, 99999);

        pirate.GetComponent<SteerForTether>().TetherPosition = transform.position;
        pirate.GetComponent<SteerForTether>().enabled = true;

        transform.parent.GetComponent<PirateHideout>().pirates.Add(pirate);

        //pirates.Add(pirate);
    }

    public GameObject SpawnPirateAt(Transform transform)
    {
        GameObject pirate = Instantiate(piratePrefab[Random.Range(0, piratePrefab.Length)], transform.position, Quaternion.identity);
        pirate.GetComponent<UniqueNPC>().npc = ScriptableObject.CreateInstance("NPC") as NPC;
        pirate.GetComponent<UniqueNPC>().npc.Name = "Pirate Fleet";

        pirate.GetComponent<HUDElements>().name = "Pirate Leader";

        pirate.GetComponent<UniqueNPC>().ID = Random.Range(1000, 99999);

        pirate.GetComponent<SteerForTether>().TetherPosition = transform.position;
        pirate.GetComponent<SteerForTether>().enabled = true;

        return pirate;
    }
}
