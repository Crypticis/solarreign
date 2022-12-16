using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnitySteer.Behaviors;

public class PirateCommanderAI : CommanderAI
{
    public Fleet fleet;
    public FleetRadar radar;
    public Transform target;
    public Vector3 wanderPoint;
    public bool isWandering = false;
    public FleetFaction fleetFaction;

    public float timer = 0f;
    public float decisionDelayMin = 2f;
    public float decisionDelayMax = 10f;
    public float recruitTimer = 0f;

    float timer2 = 0f;
    public float recruitTime = 30f;

    int cycleCounter;
    private IEnumerator decisionsCoroutine;
    public ShipCommanderAIState shipShipCommanderAIState;

    public Targeting targeting;

    [Header("Steering Behaviors")]

    public SteerToFollow follow;
    public SteerForWander wander;
    public SteerForPursuit pursue;
    public SteerForTether tether;

    public Transform formationRoot;

    public void Start()
    {
        fleet = GetComponent<AIFleet>();
        radar = GetComponentInChildren<FleetRadar>();
        fleetFaction = GetComponent<FleetFaction>();

        targeting = GetComponent<Targeting>();

        pursue = GetComponentInChildren<SteerForPursuit>();
        wander = GetComponentInChildren<SteerForWander>();
        follow = GetComponentInChildren<SteerToFollow>();
        tether = GetComponentInChildren<SteerForTether>();

        Invoke("StarterFleet", Random.Range(1f,2f));

        //forTether.TetherPosition = this.transform.position;

        decisionsCoroutine = CheckState();
        StartCoroutine(decisionsCoroutine);
    }

    public void MakeDecision()
    {
        cycleCounter++;

        if(cycleCounter >= 15)
        {
            if (fleet.fleet.Count < fleet.maxInFleet)
            {
                GameObject go = Instantiate(fleetFaction.faction.shipPrefabs[Random.Range(0, fleetFaction.faction.shipPrefabs.Length)], this.transform.position, Quaternion.identity, formationRoot);

                go.GetComponentInChildren<SteerToFollow>().Target = transform;
                go.GetComponentInChildren<SteerToFollow>().enabled = true;

                fleet.AddToFleet(go);
            }

            cycleCounter = 0;
        }

        switch (shipShipCommanderAIState)
        {
            case ShipCommanderAIState.pursuing:

                //forPoint.enabled = false;
                wander.enabled = false;
                follow.enabled = false;
                pursue.enabled = true;
                //targeting.target = target.gameObject;

                break;

            case ShipCommanderAIState.following:

                break;

            case ShipCommanderAIState.wandering:

                //forPoint.enabled = false;
                pursue.enabled = false;
                follow.enabled = false;

                target = null;

                wander.enabled = true;

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
                shipShipCommanderAIState = ShipCommanderAIState.wandering;
                MakeDecision();
                yield return new WaitForSeconds(2f);
            }
        }
    }

    public void StarterFleet()
    {
        int temp = Random.Range(fleet.maxInFleet / 2, fleet.maxInFleet);

        for (int i = 0; i < temp; i++)
        {
            GameObject go = Instantiate(fleetFaction.faction.shipPrefabs[Random.Range(0, fleetFaction.faction.shipPrefabs.Length)], this.transform.position, Quaternion.identity, formationRoot);

            go.GetComponentInChildren<SteerToFollow>().Target = transform;
            go.GetComponentInChildren<SteerToFollow>().enabled = true;

            fleet.AddToFleet(go);
        }
    }
}
