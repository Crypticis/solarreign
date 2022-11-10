using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettlementLabel : MonoBehaviour
{
    public TMP_Text nameText;
    public SettlementInfo settlementInfo;
    public SpriteRenderer factionIcon;

    Color enemyColor;
    Color allyColor;

    public void Start()
    {
        enemyColor = new Color(0.8392157f, 0.1882353f, 0.1921569f);
        allyColor = new Color(0f, 0.5176471f, 0.8901961f);
    }

    void Update()
    {
        if(nameText.text != settlementInfo.Name || nameText.color != settlementInfo.faction.factionColor || factionIcon.sprite != settlementInfo.faction.icon)
        {
            nameText.text = settlementInfo.Name;
            //nameText.color = settlementInfo.faction.factionColor;
            factionIcon.sprite = settlementInfo.faction.icon;
        }

        UpdateColor();
    }

    public void UpdateColor()
    {
        var faction = Player.playerInstance.GetComponent<FleetFaction>().faction;

        for (int i = 0; i < faction.enemies.Count; i++)
        {
            if (settlementInfo.faction == Player.playerInstance.GetComponent<FleetFaction>().faction.enemies[i])
            {
                nameText.color = enemyColor;
                return;
            }
        }

        for (int i = 0; i < faction.allies.Count; i++)
        {
            if (settlementInfo.faction == Player.playerInstance.GetComponent<FleetFaction>().faction.allies[i])
            {
                nameText.color = allyColor;
                return;
            }
        }

        if(settlementInfo.faction == Player.playerInstance.GetComponent<FleetFaction>().faction)
        {
            nameText.color = allyColor;
            return;
        }

        nameText.color = Color.white;
    }
}
