using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetPowerUI : MonoBehaviour
{
    public AIFleet fleet;
    public TMP_Text powerText;
    public FleetFaction fleetFaction;
    public SpriteRenderer factionIcon;
    Color enemyColor;
    Color allyColor;
    IEnumerator coroutine;

    private void Start()
    {
        fleet = GetComponent<AIFleet>(); 
        enemyColor = new Color(0.8392157f, 0.1882353f, 0.1921569f);
        allyColor = new Color(0f, 0.5176471f, 0.8901961f);
        coroutine = UpdateText();
        StartCoroutine(coroutine);
    }

    public IEnumerator UpdateText()
    {
        if(powerText.text != fleet.fleet.Count.ToString() || powerText.color != fleetFaction.faction.factionColor || factionIcon.sprite != fleetFaction.faction.icon)
        {
            powerText.text = string.Format("{0}", fleet.fleet.Count.ToString());
            //powerText.color = fleetFaction.faction.factionColor;
            factionIcon.sprite = fleetFaction.faction.icon;
        }

        UpdateColor();

        //Debug.Log("Updating Text on Commanders");

        yield return new WaitForSeconds(2f);
    }

    public void UpdateColor()
    {
        var faction = Player.playerInstance.GetComponent<FleetFaction>().faction;

        for (int i = 0; i < faction.enemies.Count; i++)
        {
            if (fleetFaction.faction == Player.playerInstance.GetComponent<FleetFaction>().faction.enemies[i])
            {
                powerText.color = enemyColor;
                return;
            }
        }

        for (int i = 0; i < faction.allies.Count; i++)
        {
            if (fleetFaction.faction == Player.playerInstance.GetComponent<FleetFaction>().faction.allies[i])
            {
                powerText.color = allyColor;
                return;
            }
        }

        if (faction.playerInFaction)
        {
            powerText.color = allyColor;
            return;
        }

        powerText.color = Color.white;
    }
}
