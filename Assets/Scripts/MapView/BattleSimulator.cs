using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleSimulator : MonoBehaviour
{
    //public static BattleSimulator instance;

    //private void Awake()
    //{
    //    if (instance != null)
    //    {
    //        Destroy(gameObject);
    //    }
    //    else
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //}

    //public void SimulateBattle(Fleet fleet1, Fleet fleet2)
    //{
    //    if(fleet1.GetComponent<NavMeshAgent>().isActiveAndEnabled && fleet2.GetComponent<NavMeshAgent>().isActiveAndEnabled)
    //    {
    //        fleet1.GetComponent<NavMeshAgent>().isStopped = true;
    //        fleet2.GetComponent<NavMeshAgent>().isStopped = true;
    //    }
    //    else
    //    {
    //        return;
    //    }

    //    for (int i = 0; i < fleet1.fleet.Count; i++)
    //    {
    //        if (fleet1.fleet[i].health > 0)
    //        {
    //            if (fleet1.fleet[i].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter)
    //            {
    //                for (int j = 0; j < fleet2.fleet.Count; j++)
    //                {
    //                    if(fleet2.fleet[j].health > 0)
    //                    {
    //                        if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber)
    //                        {
    //                            //Debug.Log("there is a bomber in the enemy fleet and I am a fighter");
    //                            var roll = Random.Range(1, 10) + 2;

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter)
    //                        {
    //                            //Debug.Log("there is a fighter in the enemy fleet and I am a fighter");
    //                            var roll = Random.Range(1, 10);

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser)
    //                        {
    //                            var roll = Random.Range(1, 10) - 2;

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                            //Debug.Log("there is a cruiser in the enemy fleet and I am a fighter");
    //                        }
    //                        else
    //                        {
    //                            var roll = Random.Range(1, 10);

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                    }
    //                }
    //            }

    //            if (fleet1.fleet[i].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber)
    //            {
    //                for (int j = 0; j < fleet2.fleet.Count; j++)
    //                {
    //                    if(fleet2.fleet[j].health > 0)
    //                    {
    //                        if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser)
    //                        {
    //                            //Debug.Log("there is a cruiser in the enemy fleet and I am a bomber");

    //                            var roll = Random.Range(1, 10) + 2;

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber)
    //                        {
    //                            //Debug.Log("there is a bomber in the enemy fleet and I am a bomber");

    //                            var roll = Random.Range(1, 10);

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter)
    //                        {
    //                            //Debug.Log("there is a fighter in the enemy fleet and I am a bomber");

    //                            var roll = Random.Range(1, 10) - 2;

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else
    //                        {
    //                            var roll = Random.Range(1, 10);

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                    }
    //                }
    //            }

    //            if (fleet1.fleet[i].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser)
    //            {
    //                for (int j = 0; j < fleet2.fleet.Count; j++)
    //                {
    //                    if(fleet2.fleet[j].health > 0)
    //                    {
    //                        if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter)
    //                        {
    //                            //Debug.Log("there is a fighter in the enemy fleet and I am a cruiser");

    //                            var roll = Random.Range(1, 10) + 2;

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser)
    //                        {
    //                            //Debug.Log("there is a cruiser in the enemy fleet and I am a cruiser");

    //                            var roll = Random.Range(1, 10);

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else if (fleet2.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber)
    //                        {
    //                            //Debug.Log("there is a bomber in the enemy fleet and I am a cruiser");

    //                            var roll = Random.Range(1, 10) - 2;

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                        else
    //                        {
    //                            var roll = Random.Range(1, 10);

    //                            if (roll > 5)
    //                            {
    //                                SimulateBattle(fleet1.fleet[i], fleet2.fleet[j], fleet1, fleet2);
    //                            }
    //                            else
    //                            {
    //                                SimulateBattle(fleet2.fleet[j], fleet1.fleet[i], fleet2, fleet1);
    //                            }

    //                            break;
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //public void SimulateBattle(FleetShip fleetShip1, FleetShip fleetShip2, Fleet fleet1, Fleet fleet2)
    //{
    //    //Debug.Log("Simulating battle");

    //    Damage(fleetShip1, fleetShip2);
    //    Damage(fleetShip2, fleetShip1);

    //    //Debug.Log(fleetShip1.health);
    //    //Debug.Log(fleetShip2.health);

    //    if (fleetShip1.health <= 0 || fleetShip2.health <= 0)
    //    {
    //        CheckFleet(fleet1, fleet2);

    //        return;
    //    }
    //    else
    //    {
    //        SimulateBattle(fleetShip1, fleetShip2, fleet1, fleet2);
    //    }
    //}

    //public void Damage(FleetShip fleetShip1, FleetShip fleetShip2)
    //{
    //    fleetShip1.health -= fleetShip2.attack;
    //}

    //public void CheckFleet(Fleet fleet1, Fleet fleet2)
    //{
    //    int strength1 = 0;

    //    for (int i = 0; i < fleet1.fleet.Count; i++)
    //    {
    //        if (fleet1.fleet[i].health > 0)
    //        {
    //            strength1++;
    //        }
    //    }

    //    int strength2 = 0;

    //    for (int i = 0; i < fleet2.fleet.Count; i++)
    //    {
    //        if (fleet2.fleet[i].health > 0)
    //        {
    //            strength2++;
    //        }
    //    }

    //    if (strength1 > strength2)
    //    {
    //        fleet1.GetComponent<NavMeshAgent>().isStopped = false;
    //        SetDefeat(fleet2);
    //        fleet1.UpdateFleet(); 
    //    } 
    //    else
    //    {
    //        fleet2.GetComponent<NavMeshAgent>().isStopped = false;
    //        SetDefeat(fleet1);
    //        fleet2.UpdateFleet();
    //    }
    //}

    //public void SetDefeat(Fleet fleet)
    //{
    //    fleet.GetComponent<CommanderAI>().Defeat();
    //}

    //public void SimulateSiege(Fleet fleet1, SettlementFleet settlementFleet)
    //{
    //    fleet1.GetComponent<NavMeshAgent>().isStopped = true;

    //    for (int i = 0; i < fleet1.fleet.Count; i++)
    //    {
    //        if (fleet1.fleet[i].health > 0)
    //        {
    //            if (fleet1.fleet[i].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter)
    //            {
    //                for (int j = 0; j < settlementFleet.fleet.Count; j++)
    //                {
    //                    if (settlementFleet.fleet[j].health > 0)
    //                    {
    //                        if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                        else if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                        else if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                    }
    //                }
    //            }

    //            if (fleet1.fleet[i].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber)
    //            {
    //                for (int j = 0; j < settlementFleet.fleet.Count; j++)
    //                {
    //                    if (settlementFleet.fleet[j].health > 0)
    //                    {
    //                        if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                        else if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                        else if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                    }
    //                }
    //            }

    //            if (fleet1.fleet[i].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser)
    //            {
    //                for (int j = 0; j < settlementFleet.fleet.Count; j++)
    //                {
    //                    if (settlementFleet.fleet[j].health > 0)
    //                    {
    //                        if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.fighter && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                        else if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.cruiser && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                        else if (settlementFleet.fleet[j].prefab.GetComponent<FactionMember>().shipType == ShipType.bomber && settlementFleet.fleet[i].health > 0)
    //                        {
    //                            SimulateSiegeBattle(fleet1.fleet[i], settlementFleet.fleet[j], fleet1, settlementFleet);
    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    //public void SimulateSiegeBattle(FleetShip fleetShip1, FleetShip fleetShip2, Fleet fleet1, SettlementFleet fleet2)
    //{
    //    //Debug.Log("Simulating battle");

    //    Damage(fleetShip1, fleetShip2);
    //    Damage(fleetShip2, fleetShip1);

    //    //Debug.Log(fleetShip1.health);
    //    //Debug.Log(fleetShip2.health);

    //    if (fleetShip1.health <= 0 || fleetShip2.health <= 0)
    //    {
    //        CheckSiegeFleet(fleet1, fleet2);

    //        return;
    //    }
    //    else
    //    {
    //        SimulateSiegeBattle(fleetShip1, fleetShip2, fleet1, fleet2);
    //    }
    //}

    //public void CheckSiegeFleet(Fleet fleet1, SettlementFleet fleet2)
    //{
    //    int strength1 = 0;

    //    for (int i = 0; i < fleet1.fleet.Count; i++)
    //    {
    //        if (fleet1.fleet[i].health > 0)
    //        {
    //            strength1++;
    //        }
    //    }

    //    int strength2 = 0;

    //    for (int i = 0; i < fleet2.fleet.Count; i++)
    //    {
    //        if (fleet2.fleet[i].health > 0)
    //        {
    //            strength2++;
    //        }
    //    }

    //    if (strength1 > strength2)
    //    {
    //        fleet1.GetComponent<NavMeshAgent>().isStopped = false;
    //        //Debug.Log("Defeating " + fleet2);
    //        fleet2.Defeat(fleet1.GetComponent<FleetFaction>().faction);
    //        fleet1.UpdateFleet();
    //        fleet2.GetComponent<SpaceStation>().npcOwner = fleet1.GetComponent<UniqueNPC>().npc;
    //        fleet2.GetComponent<SpaceStation>().isPlayerOwned = false;
    //        fleet1.GetComponent<FleetStats>().Command.AddExp(25);
    //    }
    //    else
    //    {
    //        //Debug.Log("Defeating " + fleet1);
    //        SetDefeat(fleet1);
    //        fleet2.UpdateFleet();
    //        fleet2.GetComponent<SpaceStation>().isPlayerOwned = false;
    //        //fleet2.GetComponent<FleetStats>().Command.AddExp(25);
    //    }
    //}
}
