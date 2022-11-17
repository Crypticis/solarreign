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
        if (StatManager.instance.skillpoints <= 0)
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

        skillPoints.text = StatManager.instance.skillpoints.ToString();

        pilotingProficiency.text = StatManager.instance.Piloting.currentLevel.ToString();

        missilesProficiency.text = StatManager.instance.Missile.currentLevel.ToString();

        projectilesProficiency.text = StatManager.instance.Projectile.currentLevel.ToString();

        energyProficiency.text = StatManager.instance.Energy.currentLevel.ToString();

        tradeSkill.text = StatManager.instance.Trade.currentLevel.ToString();

        commandSkill.text = StatManager.instance.commandLevel.ToString();

        tacticsSkill.text = StatManager.instance.tacticsLevel.ToString();

        logisticsSkill.text = StatManager.instance.logisticsLevel.ToString();

        productionSkill.text = StatManager.instance.productionLevel.ToString();
    }

    public void IncreaseCommand()
    {
        StatManager.instance.skillpoints--;
        StatManager.instance.commandLevel++;

        fleet.UpdateFleetMax();

        UpdateSkills();
    }

    public void IncreaseTactics()
    {
        StatManager.instance.skillpoints--;
        StatManager.instance.tacticsLevel++;

        UpdateSkills();
    }

    public void IncreaseLogistics()
    {
        StatManager.instance.skillpoints--;
        StatManager.instance.logisticsLevel++;

        UpdateSkills();
    }

    public void IncreaseProduction()
    {
        StatManager.instance.skillpoints--;
        StatManager.instance.productionLevel++;

        UpdateSkills();
    }
}
