using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaction : FleetFaction
{
    private void Start()
    {
        for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        {
            if(FactionManager.instance.factions[i].playerInFaction == true)
            {
                faction = FactionManager.instance.factions[i];
                return;
            }
        }
    }

    //void Update()
    //{
    //    if (GetComponentInChildren<MeshRenderer>().material != faction.materialSmooth)
    //    {
    //        GetComponentInChildren<MeshRenderer>().material = faction.materialSmooth;
    //    }
    //}

    public void UpdateFaction()
    {
        for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        {
            if (FactionManager.instance.factions[i].playerInFaction == true)
            {
                faction = FactionManager.instance.factions[i];
                return;
            }
        }
    }
}
