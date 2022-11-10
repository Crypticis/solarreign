using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PilotPartyDropSlot : MonoBehaviour, IDropHandler
{
    public PartyOverview partyOverview;
    public PartyUI partyUI;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<PilotPartySlot>().overSlot = true;
            partyUI.SetPilotToShip(eventData.pointerDrag.GetComponent<PilotPartySlot>().pilot);
            //this.GetComponent<Image>().sprite = eventData.pointerDrag.transform.Find("Icon").GetComponent<Image>().sprite;
        }
    }
}
