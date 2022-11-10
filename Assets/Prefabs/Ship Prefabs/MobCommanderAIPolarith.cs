using System.Collections;
using UnityEngine;
using UnitySteer.Behaviors;
//using Polarith.AI.Move;
public class MobCommanderAIPolarith : CommanderAI
{
    public Targeting targeting;

    [Header("Steering Behaviors")]
    //public AIMPursue pursue;
    //public AIMWander wander;
    //public AIMFollow follow;

    public SteerToFollow follow;
    public SteerForWander wander;
    public SteerForPursuit pursue;
    public SteerForTether tether;

    private IEnumerator decisionsCoroutine;
    public ShipCommanderAIState shipShipCommanderAIState;

    void Start()
    {
        //pursue = GetComponentInChildren<AIMPursue>();
        //wander = GetComponentInChildren<AIMWander>();
        //follow = GetComponentInChildren<AIMFollow>();

        pursue = GetComponentInChildren<SteerForPursuit>();
        wander = GetComponentInChildren<SteerForWander>();
        follow = GetComponentInChildren<SteerToFollow>();
        tether = GetComponentInChildren<SteerForTether>();

        targeting = GetComponent<Targeting>();

        decisionsCoroutine = CheckState();
        StartCoroutine(decisionsCoroutine);
    }

    public void MakeDecision()
    {
        switch (shipShipCommanderAIState)
        {
            case ShipCommanderAIState.pursuing:

                follow.enabled = false;
                tether.enabled = false;
                pursue.enabled = true;
                
                if(targeting.combatState != Targeting.CombatState.disengage)
                    wander.enabled = false;

                break;

            case ShipCommanderAIState.refueling:

                break;

            case ShipCommanderAIState.wandering:

                follow.enabled = true;
                tether.enabled = false;
                pursue.enabled = false;
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
}
