using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementCombat : MonoBehaviour
{
    public SettlementInfo settlementInfo;
    public SettlementFleet settlementFleet;

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FleetCommanderAI>())
        {
            var ai = other.GetComponent<FleetCommanderAI>();

            if(ai.fleetFaction.faction != settlementInfo.faction && ai.target == this.gameObject.transform)
            {
                //BattleSimulator.instance.SimulateSiege(ai.GetComponent<AIFleet>(), settlementFleet);
                ai.GetComponent<FleetTooltip>().UpdateTooltip();
            }
        }
    }
}
