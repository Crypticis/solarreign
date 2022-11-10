using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;
using TickedPriorityQueue;


[RequireComponent(typeof(AutonomousVehicle)), RequireComponent(typeof(SteerForPoint))]
public class GoForStation : MonoBehaviour
{
	public GameObject station;
    SteerForPoint steer;

    private void Start()
    {
        steer = GetComponent<SteerForPoint>();
    }

    public void Update()
    {
        steer.enabled = true;
        steer.TargetPoint = station.transform.position;
    }
}
