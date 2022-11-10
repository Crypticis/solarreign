using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyOverview : MonoBehaviour
{
    public FleetShip fleetShip;

    public void Close()
    {
        gameObject.SetActive(false);
    }
}
