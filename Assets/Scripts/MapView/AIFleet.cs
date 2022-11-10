using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFleet : Fleet
{
    public FleetStats fleetStats;
    public int defaultFleetSize = 20;
    public void UpdateFleetMax()
    {
        maxInFleet = defaultFleetSize + fleetStats.Command.currentLevel;
    }
}
