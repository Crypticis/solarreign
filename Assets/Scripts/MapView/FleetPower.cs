using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetPower : MonoBehaviour
{
    public Fleet fleet;
    public float totalPower;
    int oldFleet = 0;

    public void Start()
    {
        fleet = GetComponent<Fleet>();
    }

    public void Update()
    {
        if (fleet.fleet.Count != oldFleet)
        {
            totalPower = 0f;
            UpdatePower();
        }
    }

    public void UpdatePower()
    {
        for (int i = 0; i < fleet.fleet.Count; i++)
        {
            //totalPower += fleet.fleet[i].prefab.GetComponent<FleetMember>().power;
        }

        oldFleet = fleet.fleet.Count;
    }
}
