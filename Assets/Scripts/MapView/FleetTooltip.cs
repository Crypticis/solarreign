using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetTooltip : MonoBehaviour
{
    public TooltipTrigger tooltipTrigger;
    public Fleet fleet;
    public UniqueNPC uniqueNPC;

    public void Start()
    {
        uniqueNPC = GetComponent<UniqueNPC>();
        UpdateTooltip();
    }

    public void UpdateTooltip()
    {
        tooltipTrigger.content = ListToText(fleet.fleet);

        tooltipTrigger.header = uniqueNPC.npc.Name;
    }

    private string ListToText(List<FleetShip> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += listMember.ship.name + "\n";
        }
        return result;
    }
}
