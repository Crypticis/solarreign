using System.Collections;
using UnityEngine;
using UnitySteer.Behaviors;
//using Polarith.AI.Move;
public class ShipAI : CommanderAI
{
    public Targeting targeting;

    [Header("Steering Behaviors")]
    public SteerToFollow follow;
    public SteerForWander wander;
    public SteerForPursuit pursue;
    public SteerForTether tether;

    private IEnumerator decisionsCoroutine;
    public ShipCommanderAIState shipCommanderAIState;

    void Start()
    {
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
        switch (shipCommanderAIState)
        {
            case ShipCommanderAIState.pursuing:

                follow.enabled = false;
                tether.enabled = false;
                pursue.enabled = true;
                
                if(targeting.combatState != Targeting.CombatState.disengage)
                    wander.enabled = false;

                break;

            case ShipCommanderAIState.wandering:

                follow.enabled = false;
                tether.enabled = true;
                pursue.enabled = false;
                wander.enabled = true;

                break;

            case ShipCommanderAIState.following:

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
                shipCommanderAIState = ShipCommanderAIState.pursuing;

                MakeDecision();
                yield return new WaitForSeconds(2f);
            }
            else
            {
                if(follow.Target != null)
                {
                    shipCommanderAIState = ShipCommanderAIState.following;
                    MakeDecision();
                    yield return new WaitForSeconds(2f);
                }
                else
                {
                    shipCommanderAIState = ShipCommanderAIState.wandering;
                    MakeDecision();
                    yield return new WaitForSeconds(2f);
                }
            }
        }
    }
}
