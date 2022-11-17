using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFleet : Fleet
{
    IEnumerator coroutine;
    public float timeInterval;
    public PartyHudOverview partyHud;

    public List<Pilot> unusedPilots = new List<Pilot>();

    public void Start()
    {
        coroutine = ShipUpkeep();
        StartCoroutine(coroutine);
        UpdateFleetMax();
    }

    public override void AddToFleet(GameObject ship)
    {
        FleetShip ship1 = new FleetShip();
        ship1.ship = ship;

        partyHud.CreatePartyOverviewSlot(ship1);

        fleet.Add(ship1);
    }

    public void UpdateFleetMax()
    {
        maxInFleet = 10 + (StatManager.instance.commandLevel * 2);
    }

    public IEnumerator ShipUpkeep()
    {
        yield return new WaitForSeconds(timeInterval);

        float total = 0;

        for (int i = 0; i < fleet.Count; i++)
        {
            StatManager.instance.currentMoney -= fleet[i].ship.GetComponent<FleetMember>().upkeep;
            total += fleet[i].ship.GetComponent<FleetMember>().upkeep;
        }

        if(total > 0)
            Ticker.Ticker.AddItem("$" + total + " removed for fleet upkeep.");
    }
    
    public void AddToPilots(Pilot pilot)
    {
        unusedPilots.Add(pilot);
    }
}
