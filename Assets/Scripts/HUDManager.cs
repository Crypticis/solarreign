using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
   /* public GameObject enemyHUD;
    public GameObject allyHUD;
    public GameObject neutralHUD;

    public GameObject leadHUD;

    public GameObject[] celestialBodies;
    public GameObject[] stations;

    //public GameObject neutralHUD;

    public float targetingRange;

    public Transform player;
    public float radius;
    public Camera cam;

    private IEnumerator hudUpdate;
    float hudUpdateTime = 5f;

    void Start()
    {
        celestialBodies = GameObject.FindGameObjectsWithTag("Celestial Body");
        stations = GameObject.FindGameObjectsWithTag("Station");

        foreach (GameObject body in celestialBodies)
        {
            GameObject spawnedNeutralHUD = Instantiate(neutralHUD, gameObject.transform);
            HUDIndicator hud = spawnedNeutralHUD.GetComponent<HUDIndicator>();
            hud.target = body.transform;
            hud.player = player.transform;
            hud.cam = cam;
            hud.name = body.name;
        }

        foreach (GameObject station in stations)
        {
            GameObject spawnedNeutralHUD = Instantiate(neutralHUD, gameObject.transform);
            HUDIndicator hud = spawnedNeutralHUD.GetComponent<HUDIndicator>();
            hud.target = station.transform;
            hud.player = player.transform;
            hud.cam = cam;
            hud.name = station.name;
        }

        hudUpdate = HUDUpdate(2.0f);
        StartCoroutine(hudUpdate);

    }

    private IEnumerator HUDUpdate(float waitTime)
    {
        while (true)
        {
            Collider[] cols = Physics.OverlapSphere(player.position, radius);
            foreach (Collider col in cols)
            {
                float distance = (player.position - col.transform.position).sqrMagnitude;
                HUDElements hudelements = col.GetComponent<HUDElements>();

                if (col && col.tag == "Enemy" && distance <= (targetingRange * targetingRange) && hudelements)
                {
                    if (hudelements.hasHUD == false)
                    {
                        GameObject spawnedEnemyHUD = Instantiate(enemyHUD, gameObject.transform);
                        HUDIndicator hud = spawnedEnemyHUD.GetComponent<HUDIndicator>();
                        hud.target = col.transform;
                        hud.player = player.transform;
                        hud.cam = cam;

                        hudelements.hasHUD = true;
                    }

                    if (hudelements.hasLead == false)
                    {
                        GameObject lead = Instantiate(leadHUD, gameObject.transform);
                        Lead leadUI = lead.GetComponent<Lead>();
                        leadUI.target = col.transform;
                        leadUI.player = player.transform;
                        leadUI.cam = cam;

                        hudelements.hasLead = true;
                    }
                }
                if (col && col.tag == "Ally" && distance <= (targetingRange * targetingRange) && hudelements)
                {
                    if (hudelements.hasHUD == false)
                    {
                        GameObject spawnedAllyHUD = Instantiate(allyHUD, gameObject.transform);
                        HUDIndicator hud = spawnedAllyHUD.GetComponent<HUDIndicator>();
                        hud.target = col.transform;
                        hud.player = player.transform;
                        hud.cam = cam;
                        hudelements.hasHUD = true;
                    }
                }
            }
            yield return new WaitForSeconds(waitTime);
        }
    } */
}
