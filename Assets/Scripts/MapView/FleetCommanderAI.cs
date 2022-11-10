using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitySteer.Behaviors;

public class FleetCommanderAI : CommanderAI
{
    public Fleet fleet;
    public FleetRadar radar;
    public Transform target;
    public FleetResources resources;

    public List<GameObject> settlements = new List<GameObject>();
    public List<GameObject> enemySettlements = new List<GameObject>();
    public List<GameObject> allySettlements = new List<GameObject>();

    public int index = 0;
    public FleetFaction fleetFaction;

    //public UniqueNPC npc;
    private IEnumerator decisionsCoroutine;

    public ShipCommanderAIState shipShipCommanderAIState;

    public Targeting targeting;

    [Header("Steering Behaviors")]
    //public AIMPursue pursue;
    //public AIMOrbit orbit;
    //public AIMWander wander;
    //public AIMFollow follow;
    //public AIMFormationConfiguration formationConfiguration;

    public SteerToFollow follow;
    public SteerForWander wander;
    public SteerForPursuit pursue;
    public SteerForTether tether;

    public Transform formationRoot;

    int cycleCounter;

    public void Start()
    {
        //npc = GetComponent<UniqueNPC>();

        targeting = GetComponent<Targeting>();

        //orbit = GetComponentInChildren<AIMOrbit>();
        //pursue = GetComponentInChildren<AIMPursue>();
        //wander = GetComponentInChildren<AIMWander>();
        //follow = GetComponentInChildren<AIMFollow>();
        //formationConfiguration = GetComponentInChildren<AIMFormationConfiguration>();

        pursue = GetComponentInChildren<SteerForPursuit>();
        wander = GetComponentInChildren<SteerForWander>();
        follow = GetComponentInChildren<SteerToFollow>();
        tether = GetComponentInChildren<SteerForTether>();

        decisionsCoroutine = CheckState();
        StartCoroutine(decisionsCoroutine);

        Invoke("StarterFleet", Random.Range(1f, 5f));
    }

    public void MakeDecision()
    {
        cycleCounter++;

        if (cycleCounter >= 15)
        {
            if (fleet.fleet.Count < fleet.maxInFleet)
            {
                GameObject go = Instantiate(fleetFaction.faction.shipPrefabs[Random.Range(0, fleetFaction.faction.shipPrefabs.Length)], this.transform.position, Quaternion.identity);

                go.GetComponentInChildren<SteerToFollow>().Target = transform;
                go.GetComponentInChildren<SteerToFollow>().enabled = true;

                //go.GetComponentInChildren<AIMArrive>().Target = gameObject;
                //go.GetComponentInChildren<AIMArrive>().enabled = true;
                //go.GetComponentInChildren<AIMFollow>().Target = gameObject;
                //go.GetComponentInChildren<AIMFollow>().enabled = true;

                //go.GetComponentInChildren<AIMSeek>().GameObjects.Add(Player.playerInstance.gameObject);
                //go.GetComponentInChildren<AIMSeek>().enabled = true;

                fleet.AddToFleet(go);

                //go.GetComponentInChildren<AIMFormationArrow>().enabled = true;
                //go.GetComponentInChildren<AIMFormationArrow>().TargetObject = gameObject;

                //formationConfiguration.UpdateConfig();
            }

            cycleCounter = 0;
        }

        switch (shipShipCommanderAIState)
        {
            case ShipCommanderAIState.pursuing:

                targeting.target = target.gameObject;

                wander.enabled = false;
                follow.enabled = false;
                //orbit.enabled = false;
                pursue.enabled = true;

                break;

            case ShipCommanderAIState.recruiting:

                wander.enabled = false;
                follow.enabled = false;
                //orbit.enabled = false;
                pursue.enabled = true;

                if (allySettlements.Count <= 0)
                    FindAllySettlement();

                NextAllySettlement();

                if (!pursue.Quarry == NextAllySettlement())
                {
                    //pursue.GameObjects.Clear();

                    //pursue.GameObjects.Add(NextAllySettlement());

                    pursue.Quarry = NextAllySettlement().GetComponent<DetectableObject>();
                }

                break;

            case ShipCommanderAIState.refueling:
                break;

            case ShipCommanderAIState.wandering:

                pursue.enabled = false;
                follow.enabled = false;
                //orbit.enabled = true;

                target = null;
                pursue.Quarry = null;

                wander.enabled = true;

                break;

            case ShipCommanderAIState.hunting:

                //FindEnemySettlement();

                if (enemySettlements.Count > 0)
                {
                    for (int i = 0; i < enemySettlements.Count; i++)
                    {
                        if (target != enemySettlements[i])
                        {
                            if (enemySettlements[i].GetComponent<SettlementFleet>().fleet.Count <= fleet.fleet.Count)
                            {
                                target = enemySettlements[i].transform;

                                wander.enabled = false;
                                follow.enabled = false;

                                pursue.enabled = true;

                                //pursue.GameObjects.Clear();
                                pursue.Quarry = target.GetComponent<DetectableObject>();

                                return;
                            }

                            pursue.enabled = false;
                            follow.enabled = false;
                            //orbit.enabled = true;

                            target = null;

                            //wander.enabled = true;
                        }
                    }
                }

                break;

            default:
                break;
        }
    }

    public IEnumerator CheckState()
    {
        while (true)
        {
            if (targeting.target)
            {
                target = targeting.target.transform;
                shipShipCommanderAIState = ShipCommanderAIState.pursuing;

                MakeDecision();
                yield return new WaitForSeconds(2f);
            }

            if (NeedsFleetMembers())
            {
                shipShipCommanderAIState = ShipCommanderAIState.recruiting;

                MakeDecision();
                yield return new WaitForSeconds(2f);
            }
            //else if (EnemySettlementCount() > 0)
            //{
            //    for (int i = 0; i < enemySettlements.Count; i++)
            //    {
            //        if (enemySettlements[i].GetComponent<SettlementFleet>().fleet.Count <= fleet.fleet.Count)
            //        {
            //            shipShipCommanderAIState = ShipCommanderAIState.hunting;
            //            MakeDecision();
            //            yield return new WaitForSeconds(2f);
            //        }
            //    }

            //    shipShipCommanderAIState = ShipCommanderAIState.wandering;

            //    MakeDecision();
            //    yield return new WaitForSeconds(2f);
            //}
            else
            {
                shipShipCommanderAIState = ShipCommanderAIState.wandering;

                MakeDecision();
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public void FindSettlementList()
    {
        settlements = GameObject.FindGameObjectsWithTag("Settlement").ToList();
        settlements = settlements.OrderBy(x => (this.transform.position - x.transform.position).sqrMagnitude).ToList();
    }

    //public void FindEnemySettlement()
    //{
    //    FindSettlementList();

    //    enemySettlements.Clear();

    //    foreach (var faction in fleetFaction.faction.enemies)
    //    {
    //        for (int i = 0; i < settlements.Count; i++)
    //        {
    //            if (settlements[i].GetComponent<SettlementInfo>().faction == faction)
    //            {
    //                if (!enemySettlements.Contains(settlements[i]))
    //                {
    //                    enemySettlements.Add(settlements[i]);
    //                }
    //            }
    //        }
    //    }
    //}

    //public int EnemySettlementCount()
    //{
    //    FindEnemySettlement();
    //    return enemySettlements.Count;
    //}

    private bool NeedsFleetMembers()
    {
        if (fleet.fleet.Count < fleet.maxInFleet/* && npc.npc.money > 0*/)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool NeedsFuel()
    {
        if (resources.fuel < resources.totalFuelUpkeep)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public GameObject FindAllySettlement()
    {
        index = 0;

        FindSettlementList();

        allySettlements.Clear();

        for (int i = 0; i < settlements.Count; i++)
        {
            if(settlements[i].GetComponent<SettlementInfo>().faction == fleetFaction.faction)
            {
                allySettlements.Add(settlements[i]);
            }
        }

        //if(Vector3.Distance(transform.position, allySettlements[index].transform.position) <= 10f)
        //{
        //    index++;
        //}

        return allySettlements[index];
    }

    public GameObject NextAllySettlement()
    {
        //if (Vector3.Distance(transform.position, allySettlements[index].transform.position) <= 25f)
        //{
        //    index++;
        //}

        return allySettlements[index];
    }

    public void IncrementIndex()
    {
        if(index + 1 < allySettlements.Count)
        {
            index++;
        }
        else
        {
            FindAllySettlement();
        }
    }

    public void StarterFleet()
    {
        int num = 0;

        for (int i = 0; i < fleet.maxInFleet; i++)
        {
            //Invoke("SpawnShip", num);

            num++;
        }
    }

    public void SpawnShip()
    {
        GameObject go = Instantiate(fleetFaction.faction.shipPrefabs[Random.Range(0, fleetFaction.faction.shipPrefabs.Length)], this.transform.position, Quaternion.identity, formationRoot);

        //go.GetComponentInChildren<AIMArrive>().Target = gameObject;
        //go.GetComponentInChildren<AIMArrive>().enabled = true;
        go.GetComponentInChildren<SteerToFollow>().Target = transform;
        go.GetComponentInChildren<SteerToFollow>().enabled = true;

        //go.GetComponentInChildren<AIMFormationArrow>().enabled = true;
        //go.GetComponentInChildren<AIMFormationArrow>().TargetObject = gameObject;

        fleet.AddToFleet(go);

        //formationConfiguration.UpdateConfig();
    }
}
