using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetResources : MonoBehaviour
{
    public float fuel;
    public Fleet fleet;
    public float totalFuelUpkeep;
    public float totalUpkeep;
    int oldFleetCount;

    private IEnumerator coroutine;
    public float resourceInterval = 15f;
    private void Start()
    {
        coroutine = CalculateResources(resourceInterval);
        StartCoroutine(coroutine);
    }

    public void Update()
    {
        if(oldFleetCount != fleet.fleet.Count)
        {
            GetUpkeep();
        }
    }

    public void GetUpkeep()
    {
        totalFuelUpkeep = 0f;
        totalUpkeep = 0f;

        for (int i = 0; i < fleet.fleet.Count; i++)
        {
            totalFuelUpkeep += fleet.fleet[i].ship.GetComponent<FleetMember>().fuelUpkeep;
            totalUpkeep += fleet.fleet[i].ship.GetComponent<FleetMember>().upkeep;
        }

        oldFleetCount = fleet.fleet.Count;
    }

    private IEnumerator CalculateResources(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);

            fuel -= totalFuelUpkeep;
            GetComponent<UniqueNPC>().npc.money -= totalUpkeep;
        }
    }
    
}
