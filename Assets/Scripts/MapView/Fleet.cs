using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fleet : MonoBehaviour
{
    public List<FleetShip> fleet = new List<FleetShip>();
    public int maxInFleet;

    public virtual void AddToFleet(GameObject ship)
    {
        FleetShip ship1 = new FleetShip();
        ship1.ship = ship;

        fleet.Add(ship1);
    } 

    public void RemoveFromFleet(FleetShip ship)
    {
        if (fleet.Contains(ship))
        {
            fleet.Remove(ship);
        }
    }

    //public void UpdateFleet()
    //{
    //    for (int i = fleet.Count - 1; i >= 0; i--)
    //    {
    //        if (fleet[i].health <= 0)
    //        {
    //            fleet[i] = fleet[fleet.Count - 1];
    //            fleet.RemoveAt(fleet.Count - 1);
    //        }
    //    }
    //}
}

[System.Serializable]
public class FleetShip 
{
    public bool hasPilot = false;
    public Pilot pilot;
    public GameObject ship;
}

[System.Serializable]
public class Pilot
{
    public string name;
    public Faction faction;
    public int costToHire;
    public Level level;
    public int skillpoints;

    public int speedSkill;
    public int firespeedSkill;
    public int durabilitySkill;
    public int damageSkill;
    public int rangeSkill;

    public int modelID;

    public void ResetLevels()
    {
        level = new Level(1, null);

        skillpoints = 0;

        speedSkill = 0;
        firespeedSkill = 0;
        durabilitySkill = 0;
        damageSkill = 0;
        rangeSkill = 0;

        costToHire = 0;
        modelID = 0;
    }

    public void RandomizeStats()
    {
        speedSkill = Random.Range(0, 5);
        firespeedSkill = Random.Range(0, 5);
        durabilitySkill = Random.Range(0, 5);
        damageSkill = Random.Range(0, 5);
        rangeSkill = Random.Range(0, 5);

        modelID = Random.Range(0, GameManager.instance.database.pilotModels.Length);

        costToHire = 100 * (speedSkill + firespeedSkill + durabilitySkill + damageSkill + rangeSkill);
    }
}

