using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoinFaction : MonoBehaviour
{
    FleetFaction faction;


    void Start()
    {
        faction = GetComponent<FleetFaction>();
    }

    public void FactionJoin()
    {
        if(StatManager.instance.currentMoney >= 1000)
        {
            for (int i = 0; i < FactionManager.instance.factions.Length; i++)
            {
                if (FactionManager.instance.factions[i] == faction.faction)
                {
                    FactionManager.instance.factions[i].playerInFaction = true;

                    Player.playerInstance.GetComponent<PlayerFaction>().UpdateFaction();

                    Ticker.Ticker.AddItem("You have joined the " + faction.faction.name);
                    Ticker.Ticker.AddItem("You paid a $1000 membership fee to join");

                    StatManager.instance.currentMoney -= 1000f;
                }
                else
                {
                    FactionManager.instance.factions[i].playerInFaction = false;
                }
            }
        }
        else
        {
            Ticker.Ticker.AddItem("You need at least $1000 to join a faction.");
        }
    }
}
