using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyHudOverview : MonoBehaviour
{
    public PlayerFleet fleet;
    public GameObject slotPrefab;
    public Transform partyHudRoot;

    void Awake()
    {
        for (int i = 0; i < fleet.fleet.Count; i++)
        {
            CreatePartyOverviewSlot(fleet.fleet[i]);
        }
    }

    public void CreatePartyOverviewSlot(FleetShip ship)
    {
        PartyHudSlot slot = Instantiate(slotPrefab, partyHudRoot).GetComponent<PartyHudSlot>();
        slot.ship = ship;
    }
}
