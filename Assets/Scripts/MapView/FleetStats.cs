using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetStats : MonoBehaviour
{
    public Level Command;
    public AIFleet fleet;
    public int commandLevelStart;

    private IEnumerator coroutine;

    private void Awake()
    {
        fleet.UpdateFleetMax();
    }

    void Start()
    {
        Command = new Level(1, OnLevelUp);
        Command.MAX_LEVEL = 999;
        Command.currentLevel = commandLevelStart;
        //fleet.UpdateFleetMax();
        coroutine = AddCommandExp(15.0f);
        StartCoroutine(coroutine);
    }

    public void OnLevelUp()
    {
        fleet.UpdateFleetMax();
    }

    private IEnumerator AddCommandExp(float waitTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime);
            Command.AddExp(2);
        }
    }
}
