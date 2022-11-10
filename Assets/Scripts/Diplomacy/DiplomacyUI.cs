using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiplomacyUI : MonoBehaviour
{
    public GameObject buttonPrefab;

    public TMP_Text factionNameText;

    public Transform buttonRoot;

    public Faction selectedFaction;

    public GameObject factionMenu;

    public Faction playerFaction;

    public Button peaceButton;
    public Button warButton;

    void Update()
    {
        if (!selectedFaction)
            factionMenu.SetActive(false);
    }

    public void UpdateDiplomacyUI()
    {
        selectedFaction = null;

        playerFaction = Player.playerInstance.GetComponent<PlayerFaction>().faction;
        factionNameText.text = playerFaction.name;

        for (int i = 0; i < buttonRoot.childCount; i++)
        {
            Destroy(buttonRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        {
            if(FactionManager.instance.factions[i] != playerFaction)
            {
                GameObject button = Instantiate(buttonPrefab, buttonRoot);

                Faction factionTemp = FactionManager.instance.factions[i];

                button.GetComponent<Button>().onClick.AddListener(() => SelectFaction(factionTemp));
                button.GetComponentInChildren<TMP_Text>().text = FactionManager.instance.factions[i].name;
            }
        }
    }

    void SelectFaction(Faction faction)
    {
        if(selectedFaction == faction)
        {
            faction = null;
            return;
        }

        selectedFaction = faction;

        if(selectedFaction != null)
        {
            factionMenu.SetActive(true);

            for (int i = 0; i < playerFaction.enemies.Count; i++)
            {
                if(playerFaction.enemies[i] == selectedFaction)
                {
                    peaceButton.interactable = true;
                    warButton.interactable = false;
                    break;
                }
                else
                {
                    peaceButton.interactable = false;
                    warButton.interactable = true;
                }
            }
        }
    }

    public void DeclareWar()
    {
        if (selectedFaction)
        {
            FactionManager.instance.MakePeace(playerFaction, selectedFaction);
        }
    }

    public void DeclarePeace()
    {
        if (selectedFaction)
        {
            FactionManager.instance.MakeWar(playerFaction, selectedFaction);
        }
    }
}
