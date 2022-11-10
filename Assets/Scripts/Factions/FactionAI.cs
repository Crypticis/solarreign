using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionAI : MonoBehaviour
{
    public Faction faction;
    IEnumerator coroutine;

    public void Start()
    {
        coroutine = MakeDecision();
        StartCoroutine(coroutine);
    }

    public IEnumerator MakeDecision()
    {
        if(!faction.isPlayerLeader)
            while (true)
            {
                if (faction.enemies.Count > 1)
                {
                    for (int i = 0; i < faction.enemies.Count; i++)
                    {
                        if (faction.enemies[i].name != "Pirates" && FactionManager.instance.DetermineIfStronger(faction.enemies[i], faction))
                        {
                            if (FactionManager.instance.WouldAcceptPeace(faction, faction.enemies[i]))
                            {
                                FactionManager.instance.MakePeace(faction, faction.enemies[i]);
                                break;
                            }
                        }
                    }
                }

                for (int i = 0; i < faction.allies.Count; i++)
                {
                    if (FactionManager.instance.WouldRemoveAlly(faction, faction.allies[i]))
                    {
                        FactionManager.instance.RemoveAlly(faction, faction.allies[i]);
                    }
                }

                for (int i = 0; i < FactionManager.instance.factions.Length; i++)
                {
                    if (FactionManager.instance.factions[i].name != "Pirates" && FactionManager.instance.factions[i].name != "Neutral" && FactionManager.instance.factions[i] != faction)
                    {
                        if (!faction.allies.Contains(FactionManager.instance.factions[i]) && !faction.enemies.Contains(FactionManager.instance.factions[i]))
                        {
                            if (FactionManager.instance.WantsAlly(faction))
                            {
                                if (FactionManager.instance.WouldAcceptAlly(faction, FactionManager.instance.factions[i]))
                                {
                                    FactionManager.instance.MakeAlly(faction, FactionManager.instance.factions[i]);
                                }
                            }
                        }

                        if (!faction.enemies.Contains(FactionManager.instance.factions[i]) && !faction.allies.Contains(FactionManager.instance.factions[i]))
                        {
                            if (FactionManager.instance.DetermineIfStronger(faction, FactionManager.instance.factions[i]))
                            {
                                FactionManager.instance.MakeWar(faction, FactionManager.instance.factions[i]);
                            }
                        }
                    }
                }

            //Debug.Log("1");
            yield return new WaitForSeconds(Random.Range(900f, 1800f));
            //GameManager.instance.FactionColorUpdate();
        }
    }
}
