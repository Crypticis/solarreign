using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FactionUI : MonoBehaviour
{
    public Transform factionUI;

    public GameObject activeFactionSlot;

    public GameObject factionSlotPrefab;

    public GameObject factionOverview;

    public Faction neutralFaction;

    public Button leaveFaction;
    public Button joinFaction;

    void Start()
    {
        foreach (Faction faction in FactionManager.instance.factions)
        {

            if (faction.name == "Neutral" || faction.isPlayerFaction == true)
                continue;

            GameObject go = Instantiate(factionSlotPrefab, factionUI);
            go.transform.Find("ActiveButton").GetComponentInChildren<Button>().onClick.AddListener(() => { SetActiveSlot(go); });
            go.GetComponentInChildren<TMP_Text>().text = faction.name;
            go.transform.Find("Icon").GetComponentInChildren<Image>().sprite = faction.icon;
            go.GetComponent<FactionSlot>().faction = faction;
        }
    }

    public void LeaveFaction()
    {
        for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        {
            FactionManager.instance.factions[i].playerInFaction = false;
        }

        neutralFaction.playerInFaction = true;

        Player.playerInstance.GetComponent<PlayerFaction>().UpdateFaction();

        Ticker.Ticker.AddItem("You have left your faction.");
        //GameManager.instance.FactionColorUpdate();
    }

    public void SetActiveSlot(GameObject slot)
    {
        if(activeFactionSlot == slot)
        {
            activeFactionSlot = null;
            factionOverview.SetActive(false);
        }
        else
        {
            activeFactionSlot = slot;
            factionOverview.SetActive(true);
            factionOverview.GetComponent<FactionOverview>().faction = activeFactionSlot.GetComponent<FactionSlot>().faction;
            factionOverview.GetComponent<FactionOverview>().UpdateOverview();
        }
    }

    public void Update()
    {
        if (neutralFaction.playerInFaction == true)
        {
            leaveFaction.interactable = false;
        }
        else
        {
            leaveFaction.interactable = true;
        }
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

}
