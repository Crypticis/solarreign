using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class FactionFleetSpawner : MonoBehaviour
{
    public List<GameObject> fleets = new List<GameObject>();
    public int maxFleets = 1;

    public GameObject fleetPrefab;

    void Start()
    {
        Invoke("SpawnFleet", 1f);
    }

    public void SpawnFleet()
    {
        GameObject fleet = Instantiate(fleetPrefab, transform.position, Quaternion.identity);
        fleet.GetComponent<FleetFaction>().faction = GetComponentInParent<SettlementInfo>().faction;
        fleet.GetComponent<SteerForTether>().TetherPosition = this.transform.position;
        fleet.GetComponent<SteerForTether>().enabled = true;

        fleets.Add(fleet);
    }

    public void ClearFleet()
    {
        fleets.Clear();
    }
}
