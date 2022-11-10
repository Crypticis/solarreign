using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class MobCommanderAI : CommanderAI
{
    public Targeting targeting;

    [Header("Steering Behaviors")]
    public SteerForPoint forPoint;
    public SteerForPursuit forPursuit;
    public SteerForWander forWander;
    public SteerForTether forTether;
    public SteerToFollow toFollow;

    private IEnumerator decisionsCoroutine;
    public ShipCommanderAIState shipShipCommanderAIState;

    // Start is called before the first frame update
    void Start()
    {
        forPoint = GetComponent<SteerForPoint>();
        forPursuit = GetComponent<SteerForPursuit>();
        forWander = GetComponent<SteerForWander>();
        forTether = GetComponent<SteerForTether>();
        toFollow = GetComponent<SteerToFollow>();
        targeting = GetComponent<Targeting>();

        decisionsCoroutine = CheckState();
        StartCoroutine(decisionsCoroutine);
    }

    public void MakeDecision()
    {
        switch (shipShipCommanderAIState)
        {
            case ShipCommanderAIState.pursuing:

                forPoint.enabled = false;
                forWander.enabled = false;
                forPursuit.enabled = true;

                break;

            case ShipCommanderAIState.refueling:

                break;

            case ShipCommanderAIState.wandering:

                forPoint.enabled = false;
                forPursuit.enabled = false;
                forWander.enabled = true;

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
