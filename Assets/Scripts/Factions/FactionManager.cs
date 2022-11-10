using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FactionManager : MonoBehaviour
{
    public static FactionManager instance;
    public Faction[] factions;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetFactions()
    {
        for (int i = 0; i < factions.Length; i++)
        {
            factions[i].Reset();
        }
    }

    public bool DetermineIfStronger(Faction faction1, Faction faction2)
    {
        var factionOne = CalculateUnitCount(faction1);
        var factionTwo = CalculateUnitCount(faction2);

        if (faction1.leader)
        {
            if (faction1.leader.leaderTrait == NPC.LeaderTrait.pacifist)
            {
                factionOne -= 10;
            }
            else if (faction1.leader.leaderTrait == NPC.LeaderTrait.warmonger)
            {
                factionOne += 10;
            }
            else if (faction1.leader.leaderTrait == NPC.LeaderTrait.economist)
            {
                factionOne += 5;
            }
        }

        if (factionOne > factionTwo)
        {
            return true;
        }

        return false;
    }

    public bool WantsAlly(Faction faction)
    {
        var acceptance = -20;

        if (faction.leader)
        {
            if (faction.leader.leaderTrait == NPC.LeaderTrait.pacifist)
            {
                acceptance += 20;
            }
            else if (faction.leader.leaderTrait == NPC.LeaderTrait.warmonger)
            {
                acceptance -= 20;
            }
            else if (faction.leader.leaderTrait == NPC.LeaderTrait.economist)
            {
                acceptance += 10;
            }

            if(faction.leader.personality == NPC.Personality.pragmatist)
            {
                acceptance -= 10;
            }
            else if(faction.leader.personality == NPC.Personality.gloryseeker)
            {
                acceptance += 10;
            }
        }

        for (int i = 0; i < faction.enemies.Count; i++)
        {
            acceptance += 10;
        }

        for (int i = 0; i < faction.allies.Count; i++)
        {
            acceptance -= 10;
        }

        if (acceptance > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool WouldAcceptPeace(Faction faction1, Faction faction2)
    {
        var acceptance = -20;

        if (faction2.leader)
        {
            if (faction2.leader.leaderTrait == NPC.LeaderTrait.pacifist)
            {
                acceptance += 20;
            }
            else if (faction2.leader.leaderTrait == NPC.LeaderTrait.warmonger)
            {
                acceptance -= 20;
            }
            else if (faction2.leader.leaderTrait == NPC.LeaderTrait.economist)
            {
                acceptance += 10;
            }

            if (faction2.leader.personality == NPC.Personality.pragmatist)
            {
                acceptance += 20;
            }
            else if (faction2.leader.personality == NPC.Personality.gloryseeker)
            {
                acceptance -= 20;
            }
        }

        for (int i = 0; i < faction2.enemies.Count; i++)
        {
            acceptance += 10;
        }

        for (int i = 0; i < faction2.allies.Count; i++)
        {
            acceptance -= 10;
        }

        if(CalculateUnitCount(faction1) > CalculateUnitCount(faction2))
        {
            acceptance += (CalculateUnitCount(faction1) - CalculateUnitCount(faction2) / 10);
        } 
        else
        {
            acceptance += (CalculateUnitCount(faction1) - CalculateUnitCount(faction2) / 10);
        }

        if(acceptance > 0)
        {
            return true;
        } 
        else
        {
            return false;
        }
    }

    public bool WouldAcceptAlly(Faction faction1, Faction faction2)
    {
        var acceptance = -20;

        if (faction2.leader)
        {
            if (faction2.leader.leaderTrait == NPC.LeaderTrait.pacifist)
            {
                acceptance += 20;
            }
            else if (faction2.leader.leaderTrait == NPC.LeaderTrait.warmonger)
            {
                acceptance -= 20;
            }
            else if (faction2.leader.leaderTrait == NPC.LeaderTrait.economist)
            {
                acceptance += 10;
            }

            if (faction2.leader.personality == NPC.Personality.pragmatist)
            {
                acceptance += 20;
            }
            else if (faction2.leader.personality == NPC.Personality.gloryseeker)
            {
                acceptance -= 20;
            }
        }

        for (int i = 0; i < faction2.enemies.Count; i++)
        {
            acceptance += 10;
        }

        for (int i = 0; i < faction2.allies.Count; i++)
        {
            acceptance -= 10;
        }

        if (CalculateUnitCount(faction1) > CalculateUnitCount(faction2))
        {
            acceptance += (CalculateUnitCount(faction1) - CalculateUnitCount(faction2) / 10);
        }
        else
        {
            acceptance += (CalculateUnitCount(faction1) - CalculateUnitCount(faction2) / 10);
        }

        acceptance -= FindDistanceBetweenFactions(faction1, faction2) / 1000;

        if (acceptance > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool WouldRemoveAlly(Faction faction1, Faction faction2)
    {
        var acceptance = -20;

        for (int i = 0; i < faction1.allies.Count; i++)
        {
            acceptance += 5;
        }

        for (int i = 0; i < faction2.enemies.Count; i++)
        {
            acceptance += 5;
        }

        if (CalculateUnitCount(faction1) > CalculateUnitCount(faction2))
        {
            acceptance += (CalculateUnitCount(faction1) - CalculateUnitCount(faction2) / 10);
        }
        else
        {
            acceptance += (CalculateUnitCount(faction1) - CalculateUnitCount(faction2) / 10);
        }

        acceptance += FindDistanceBetweenFactions(faction1, faction2) / 1000;

        if (acceptance > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MakeWar(Faction faction1, Faction faction2)
    {
        faction1.enemies.Add(faction2); 
        faction2.enemies.Add(faction1);

        Ticker.Ticker.AddItem(faction1 + " declared war on " + faction2);
    }

    public void MakePeace(Faction faction1, Faction faction2)
    {
        faction1.enemies.Remove(faction2);
        faction2.enemies.Remove(faction1);

        Ticker.Ticker.AddItem(faction1 + " made peace with " + faction2);
    }

    public void MakeAlly(Faction faction1, Faction faction2)
    {
        faction1.allies.Add(faction2);
        faction2.allies.Add(faction1);

        Ticker.Ticker.AddItem(faction1 + " formed an alliance with " + faction2);
    }
    public void RemoveAlly(Faction faction1, Faction faction2)
    {
        faction1.allies.Remove(faction2);
        faction2.allies.Remove(faction1);

        Ticker.Ticker.AddItem(faction1 + " dissolved alliance with " + faction2);
    }

    public int CalculateUnitCount(Faction faction)
    {
        GameObject[] commanders = GameObject.FindGameObjectsWithTag("NPC");

        List<GameObject> commandersInFaction = new List<GameObject>();

        for (int i = 0; i < commanders.Length; i++)
        {
            if(commanders[i].GetComponent<FleetFaction>().faction == faction)
            {
                commandersInFaction.Add(commanders[i]);
            }
        }

        int amount = 0;

        for (int i = 0; i < commandersInFaction.Count; i++)
        {
            amount += commandersInFaction[i].GetComponent<AIFleet>().fleet.Count;
        }

        return amount;
    }

    public int FindDistanceBetweenFactions(Faction faction1, Faction faction2)
    {
        GameObject[] settlements = GameObject.FindGameObjectsWithTag("Settlement");

        List<GameObject> settlementsInFaction = new List<GameObject>();
        List<GameObject> settlementsInFaction2 = new List<GameObject>();

        for (int i = 0; i < settlements.Length; i++)
        {
            if(settlements[i].GetComponent<SettlementInfo>().faction == faction1)
            {
                settlementsInFaction.Add(settlements[i]);
            }

            if (settlements[i].GetComponent<SettlementInfo>().faction == faction2)
            {
                settlementsInFaction2.Add(settlements[i]);
            }
        }

        var closestDistance = 0;

        for (int a = 0; a < settlementsInFaction.Count; ++a)
        {
            for (int b = 0; b < settlementsInFaction2.Count; ++b)
            {
                float distance = Vector3.Distance(settlementsInFaction[a].transform.position, settlementsInFaction2[b].transform.position);

                if (distance < closestDistance)
                {
                    closestDistance = (int)distance;
                }
            }
        }

        return closestDistance;
    }

    public bool CheckIfEnemy(Faction faction1, Faction faction2)
    {
        if (faction1.enemies.Contains(faction2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfAlly(Faction faction1, Faction faction2)
    {
        if (faction1.allies.Contains(faction2))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
