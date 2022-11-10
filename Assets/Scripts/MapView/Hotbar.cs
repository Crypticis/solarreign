using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hotbar : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();
    public GameObject diplomacyUI;
    public GameObject diplomacyButton;

    public GameObject[] menus;

    int lastOpened = 0;

    public void OnEnable()
    {
        OpenUI(lastOpened);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            menus[2].GetComponent<InventoryUI>().UpdateSlots();
        }

        if (Player.playerInstance.GetComponent<PlayerFaction>().faction.isPlayerLeader)
        {
            diplomacyButton.SetActive(true);
        }
        else
        {
            diplomacyButton.SetActive(false);
        }
    }

    public void OpenUI(int index)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            if (i != index)
                menus[i].SetActive(false);
            else
            {
                menus[i].SetActive(true);
                lastOpened = i;
            }
        }
    }

    public void PauseButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(0);
    }

    public void SkillButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(1);
    }

    public void InventoryButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(2);
        menus[2].GetComponent<InventoryUI>().UpdateSlots();
    }

    public void FactionsButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(3);
    }
    public void LedgerButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(4);
    }

    public void FleetButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(5);
    }

    public void MissionButton()
    {
        AudioManager.instance.Play("Click");
        OpenUI(6);
    }

    public void DiplomacyButton()
    {
        AudioManager.instance.Play("Click");

        diplomacyUI.GetComponent<DiplomacyUI>().UpdateDiplomacyUI();

        if(!diplomacyUI.activeSelf)
            diplomacyUI.SetActive(true);
        else
            diplomacyUI.SetActive(false);
    }
}
