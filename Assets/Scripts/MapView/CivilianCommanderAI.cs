using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitySteer.Behaviors;

public class CivilianCommanderAI : CommanderAI
{
    Fleet fleet;
    //NavMeshAgent agent;
    FleetRadar radar;
    public Transform target;
    public FleetResources resources;
    public List<Transform> settlements = new List<Transform>();

    public Vector3 wanderPoint;
    public bool isWandering = false;
    public GameObject prefab;
    public FleetFaction fleetFaction;

    public float timer = 0f;
    public float decisionDelayMin = 2f;
    public float decisionDelayMax = 10f;

    public Transform targetSettlement;
    public Transform originSettlement;

    public NPCTraderShip tradeship;

    public GameObject[] cargoPods;

    private IEnumerator decisionsCoroutine;
    public ShipCommanderAIState shipShipCommanderAIState;

    public Targeting targeting;
    public WarpJumpAI warp;
    public Transform destination;
    //public DestinationType destinationType;

    [Header("Steering Behaviors")]
    //public AIMPursue pursue;
    //public AIMWander wander;
    //public AIMFollow follow;

    public SteerToFollow follow;
    public SteerForWander wander;
    public SteerForPursuit pursue;
    public SteerForTether tether;

    public void Start()
    {
        fleet = GetComponent<AIFleet>();
        radar = GetComponentInChildren<FleetRadar>();
        fleetFaction = GetComponent<FleetFaction>();
        tradeship = GetComponent<NPCTraderShip>();
        targeting = GetComponent<Targeting>();

        pursue = GetComponentInChildren<SteerForPursuit>();
        wander = GetComponentInChildren<SteerForWander>();
        follow = GetComponentInChildren<SteerToFollow>();
        tether = GetComponentInChildren<SteerForTether>();

        targeting = GetComponent<Targeting>();

        GetComponentInChildren<MeshRenderer>().material = fleetFaction.faction.materialDetail;

        //if(cargoPods.Length > 0)
        //    for (int i = 0; i < cargoPods.Length; i++)
        //    {
        //        cargoPods[i].GetComponent<MeshRenderer>().material = fleetFaction.faction.materialDetail;
        //    }

        int temp = Random.Range(1, fleet.maxInFleet);

        for (int i = 0; i < temp; i++)
        {
            fleet.AddToFleet(fleetFaction.faction.shipPrefabs[Random.Range(0, fleetFaction.faction.shipPrefabs.Length)]);
        }

        //if (destinationType == DestinationType.warpgate)
        //{
        //    destination = FindGate().transform;
        //}
        //else if (destinationType == DestinationType.settlement)
        //{
        //    destination = FindSettlement().transform;
        //}

        decisionsCoroutine = CheckState();
        StartCoroutine(decisionsCoroutine);

       // FindAllySettlements();
    }

    public void MakeDecision()
    {
        switch (shipShipCommanderAIState)
        {
            case ShipCommanderAIState.pursuing:

                //wander.enabled = false;
                follow.enabled = false;

                pursue.enabled = true;
                targeting.target = target.gameObject;

                break;

            case ShipCommanderAIState.refueling:

                //if (!targetSettlement)
                //{
                //    if(settlements.Count > 0)
                //    {
                //        targetSettlement = settlements[Random.Range(0, settlements.Count)];
                //    }
                //    else
                //    {
                //        FindAllySettlements();
                //        targetSettlement = settlements[Random.Range(0, settlements.Count)];
                //    }
                //}

                //wander.enabled = false;

                follow.enabled = false;

                pursue.enabled = true;

                pursue.Quarry = destination.GetComponent<DetectableObject>();

                //if (Vector3.Distance(pursue.Quarry.transform.position, this.transform.position) > 1000)
                //{
                //    //warp.StartWarp(pursue.Quarry.transform);
                //}

                break;

            case ShipCommanderAIState.wandering:

                follow.enabled = false;
                pursue.enabled = false;
                target = null;

                //wander.enabled = true;

                break;

            default:
                break;
        }
    }

    public IEnumerator CheckState()
    {
        while (true)
        {
            if (targeting.target != null)
            {
                target = targeting.target.transform;
                shipShipCommanderAIState = ShipCommanderAIState.pursuing;

                MakeDecision();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                shipShipCommanderAIState = ShipCommanderAIState.refueling;
                MakeDecision();
                yield return new WaitForSeconds(2f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (targetSettlement)
        {
            if(other.transform == targetSettlement)
            {
                for (int i = 0; i < tradeship.tradeGoods.Count; i++)
                {
                    //other.GetComponent<SettlementTrader>().inventory.AddItem(other.GetComponent<SettlementTrader>().shopItems[i].item, tradeship.tradeGoods[i].amount);

                    //other.GetComponent<SettlementTrader>().inventory.itemSlots[i].amount += tradeship.tradeGoodAmount[i];

                    other.GetComponent<SettlementTrader>().inventory.AddItem(tradeship.tradeGoods[i].item, tradeship.tradeGoods[i].amount);

                    for (int j = 0; j < other.GetComponent<SettlementTrader>().shopItems.Length; j++)
                    {
                        if(other.GetComponent<SettlementTrader>().shopItems[j].item == tradeship.tradeGoods[i].item)
                        {
                            other.GetComponent<SettlementTrader>().shopItems[j].currentPrice *= .99f;
                        }
                    }
                    
                }

                Defeat();
            }
        }
    }

    public void FindAllySettlements()
    {
        var temp = GameObject.FindGameObjectsWithTag("Settlement");

        for (int i = 0; i < temp.Length; i++)
        {
            if (temp[i].GetComponent<SettlementInfo>().faction == fleetFaction.faction)
            {
                settlements.Add(temp[i].transform);
            }
        }

        if (settlements.Count == 0)
        {
            isWandering = true;
            return;
        }
    }
    public GameObject FindGate()
    {
        var temp = GameObject.FindGameObjectsWithTag("Warp Gate");
        return temp[Random.Range(0, temp.Length)];
    }

    public GameObject FindSettlement()
    {
        var temp = GameObject.FindGameObjectsWithTag("Settlement");
        return temp[Random.Range(0, temp.Length)];
    }

    //public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    //{
    //    Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

    //    randomDirection += origin;

    //    NavMeshHit navHit;

    //    NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

    //    return navHit.position;
    //}

    //public override void Defeat()
    //{
    //    Destroy(gameObject);
    //}
}
//public enum DestinationType
//{
//    warpgate,
//    settlement,
//    asteroidbelt
//}
