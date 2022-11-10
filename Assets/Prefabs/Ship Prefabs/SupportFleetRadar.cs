using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportFleetRadar : FleetRadar
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CommanderAI>())
        {
            if (fleetFaction.faction.allies.Contains(other.GetComponent<FleetFaction>().faction))
            {
                targeting.enemies.Add(other.gameObject);
            }
        }

        if (other.GetComponent<Player>())
        {
            targeting.enemies.Add(other.gameObject);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        if (targeting.enemies.Contains(other.gameObject))
        {
            targeting.enemies.Remove(other.gameObject);
        }
    }

}
