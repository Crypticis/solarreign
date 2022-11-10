using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class SpawnZone : MonoBehaviour
{

    public EnemyZone enemyZone;
    public float respawnTimer = 60f;

    public bool respawnEnabled;

    public int maxF2 = 5;
    public int minF2 = 1;

    public GameObject[] faction2;

    public int maxDebris = 5;
    public int minDebris = 1;

    int numberOfDebris = 0;

    public GameObject[] debris;

    public GameObject[] faction1;

    public int maxF1 = 5;
    public int minF1= 1;

    public GameObject[] faction3;

    public int maxF3 = 5;
    public int minF3 = 1;

    public Vector3 startPosition;

    public List<GameObject> spawnedFaction2;
    public List<GameObject> spawnedFaction1;
    public List<GameObject> spawnedFaction3;

    public float waitToSpawn;

    void Start()
    {
        spawnedFaction2 = new List<GameObject>();
        spawnedFaction1 = new List<GameObject>();
        startPosition = this.transform.position;

        enemyZone = GetComponent<EnemyZone>();

        if(waitToSpawn > 0)
        {
            Invoke("SpawnNPCs", waitToSpawn);
        } else
        {
            SpawnNPCs();
        }

        SpawnDebris();
    }

    void Update()
    {
        //Enemies

        if(this.transform.position != startPosition)
        {
            foreach (GameObject spawnedEnemy in spawnedFaction2)
            {
                if(spawnedEnemy)
                    spawnedEnemy.GetComponent<SteerForTether>().TetherPosition = this.transform.position;
            }

            foreach (GameObject spawnedAlly in spawnedFaction1)
            {
                if(spawnedAlly)
                    spawnedAlly.GetComponent<SteerForTether>().TetherPosition = this.transform.position;
            }

            foreach (GameObject spawnedF3 in spawnedFaction3)
            {
                if (spawnedF3)
                    spawnedF3.GetComponent<SteerForTether>().TetherPosition = this.transform.position;
            }

            startPosition = this.transform.position;
        }

        for (int i = 0; i < spawnedFaction2.Count; i++)
        {
           if(spawnedFaction2[i] == null)
            {
                spawnedFaction2[i] = spawnedFaction2[spawnedFaction2.Count - 1];
                spawnedFaction2.RemoveAt(spawnedFaction2.Count - 1);
            }
        }

        for (int i = 0; i < spawnedFaction1.Count; i++)
        {
            if (spawnedFaction1[i] == null)
            {
                spawnedFaction1[i] = spawnedFaction1[spawnedFaction1.Count - 1];
                spawnedFaction1.RemoveAt(spawnedFaction1.Count - 1);
            }
        }

        //Respawning

        if (respawnEnabled)
        {
            if ((spawnedFaction1.Count <= 0 && maxF1 > 0) || (spawnedFaction2.Count <= 0 && maxF2 > 0) || (spawnedFaction3.Count <= 0 && maxF3 > 0))
            {
                Invoke("SpawnNPCs", 120f);
            }
        }
    }

    void SpawnNPCs()
    {
        if (faction2 != null && maxF2 != 0)
        {
            int numberOfF2 = UnityEngine.Random.Range(minF2, maxF2);

            for (int i = 0; i < numberOfF2; i++)
            {
                GameObject ship = Instantiate(faction2[UnityEngine.Random.Range(0, faction2.Length)], enemyZone.Waypoint, UnityEngine.Random.rotation);
                ship.GetComponent<SteerForTether>().TetherPosition = enemyZone.transform.position;
                spawnedFaction2.Add(ship);
            }
        }

        if (faction1 != null && maxF1 != 0)
        {
            int numberOfF3 = UnityEngine.Random.Range(minF1, maxF1);

            for (int i = 0; i < numberOfF3; i++)
            {
                GameObject ship = Instantiate(faction1[UnityEngine.Random.Range(0, faction1.Length)], enemyZone.Waypoint, UnityEngine.Random.rotation);
                ship.GetComponent<SteerForTether>().TetherPosition = enemyZone.transform.position;
                spawnedFaction1.Add(ship);
            }
        }

        if (faction3 != null && maxF3 != 0)
        {
            int numberOfF1 = UnityEngine.Random.Range(minF3, maxF3);

            for (int i = 0; i < numberOfF1; i++)
            {
                GameObject ship = Instantiate(faction3[UnityEngine.Random.Range(0, faction3.Length)], enemyZone.Waypoint, UnityEngine.Random.rotation);
                ship.GetComponent<SteerForTether>().TetherPosition = enemyZone.transform.position;
                spawnedFaction3.Add(ship);
            }
        }
    }

    void SpawnDebris()
    {
        if (debris != null && maxDebris != 0)
        {

            numberOfDebris = Random.Range(minDebris, maxDebris);

            for (int i = 0; i < numberOfDebris; i++)
            {
                Instantiate(debris[UnityEngine.Random.Range(0, debris.Length)], enemyZone.Waypoint, UnityEngine.Random.rotation);
            }
        }
    }
}
