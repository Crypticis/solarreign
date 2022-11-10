using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionOverview : MonoBehaviour
{
    public Faction faction;
    public FactionUI factionUI;
    public Button buttonJoin;

    public TMP_Text[] overviewText;

    public void UpdateOverview()
    {
        var settlements = GameObject.FindGameObjectsWithTag("Settlement");
        List<string> ownedSettlements = new List<string>();

        for (int i = 0; i < settlements.Length; i++)
        {
            if (settlements[i].GetComponent<SettlementInfo>().faction == faction)
            {
                ownedSettlements.Add(settlements[i].GetComponent<SettlementInfo>().Name);
            }
        }

        overviewText[0].text = faction.name;
        overviewText[1].text = faction.description;
        overviewText[2].text = ListToText(faction.enemies);
        overviewText[3].text = ListToText(faction.allies);
        overviewText[4].text = ListStringsToText(ownedSettlements);

        if (faction.name == "Pirates")
        {
            buttonJoin.interactable = false;
        }
        else
        {
            buttonJoin.interactable = true;
        }
    }

    public void JoinFaction()
    {
        factionUI.LeaveFaction();

        faction.playerInFaction = true;

        Player.playerInstance.GetComponent<PlayerFaction>().UpdateFaction();

        Ticker.Ticker.AddItem("You have joined " + faction.name + " faction.");

        //GameManager.instance.FactionColorUpdate();
    }

    private string ListToText(List<Faction> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += listMember.name + "\n";
        }
        return result;
    }

    private string ListStringsToText(List<string> list)
    {
        string result = "";
        foreach (var listMember in list)
        {
            result += listMember + "\n";
        }
        return result;
    }
}
