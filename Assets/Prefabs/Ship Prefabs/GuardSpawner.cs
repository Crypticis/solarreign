using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class GuardSpawner : MonoBehaviour
{
    public Faction faction;
    public int maxGuards;
    public List<GameObject> guards = new List<GameObject>();
    public int distanceFromOrbit = 750;

    void Start()
    {
        if (guards.Count < maxGuards)
        {
            int amount = maxGuards - guards.Count;

            for (int i = 0; i < amount; i++)
            {
                GameObject go;

                if (faction.shipPrefabs.Length > 1)
                {
                    go = Instantiate(faction.shipPrefabs[Random.Range(0, faction.shipPrefabs.Length - 2)], new Vector3(this.transform.position.x + Random.Range(-500, 500), this.transform.position.y + Random.Range(-500, 500), this.transform.position.z + Random.Range(-500, 500)), Quaternion.identity);
                }
                else
                {
                    go = Instantiate(faction.shipPrefabs[Random.Range(0, faction.shipPrefabs.Length)], new Vector3(this.transform.position.x + Random.Range(-500, 500), this.transform.position.y + Random.Range(-500, 500), this.transform.position.z + Random.Range(-500, 500)), Quaternion.identity);
                }

                //var orbit = go.GetComponentInChildren<AIMOrbit>();

                //orbit.enabled = true;
                //orbit.Target = this.gameObject;
                //orbit.Orbit.Radius = distanceFromOrbit;

                go.GetComponent<SteerForTether>().TetherPosition = this.transform.position;


                guards.Add(go);
            }
        }

        StartCoroutine("FillGuards");
    }

    IEnumerator FillGuards()
    {
        while (true)
        {
            if(guards.Count < maxGuards)
            {
                int amount = maxGuards - guards.Count;

                for (int i = 0; i < amount; i++)
                {
                    GameObject go;

                    if(faction.shipPrefabs.Length > 1)
                    {
                        go = Instantiate(faction.shipPrefabs[Random.Range(0, faction.shipPrefabs.Length - 2)], new Vector3(this.transform.position.x + Random.Range(-500, 500), this.transform.position.y + Random.Range(-500, 500), this.transform.position.z + Random.Range(-500, 500)), Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate(faction.shipPrefabs[Random.Range(0, faction.shipPrefabs.Length)], new Vector3(this.transform.position.x + Random.Range(-500, 500), this.transform.position.y + Random.Range(-500, 500), this.transform.position.z + Random.Range(-500, 500)), Quaternion.identity);
                    }

                    go.GetComponent<SteerForTether>().TetherPosition = this.transform.position;

                    guards.Add(go);
                }
            }

            yield return new WaitForSeconds(300f);
        }
    }
}
