using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyUI : MonoBehaviour
{
    public Canvas canvas;

    public Transform shipRoot;
    public Transform pilotRoot;

    public TMP_Text healthText, attackText, nameText, skillpointsText, pilotNameText;
    public Button disbandButton;

    public GameObject activePartySlot;
    public GameObject shipSlotPrefab;
    public GameObject partyOverview;

    public List<GameObject> shipSlots = new List<GameObject>();


    //
    public GameObject pilotSlotPrefab;
    public List<GameObject> pilotSlots = new List<GameObject>();
    public GameObject dronePilot;
    //

    public GameObject grayOutSkills; 

    public GameObject shipModel;
    public Transform pilotModelRoot;

    public TMP_Text amountText;

    public TMP_Text speed, firespeed, durability, damage, range;
    public Button[] buttons;

    public GameObject partyRenderCam;
    public GameObject pilotRenderCam;

    public void Start()
    {
        UpdatePartyUI();
        GameManager.instance.database.UpdateID();
    }

    private void OnDisable()
    {
        partyRenderCam.SetActive(false);
        pilotRenderCam.SetActive(false);
    }

    private void OnEnable()
    {
        partyRenderCam.SetActive(true);
        pilotRenderCam.SetActive(true);
    }

    public void SetActiveSlot(GameObject slot)
    {
        if (activePartySlot == slot)
        {
            activePartySlot = null;
            partyOverview.GetComponent<PartyOverview>().fleetShip = null;
            partyOverview.SetActive(false);
        }
        else
        {
            activePartySlot = slot;
            partyOverview.GetComponent<PartyOverview>().fleetShip = slot.GetComponent<PartySlot>().fleetShip;
            UpdateActivePartySlot();
            partyOverview.SetActive(true);
        }
    }

    public void UpdateActivePartySlot()
    {
        UpdateSkills();
        UpdateButtons();

        nameText.text = activePartySlot.GetComponent<PartySlot>().fleetShip.ship.GetComponent<HUDElements>().name;
        healthText.text = activePartySlot.GetComponent<PartySlot>().fleetShip.ship.GetComponent<DamageHandler>().health + " / " + activePartySlot.GetComponent<PartySlot>().fleetShip.ship.GetComponent<DamageHandler>().maxHealth.ToString();
        //attackText.text = activePartySlot.GetComponent<PartySlot>().fleetShip.attack.ToString();
        if(activePartySlot.GetComponent<PartySlot>().fleetShip.hasPilot)
        {
            skillpointsText.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints.ToString();
            pilotNameText.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.name;
            grayOutSkills.SetActive(false);
            if (pilotModelRoot.childCount > 0)
                Destroy(pilotModelRoot.GetChild(0).gameObject);
            Instantiate(GameManager.instance.database.GetPilotModel[activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.modelID], pilotModelRoot);
        }
        else
        {
            pilotNameText.text = "Autonomous Drone";
            grayOutSkills.SetActive(true);
            if(pilotModelRoot.childCount > 0)
                Destroy(pilotModelRoot.GetChild(0).gameObject);
            Instantiate(dronePilot, pilotModelRoot);
        }

        disbandButton.onClick.RemoveAllListeners();
        disbandButton.onClick.AddListener(() => { AddBackToUnusedPilots(); Player.playerInstance.fleet.RemoveFromFleet(partyOverview.GetComponent<PartyOverview>().fleetShip); SetActiveSlot(activePartySlot); UpdatePartyUI(); });

        shipModel.GetComponent<MeshFilter>().sharedMesh = partyOverview.GetComponent<PartyOverview>().fleetShip.ship.GetComponent<MeshFilter>().sharedMesh;
        shipModel.GetComponent<Renderer>().sharedMaterial = partyOverview.GetComponent<PartyOverview>().fleetShip.ship.GetComponent<Renderer>().sharedMaterial;

        List<float> size = new List<float>();

        size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.z * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.z));
        size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.x * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.x));
        size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.y * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.y));

        size = size.OrderBy(i => i).ToList();

        float objectSize = size[0] / 2f;

        shipModel.GetComponent<MeshFilter>().transform.localScale /= objectSize;
    }

    public void AddBackToUnusedPilots()
    {
        if (partyOverview.GetComponent<PartyOverview>().fleetShip.hasPilot)
        {
            Player.playerInstance.fleet.unusedPilots.Add(partyOverview.GetComponent<PartyOverview>().fleetShip.pilot);
        }
    }

    public void UpdatePartyUI()
    {
        //Ships

        for (int i = 0; i < shipSlots.Count; i++)
        {
            Destroy(shipSlots[i].gameObject);
        }

        shipSlots.Clear();

        foreach (FleetShip ship in Player.playerInstance.fleet.fleet)
        {
            GameObject go = Instantiate(shipSlotPrefab, shipRoot);
            go.transform.Find("ActiveButton").GetComponentInChildren<Button>().onClick.AddListener(() => { SetActiveSlot(go); });
            go.GetComponentInChildren<TMP_Text>().text = ship.ship.name;
            go.transform.Find("Icon").GetComponentInChildren<Image>().sprite = ship.ship.GetComponent<HUDElements>().icon;
            go.GetComponent<PartySlot>().fleetShip = ship;
            shipSlots.Add(go);
        }

        amountText.text = Player.playerInstance.fleet.fleet.Count.ToString() + " / " + Player.playerInstance.fleet.maxInFleet;


        //Pilots

        for (int i = 0; i < pilotSlots.Count; i++)
        {
            Destroy(pilotSlots[i].gameObject);
        }

        pilotSlots.Clear();

        foreach (Pilot pilot in Player.playerInstance.fleet.unusedPilots)
        {
            GameObject go = Instantiate(pilotSlotPrefab, pilotRoot);
            go.GetComponent<PilotPartySlot>().canvas = canvas;
            //go.transform.Find("ActiveButton").GetComponentInChildren<Button>().onClick.AddListener(() => { SetActiveSlot(go); });
            go.GetComponentInChildren<TMP_Text>().text = pilot.name;
            //go.transform.Find("Icon").GetComponentInChildren<Image>().sprite = ship.prefab.GetComponent<HUDElements>().icon;
            go.GetComponent<PilotPartySlot>().pilot = pilot;
            pilotSlots.Add(go);
        }
    }

    public void SetPilotToShip(Pilot pilot)
    {
        if(partyOverview.GetComponent<PartyOverview>().fleetShip.hasPilot)
        {
            Player.playerInstance.fleet.unusedPilots.Add(partyOverview.GetComponent<PartyOverview>().fleetShip.pilot);
        }

        partyOverview.GetComponent<PartyOverview>().fleetShip.hasPilot = true;

        partyOverview.GetComponent<PartyOverview>().fleetShip.pilot = pilot;
        Player.playerInstance.fleet.unusedPilots.Remove(pilot);

        UpdateActivePartySlot();

        //

        for (int i = 0; i < pilotSlots.Count; i++)
        {
            Destroy(pilotSlots[i].gameObject);
        }

        pilotSlots.Clear();

        foreach (Pilot _pilot in Player.playerInstance.fleet.unusedPilots)
        {
            GameObject go = Instantiate(pilotSlotPrefab, pilotRoot);
            go.GetComponent<PilotPartySlot>().canvas = canvas;
            //go.transform.Find("ActiveButton").GetComponentInChildren<Button>().onClick.AddListener(() => { SetActiveSlot(go); });
            go.GetComponentInChildren<TMP_Text>().text = _pilot.name;
            //go.transform.Find("Icon").GetComponentInChildren<Image>().sprite = ship.prefab.GetComponent<HUDElements>().icon;
            go.GetComponent<PilotPartySlot>().pilot = _pilot;
            pilotSlots.Add(go);
        }
    }

    //Skills

    public void UpdateButtons()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if(activePartySlot.GetComponent<PartySlot>().fleetShip.hasPilot && activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints > 0)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void UpdateSkills()
    {
        if (activePartySlot.GetComponent<PartySlot>().fleetShip.hasPilot == false)
        {
            skillpointsText.text = "0";
            speed.text = "0";
            firespeed.text = "0";
            durability.text = "0";
            damage.text = "0";
            range.text = "0";

            return;
        }

        skillpointsText.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints.ToString();
        speed.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.speedSkill.ToString();
        firespeed.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.firespeedSkill.ToString();
        durability.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.durabilitySkill.ToString();
        damage.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.damageSkill.ToString();
        range.text = activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.rangeSkill.ToString();
    }

    public void IncreaseSpeed()
    {
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints--;
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.speedSkill++;

        UpdateSkills();
        UpdateButtons();
    }

    public void IncreaseFirespeed()
    {
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints--;
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.firespeedSkill++;

        UpdateSkills();
        UpdateButtons();
    }

    public void IncreaseDurability()
    {
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints--;
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.durabilitySkill++;

        UpdateSkills();
        UpdateButtons();
    }

    public void IncreaseDamage()
    {
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints--;
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.damageSkill++;

        UpdateSkills();
        UpdateButtons();
    }
    public void IncreaseRange()
    {
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.skillpoints--;
        activePartySlot.GetComponent<PartySlot>().fleetShip.pilot.rangeSkill++;

        UpdateSkills();
        UpdateButtons();
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
