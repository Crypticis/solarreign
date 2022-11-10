using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecruitmentUI : MonoBehaviour
{
    public SpaceStationUI stationUI;
    int oldCount;
    public GameObject slotPrefab;
    public TMP_Text recruitableText;
    public List<GameObject> slots = new List<GameObject>();

    void Update()
    {
        if(stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips.Count != oldCount)
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

        if(recruitableText.text != stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips.Count.ToString() + " purchasable ships.")
        {
            recruitableText.text = stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips.Count.ToString() + " purchasable ships.";
        }
    }

    public void UpdateUI()
    {
        oldCount = stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips.Count;

        for (int i = 0; i < slots.Count; i++)
        {
            Destroy(slots[i]);
        }

        for (int i = 0; i < stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips.Count; i++)
        {
            GameObject slot = Instantiate(slotPrefab, this.transform);
            slot.GetComponent<RecruitmentSlot>().shipToRecruit = stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips[i];
            slot.GetComponent<RecruitmentSlot>().cost = stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips[i].GetComponent<HUDElements>().recruitmentCost;
            slot.GetComponent<RecruitmentSlot>().stationUI = stationUI;
            //slot.GetComponent<TooltipTrigger>().content = "Cost: " + slot.GetComponent<RecruitmentSlot>().cost;
            //slot.GetComponent<TooltipTrigger>().header = slot.GetComponent<RecruitmentSlot>().shipToRecruit.name;
            slot.transform.Find("Icon").GetComponent<Image>().sprite = stationUI.spaceStation.GetComponent<SettlementRecruitment>().purchasableShips[i].GetComponent<HUDElements>().iconModel;

            slots.Add(slot);
        }
    }
}
