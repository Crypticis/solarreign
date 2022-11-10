using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class FleetFollower : MonoBehaviour
{
    FleetManager fleetManager;

    private void Start()
    {
        fleetManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<FleetManager>();
    }

    public void JoinFleet()
    {
        fleetManager.AddToFleet(this.gameObject);
    }
}
