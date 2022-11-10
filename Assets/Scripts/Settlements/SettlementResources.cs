using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementResources : MonoBehaviour
{
    public SettlementInfo settlementInfo;
    public SettlementRecruitment settlementRecruitment;
    public ResourceType resource;

    private IEnumerator coroutine;

    SettlementTrader trader;

    public Item[] resourceItems;

    private void Start()
    {
        settlementInfo = GetComponent<SettlementInfo>();
        settlementRecruitment = GetComponent<SettlementRecruitment>();
        trader = GetComponent<SettlementTrader>();
        coroutine = ResourceCalculation(15f);
        StartCoroutine(coroutine);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FleetCommanderAI>())
        {
            var ai = other.GetComponent<FleetCommanderAI>();
            var fleet = ai.fleet;

            if (ai.shipShipCommanderAIState == FleetCommanderAI.ShipCommanderAIState.recruiting)
            {
                if (settlementRecruitment.purchasableShips.Count > 0)
                {
                    var temp = Random.Range(1, settlementRecruitment.purchasableShips.Count);

                    for (int i = 0; i < settlementRecruitment.purchasableShips.Count; i++)
                    {
                        //if (ai.GetComponent<UniqueNPC>().npc.money >= settlementRecruitment.purchasableShips[i].GetComponent<HUDElements>().recruitmentCost)
                        //{
                        //    GameObject go = Instantiate(settlementRecruitment.purchasableShips[i], other.transform.position, Quaternion.identity);
                        //    go.GetComponentInChildren<AIMArrive>().Target = ai.gameObject;
                        //    go.GetComponentInChildren<AIMArrive>().enabled = true;
                        //    go.GetComponentInChildren<AIMFollow>().Target = ai.gameObject;
                        //    go.GetComponentInChildren<AIMFollow>().enabled = true;
                        //    fleet.AddToFleet(go);

                        //    ai.GetComponent<UniqueNPC>().npc.money -= settlementRecruitment.purchasableShips[i].GetComponent<HUDElements>().recruitmentCost;
                        //    settlementRecruitment.purchasableShips.Remove(settlementRecruitment.purchasableShips[i]);
                        //    ai.GetComponent<FleetTooltip>().UpdateTooltip();
                        //}

                        GameObject go = Instantiate(settlementRecruitment.purchasableShips[i], other.transform.position, Quaternion.identity);
                        //go.GetComponentInChildren<AIMArrive>().Target = ai.gameObject;
                        //go.GetComponentInChildren<AIMArrive>().enabled = true;
                        //go.GetComponentInChildren<AIMFollow>().Target = ai.gameObject;
                        //go.GetComponentInChildren<AIMFollow>().enabled = true;
                        fleet.AddToFleet(go);

                        fleet.GetComponent<FleetCommanderAI>().IncrementIndex();

                        settlementRecruitment.purchasableShips.Remove(settlementRecruitment.purchasableShips[i]);
                        //ai.GetComponent<FleetTooltip>().UpdateTooltip();
                    }

                    //if(ai.GetComponent<UniqueNPC>().npc.money >= settlementRecruitment.purchasableShips[0].GetComponent<HUDElements>().recruitmentCost)
                    //{
                    //    fleet.AddToFleet(settlementRecruitment.purchasableShips[0]);
                    //    ai.GetComponent<UniqueNPC>().npc.money -= settlementRecruitment.purchasableShips[0].GetComponent<HUDElements>().recruitmentCost;
                    //    settlementRecruitment.purchasableShips.Remove(settlementRecruitment.purchasableShips[0]);
                    //}
                }
            }
            //if (ai.shipShipCommanderAIState == FleetCommanderAI.ShipCommanderAIState.refueling)
            //{
            //    ai.resources.fuel += ai.resources.totalFuelUpkeep * 5;
            //}
        }
    }

    private IEnumerator ResourceCalculation(float waitTime)
    {
        while (true)
        {
            if(resource == ResourceType.crew)
            {
                trader.inventory.AddItem(resourceItems[0], 1);
            }

            if (resource == ResourceType.spaceMetal)
            {
                trader.inventory.AddItem(resourceItems[1], 1);
            }

            if (resource == ResourceType.ice)
            {
                trader.inventory.AddItem(resourceItems[2], 1);
            }

            if (resource == ResourceType.water)
            {
                trader.inventory.AddItem(resourceItems[3], 1);
            }

            if (resource == ResourceType.scrap)
            {
                trader.inventory.AddItem(resourceItems[4], 1);
            }

            if (resource == ResourceType.food)
            {
                trader.inventory.AddItem(resourceItems[5], 1);
            }

            if (resource == ResourceType.rawOres)
            {
                trader.inventory.AddItem(resourceItems[6], 1);
            }

            if (resource == ResourceType.gems)
            {
                trader.inventory.AddItem(resourceItems[7], 1);
            }

            if (resource == ResourceType.organics)
            {
                trader.inventory.AddItem(resourceItems[8], 1);
            }

            if (resource == ResourceType.gas)
            {
                trader.inventory.AddItem(resourceItems[9], 1);
            }

            yield return new WaitForSeconds(waitTime);
            //print("ResourceCalculation " + Time.time);
        }
    }
}
public enum ResourceType
{
    crew,
    spaceMetal,
    ice,
    water,
    scrap,
    food,
    rawOres,
    gems,
    organics,
    gas
}