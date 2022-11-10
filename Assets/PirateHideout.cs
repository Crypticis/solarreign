using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class PirateHideout : MonoBehaviour
{
    public int maxPirates = 1;
    public string prefix;

    public List<GameObject> pirates = new List<GameObject>();

    private void Start()
    {
        Invoke("StartCoroutineDelay", 1.5f);

        Invoke("SpawnPirates", 1f);
    }

    void StartCoroutineDelay()
    {
        StartCoroutine("UpdatePosition");
    }

    IEnumerator UpdatePosition()
    {
        while (true)
        {
            for (int i = 0; i < pirates.Count; i++)
            {
                pirates[i].GetComponent<SteerForTether>().TetherPosition = transform.position;
            }

            if(pirates.Count <= 0)
            {
                POIManager.instance.RemovePOI(GetComponent<PirateHideout>().name);

                Destroy(this);
            }

            yield return new WaitForSeconds(5f);
        }
    }

    public void SpawnPirates()
    {
        for (int i = 0; i < maxPirates; i++)
        {
            PirateSpawner.instance.SpawnPirateAt(transform, prefix);
        }
    }
}
