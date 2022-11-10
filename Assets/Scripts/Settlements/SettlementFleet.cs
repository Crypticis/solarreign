using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementFleet : Fleet
{
    public SettlementInfo settlementInfo;

    private void Update()
    {
        if (fleet.Count < maxInFleet)
        {
            AddToFleet(settlementInfo.faction.shipPrefabs[Random.Range(0, settlementInfo.faction.shipPrefabs.Length)]);
        }
    }

    public void Defeat(Faction faction)
    {
        settlementInfo.faction = faction;
        fleet.Clear();
    }
}
