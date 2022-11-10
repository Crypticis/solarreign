using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnitySteer.Behaviors;

public class FleetManager : MonoBehaviour
{
    public List<GameObject> fleet = new List<GameObject>();
    public List<GameObject> fleetSlots = new List<GameObject>();

    public Player player;
    public GameObject fleetSlot;
    public Transform fleetUI;

    public GameObject activeComm;
    public TMP_Text nameText;

    int currentLength;
    int oldLength = 0;

    void Update()
    {
        for (int i = 0; i < fleet.Count; i++)
        {
            if(fleet[i] != null)
            {
                fleet[i].GetComponent<SteerForTether>().TetherPosition = player.transform.position;
            } 
            else
            {
                fleet[i] = fleet[fleet.Count - 1];
                fleet.RemoveAt(fleet.Count - 1);
            }
        }

        if (oldLength != currentLength)
        {
            oldLength = currentLength;
            GameObject[] targets = fleet.ToArray();
            foreach (GameObject target in targets)
            {
                if (target && target.GetComponent<HUDElements>() != null && !target.GetComponent<HUDElements>().inFleet)
                {
                    GameObject go = Instantiate(fleetSlot, fleetUI);
                    go.transform.Find("Icon").GetComponentInChildren<Image>().sprite = target.GetComponent<HUDElements>().icon;
                    go.transform.Find("Name").GetComponentInChildren<TMP_Text>().text = target.GetComponent<HUDElements>().name;
                    go.transform.GetComponentInChildren<Button>().onClick.AddListener(() => { SetActiveComm(go); });
                    target.GetComponent<HUDElements>().inFleet = true;
                    go.GetComponent<FleetSlot>().commsTarget = target;
                    fleetSlots.Add(go);
                }
            }
        }
    }

    public void AddToFleet(GameObject ship)
    {
        if(StatManager.instance.playerStatsObject.currentInFleet < StatManager.instance.playerStatsObject.maxInFleet)
        {
            fleet.Add(ship);
            //ship.GetComponent<SteerForTether>().MaximumDistance = 5f;
            //ship.GetComponent<SteerForTether>().Weight = 1000f;

            ship.GetComponent<SteerForTether>().enabled = false;
            ship.GetComponent<SteerForWander>().enabled = false;

            //ship.GetComponent<SteerForTether>().enabled = false;
            ship.GetComponent<SteerToFollow>().enabled = true;
            ship.GetComponent<SteerToFollow>().Target = player.transform;

            currentLength++;
            StatManager.instance.playerStatsObject.currentInFleet++;
        }
    }

    public void RemoveFromFleet(GameObject ship)
    {
        fleet.Remove(ship);
        //ship.GetComponent<SteerForTether>().enabled = false;
        ship.GetComponent<SteerForWander>().enabled = true;
        //ship.GetComponent<SteerForTether>().enabled = true;
        ship.GetComponent<SteerToFollow>().enabled = false;

        currentLength--;
        StatManager.instance.playerStatsObject.currentInFleet--;
        for (int x = 0; x < fleetSlots.Count; x++)
        {
            if (fleetSlots[x].GetComponent<CommsSlot>().commsTarget == ship)
            {
                Destroy(fleetSlots[x]);
                fleetSlots.Remove(fleetSlots[x]);
            }
        }
    }

    public void SetActiveComm(GameObject slot)
    {
        slot.GetComponent<CommsSlot>().commsTarget.GetComponent<DialogueActivator>().Interact(player);
        nameText.text = slot.GetComponent<CommsSlot>().commsTarget.GetComponent<HUDElements>().name;
        activeComm = slot;
    }
}
