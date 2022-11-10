using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FleetRadar : MonoBehaviour
{
    Fleet fleet;
    public FleetFaction fleetFaction;
    public Targeting targeting;

    public void Awake()
    {
        fleet = GetComponentInParent<Fleet>();
        targeting = GetComponentInParent<Targeting>();
        fleetFaction = GetComponentInParent<FleetFaction>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerFaction>())
        {
            for (int i = 0; i < StatManager.instance.playerStatsObject.relations.Length; i++)
            {
                if(fleetFaction.faction == StatManager.instance.playerStatsObject.relations[i].faction)
                {
                    if(StatManager.instance.playerStatsObject.relations[i].relation <= 0)
                    {
                        targeting.enemies.Add(other.gameObject);
                        targeting.SortList();
                        return;
                    }
                }
            }

            if (fleetFaction.faction.IsEnemy(other.gameObject.GetComponent<FleetFaction>().faction) && !targeting.enemies.Contains(other.gameObject))
            {
                targeting.enemies.Add(other.gameObject);
                targeting.SortList();
                return;
            }
        }

        if (other.gameObject.GetComponent<FleetFaction>())
        {
            if (fleetFaction.faction.IsEnemy(other.gameObject.GetComponent<FleetFaction>().faction) && !targeting.enemies.Contains(other.gameObject))
            {
                targeting.enemies.Add(other.gameObject);
                targeting.SortList();
                return;
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (targeting.enemies.Contains(other.gameObject))
        {
            targeting.enemies.Remove(other.gameObject);
        }
    }
}


