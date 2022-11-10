using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PilotRecruitmentSlot : MonoBehaviour
{
    public Pilot pilotToRecruit;
    public Button button;
    public float cost;
    public SpaceStationUI stationUI;
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text totalSkillText;
    TooltipTrigger tooltipTrigger;

    void Start()
    {
        var total = (pilotToRecruit.speedSkill + pilotToRecruit.firespeedSkill + pilotToRecruit.durabilitySkill + pilotToRecruit.damageSkill + pilotToRecruit.rangeSkill);
        totalSkillText.text = total.ToString();

        tooltipTrigger = GetComponentInChildren<TooltipTrigger>();
        tooltipTrigger.header = "Total Skills";
        tooltipTrigger.content = "Speed Skill: " + pilotToRecruit.speedSkill.ToString() + "\nFirespeed Skill: " + pilotToRecruit.firespeedSkill.ToString() + "\nDurability Skill: " + pilotToRecruit.durabilitySkill.ToString() + "\nDamage Skill: " + pilotToRecruit.damageSkill.ToString() + "\nRange Skill: " + pilotToRecruit.rangeSkill.ToString();
    }

    void Update()
    {
        if (pilotToRecruit == null)
        {
            Destroy(gameObject);
        }

        if (nameText.text != pilotToRecruit.name)
        {
            nameText.text = pilotToRecruit.name;
        }

        if (costText.text != cost.ToString())
        {
            costText.text = cost.ToString();
        }
    }

    public void AddToFleet()
    {
        if (StatManager.instance.playerStatsObject.currentMoney >= cost)
        {
            Player.playerInstance.fleet.AddToPilots(pilotToRecruit);
            StatManager.instance.playerStatsObject.currentMoney -= cost;
            Ticker.Ticker.AddItem("Recruited " + pilotToRecruit.name + " for " + cost.ToString());
            stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots.Remove(pilotToRecruit);
            pilotToRecruit = null;
            Destroy(gameObject);
        }
        else if (StatManager.instance.playerStatsObject.currentMoney >= cost && Player.playerInstance.fleet.fleet.Count >= Player.playerInstance.fleet.maxInFleet)
        {
            Ticker.Ticker.AddItem("Unable to recruit. Need more fleet space.");
        }
        else
        {
            Ticker.Ticker.AddItem("Unable to recruit. Need more money");
        }
    }
}
