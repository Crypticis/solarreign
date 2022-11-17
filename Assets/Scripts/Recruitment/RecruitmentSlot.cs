using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnitySteer.Behaviors;

public class RecruitmentSlot : MonoBehaviour
{
    public GameObject shipToRecruit;
    public Button button;
    public float cost;
    public SpaceStationUI stationUI;
    public TMP_Text nameText;
    public TMP_Text costText;

    public void Update()
    {
        if(shipToRecruit == null)
        {
            Destroy(gameObject);
        }

        if(nameText.text != shipToRecruit.name)
        {
            nameText.text = shipToRecruit.name;
        }

        if (costText.text != cost.ToString())
        {
            costText.text = cost.ToString();
        }
    }

    public void AddToFleet()
    {
        if (!shipToRecruit.GetComponent<FleetFaction>().faction.playerInFaction)
        {
            button.interactable = false;
        } else
        {
            button.interactable = true;
        }

        if(StatManager.instance.currentMoney >= cost && Player.playerInstance.fleet.fleet.Count < Player.playerInstance.fleet.maxInFleet)
        {
            GameObject go = Instantiate(shipToRecruit, Player.playerInstance.transform.position, Quaternion.identity);
            go.GetComponent<SteerToFollow>().Target = Player.playerInstance.transform;
            go.GetComponent<SteerToFollow>().enabled = true;

            Player.playerInstance.fleet.AddToFleet(go);
            StatManager.instance.currentMoney -= cost;
            Ticker.Ticker.AddItem("Recruited " + shipToRecruit.name + " for " + cost.ToString());
            stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips.Remove(shipToRecruit);
            shipToRecruit = null;
            Destroy(gameObject);
        }
        else if(StatManager.instance.currentMoney >= cost && Player.playerInstance.fleet.fleet.Count >= Player.playerInstance.fleet.maxInFleet)
        {
            Ticker.Ticker.AddItem("Unable to recruit. Need more fleet space.");
        }
        else
        {
            Ticker.Ticker.AddItem("Unable to recruit. Need more money");
        }
    }
}
