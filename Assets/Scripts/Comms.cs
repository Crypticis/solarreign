using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Comms : MonoBehaviour
{
    List<GameObject> potentialComms = new List<GameObject>();
    List<GameObject> commsSlots = new List<GameObject>();

    int currentLength;
    int oldLength = 0;

    public GameObject commsSlot;
    public Transform commsUI;
    public GameObject activeComm;
    public TMP_Text nameText;

    public Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }

    private void Update()
    {
        currentLength = potentialComms.Count;
        if (oldLength != currentLength)
        {
            oldLength = currentLength;
            GameObject[] targets = potentialComms.ToArray();
            foreach (GameObject target in targets)
            {
                //if (target && target.GetComponent<HUDElements>() != null && !target.GetComponent<HUDElements>().inOverlay)
                //{
                //    GameObject go = Instantiate(commsSlot, commsUI);
                //    go.transform.Find("Icon").GetComponentInChildren<Image>().sprite = target.GetComponent<HUDElements>().icon;
                //    go.transform.Find("Name").GetComponentInChildren<TMP_Text>().text = target.GetComponent<HUDElements>().name;
                //    go.transform.GetComponentInChildren<Button>().onClick.AddListener(() => { SetActiveComm(go); });
                //    target.GetComponent<HUDElements>().inOverlay = true;
                //    go.GetComponent<CommsSlot>().commsTarget = target;
                //    commsSlots.Add(go);
                //}
            }
        }
    }

    public void SetActiveComm(GameObject slot)
    {
        slot.GetComponent<CommsSlot>().commsTarget.GetComponent<DialogueActivator>().Interact(player);
        nameText.text = slot.GetComponent<CommsSlot>().commsTarget.GetComponent<HUDElements>().name;
        activeComm = slot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ally")
        {
            if (!potentialComms.Contains(other.gameObject))
            {
                potentialComms.Add(other.gameObject);
            }
        }

        //for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        //{
        //    if (FactionManager.instance.factions[i].playerInFaction)
        //    {
        //        if(other.tag == FactionManager.instance.factions[i].tag)
        //        {
        //            if (!potentialComms.Contains(other.gameObject))
        //            {
        //                potentialComms.Add(other.gameObject);
        //            }
        //        }
        //    }
        //}
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ally")
        {
            potentialComms.Remove(other.gameObject);

            for (int j = 0; j < commsSlots.Count; j++)
            {
                if (commsSlots[j].GetComponent<CommsSlot>().commsTarget == other.gameObject)
                {
                    Destroy(commsSlots[j]);
                    commsSlots.Remove(commsSlots[j]);
                }
            }
        }

        //for (int i = 0; i < FactionManager.instance.factions.Length; i++)
        //{
        //    if (FactionManager.instance.factions[i].playerInFaction)
        //    {
        //        if (other.tag == FactionManager.instance.factions[i].tag)
        //        {
        //            potentialComms.Remove(other.gameObject);

        //            for (int j = 0; j < commsSlots.Count; j++)
        //            {
        //                if (commsSlots[j].GetComponent<CommsSlot>().commsTarget == other.gameObject)
        //                {
        //                    Destroy(commsSlots[j]);
        //                    commsSlots.Remove(commsSlots[j]);
        //                }
        //            }
        //        }
        //    }
        //}


        //if (other.tag == "Ally")
        //{
        //    potentialComms.Remove(other.gameObject);

        //    for (int i = 0; i < commsSlots.Count; i++)
        //    {
        //        if (commsSlots[i].GetComponent<CommsSlot>().commsTarget == other.gameObject)
        //        {
        //            Destroy(commsSlots[i]);
        //            commsSlots.Remove(commsSlots[i]);
        //        }
        //    }
        //}
    }
}
