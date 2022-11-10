using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FleetEngage : MonoBehaviour
{
    //public FleetRadar fleetRadar;
    //public NavMeshAgent agent;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.transform == fleetRadar.target)
    //    {
    //        if (other.GetComponent<PlayerMap>())
    //        {
    //            other.GetComponent<PlayerMap>().ShowEnemyUI();
    //            if(SurrenderHandler.instance.isCaptive == false)
    //                this.GetComponent<Interactable>().Interact(other.GetComponent<PlayerMap>());

    //            if(other.gameObject.activeSelf == true)
    //            {
    //                agent.isStopped = true;
    //            }
    //        }

    //        if (other.GetComponent<CommanderAI>())
    //        {
    //            //BattleSimulator.instance.SimulateBattle(other.GetComponent<Fleet>(), GetComponent<Fleet>());
    //            try { GetComponent<FleetTooltip>().UpdateTooltip(); } catch { Debug.Log("Unable to find Fleet Tooltip."); };
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.transform == fleetRadar.target)
    //    {
    //        ReEngageNavMeshAgent();
    //    }
    //}

    //public void ReEngageNavMeshAgent()
    //{
    //    GetComponent<NavMeshAgent>().isStopped = false;
    //}
}
