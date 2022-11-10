using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnitySteer.Behaviors;

public class UniqueNPC : MonoBehaviour
{
    public int ID;
    public NPC npc;
    //public TMP_Text nameText;
    FleetFaction fleetFaction;
    Color enemyColor;
    Color allyColor;

    public void Start()
    {
        //gameObject.name = npc.Name;
        fleetFaction = GetComponent<FleetFaction>();
        enemyColor = new Color(0.8392157f, 0.1882353f, 0.1921569f);
        allyColor = new Color(0f, 0.5176471f, 0.8901961f);

        //nameText.text = npc.Name;

        //UpdateColor();
    }

    //public void UpdateColor()
    //{
    //    var faction = Player.playerInstance.GetComponent<FleetFaction>().faction;

    //    for (int i = 0; i < faction.enemies.Count; i++)
    //    {
    //        if (fleetFaction.faction == Player.playerInstance.GetComponent<FleetFaction>().faction.enemies[i])
    //        {
    //            nameText.color = enemyColor;
    //            return;
    //        }
    //    }

    //    for (int i = 0; i < faction.allies.Count; i++)
    //    {
    //        if (fleetFaction.faction == Player.playerInstance.GetComponent<FleetFaction>().faction.allies[i] || fleetFaction.faction.playerInFaction)
    //        {
    //            nameText.color = allyColor;
    //            return;
    //        }
    //    }

    //    nameText.color = Color.white;
    //}
}
