using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleetSpeed : MonoBehaviour
{
    public Fleet fleet;
    public float speed;
    float speedChangePerUnit = 0.25f;
    public float startingSpeed = 40f; 

    public void Update()
    {
        speed = startingSpeed - (fleet.fleet.Count * (speedChangePerUnit - (.01f * StatManager.instance.logisticsLevel)));

        if(speed < (startingSpeed * .1f))
        {
            speed = (startingSpeed * .1f);
        }

        //agent.speed = speed;
    }
}
