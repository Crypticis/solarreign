using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public TMP_Text pilotingProficiency;
    public TMP_Text missilesProficiency;
    public TMP_Text projectilesProficiency;
    public TMP_Text energyProficiency;

    public TMP_Text commandSkill;
    public TMP_Text tacticsSkill;
    public TMP_Text logisticsSkill;
    public TMP_Text tradeSkill;
    public TMP_Text productionSkill;

    public TMP_Text skillPoints;

    public Button[] buttons;

    public PlayerFleet fleet;

    public void OnEnable()
    {
        UpdateSkills();
    }

    public void UpdateSkills()
    {
        if (StatManager.instance.playerStatsObject.skillpoints <= 0)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = false;
            }
        }
        else
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].interactable = true;
            }
        }

        skillPoints.text = StatManager.instance.playerStatsObject.skillpoints.ToString();

        pilotingProficiency.text = StatManager.instance.playerStatsObject.Piloting.currentLevel.ToString();

        missilesProficiency.text = StatManager.instance.playerStatsObject.Missile.currentLevel.ToString();

        projectilesProficiency.text = StatManager.instance.playerStatsObject.Projectile.currentLevel.ToString();

        energyProficiency.text = StatManager.instance.playerStatsObject.Energy.currentLevel.ToString();

        tradeSkill.text = StatManager.instance.playerStatsObject.Trade.currentLevel.ToString();

        commandSkill.text = StatManager.instance.playerStatsObject.commandLevel.ToString();

        tacticsSkill.text = StatManager.instance.playerStatsObject.tacticsLevel.ToString();

        logisticsSkill.text = StatManager.instance.playerStatsObject.logisticsLevel.ToString();

        productionSkill.text = StatManager.instance.playerStatsObject.productionLevel.ToString();
    }

    public void IncreaseCommand()
    {
        StatManager.instance.playerStatsObject.skillpoints--;
        StatManager.instance.playerStatsObject.commandLevel++;

        fleet.UpdateFleetMax();

        UpdateSkills();
    }

    public void IncreaseTactics()
    {
        StatManager.instance.playerStatsObject.skillpoints--;
        StatManager.instance.playerStatsObject.tacticsLevel++;

        UpdateSkills();
    }

    public void IncreaseLogistics()
    {
        StatManager.instance.playerStatsObject.skillpoints--;
        StatManager.instance.playerStatsObject.logisticsLevel++;

        UpdateSkills();
    }

    public void IncreaseProduction()
    {
        StatManager.instance.playerStatsObject.skillpoints--;
        StatManager.instance.playerStatsObject.productionLevel++;

        UpdateSkills();
    }
}
