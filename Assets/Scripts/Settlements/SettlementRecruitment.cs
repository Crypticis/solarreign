using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementRecruitment : MonoBehaviour
{
    public List<GameObject> purchasableShips = new List<GameObject>();
    public int maxPurchasableShips = 10;

    public List<Pilot> recruitablePilots = new List<Pilot>();
    public int maxRecruitablePilots = 10;

    SettlementInfo info;
    public IEnumerator RecruitCycle;


    void Start()
    {
        info = GetComponent<SettlementInfo>();

        // Fleet Stuff

        RecruitCycle = RefillRecruits(10f);
        StartCoroutine(RecruitCycle);

        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            if (purchasableShips.Count < maxPurchasableShips)
            {
                if (info.faction.shipPrefabs.Length >= 4)
                {
                    var temp = Random.Range(1, 10);

                    if (temp > 9)
                    {
                        purchasableShips.Add(info.faction.shipPrefabs[4]);
                    }
                    else if (temp > 7)
                    {
                        purchasableShips.Add(info.faction.shipPrefabs[Random.Range(2, 3)]);
                    }
                    else
                    {
                        purchasableShips.Add(info.faction.shipPrefabs[Random.Range(0, 1)]);
                    }
                }
                else
                {
                    purchasableShips.Add(info.faction.shipPrefabs[Random.Range(0, info.faction.shipPrefabs.Length)]);
                }
            }
        }

        for (int i = 0; i < Random.Range(3,6); i++)
        {
            if (recruitablePilots.Count < maxRecruitablePilots)
            {
                recruitablePilots.Add(CreateNewPilot());
            }
        }
    }

    IEnumerator RefillRecruits(float time)
    {
        while (true)
        {
            for (int i = 0; i < Random.Range(1, 5); i++)
            {
                if (purchasableShips.Count < maxPurchasableShips)
                {
                    if (info.faction.shipPrefabs.Length >= 4)
                    {
                        var temp = Random.Range(1, 10);

                        if (temp > 9)
                        {
                            purchasableShips.Add(info.faction.shipPrefabs[4]);
                        }
                        else if (temp > 7)
                        {
                            purchasableShips.Add(info.faction.shipPrefabs[Random.Range(2, 3)]);
                        }
                        else
                        {
                            purchasableShips.Add(info.faction.shipPrefabs[Random.Range(0, 1)]);
                        }
                    }
                    else
                    {
                        purchasableShips.Add(info.faction.shipPrefabs[Random.Range(0, info.faction.shipPrefabs.Length)]);
                    }
                }
            }

            for (int i = 0; i < Random.Range(1, 2); i++)
            {
                if (recruitablePilots.Count < maxRecruitablePilots)
                {
                    recruitablePilots.Add(CreateNewPilot());
                }
            }

            yield return new WaitForSeconds(time);
        }
    }

    public Pilot CreateNewPilot()
    {
        Pilot newPilot = new Pilot();

        newPilot.name = StatManager.instance.GeneratePilotName();

        newPilot.ResetLevels();
        newPilot.RandomizeStats();

        return newPilot;
    }
}
