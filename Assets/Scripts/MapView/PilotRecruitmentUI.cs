using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PilotRecruitmentUI : MonoBehaviour
{
    public SpaceStationUI stationUI;
    int oldCount;
    public GameObject slotPrefab;
    public TMP_Text recruitableText;
    public List<GameObject> slots = new List<GameObject>();

    void Update()
    {
        if (stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots.Count != oldCount)
        {
            //UpdateUI();
        }

        if (!stationUI.spaceStation)
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        if (recruitableText.text != stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots.Count.ToString() + " recruitable pilots.")
        {
            recruitableText.text = stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots.Count.ToString() + " recruitable pilots.";
        }
    }

    public void UpdateUI()
    {
        oldCount = stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots.Count;

        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i]);
        }

        for (int i = 0; i < stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, this.transform);
            slot.GetComponent<PilotRecruitmentSlot>().pilotToRecruit = stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots[i];
            slot.GetComponent<PilotRecruitmentSlot>().cost = stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots[i].costToHire;
            slot.GetComponent<PilotRecruitmentSlot>().stationUI = stationUI;
            //slot.GetComponent<TooltipTrigger>().content = "Cost: " + slot.GetComponent<RecruitmentSlot>().cost;
            //slot.GetComponent<TooltipTrigger>().header = slot.GetComponent<RecruitmentSlot>().shipToRecruit.name;
            //slot.transform.Find("Icon").GetComponent<Image>().sprite = stationUI.spaceStation.GetComponent<SettlementRecruitment>().recruitablePilots[i].GetComponent<HUDElements>().iconModel;

            slots.Add(slot);
        }
    }
}
