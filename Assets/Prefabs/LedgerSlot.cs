using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class LedgerSlot : MonoBehaviour
{
    public Shop trader;

    public Ledger ledger;

    public TMP_Text nameText;
    public TMP_Text[] priceText;
    public TMP_Text factionText;

    public GameObject settlement;

    IEnumerator coroutine;

    public void Start()
    {
        coroutine = UpdatePrices();
        StartCoroutine(coroutine);

        nameText.text = trader.GetComponent<SettlementInfo>().Name;
    }

    public IEnumerator UpdatePrices()
    {
        for (int i = 0; i < priceText.Length; i++)
        {
            priceText[i].text = "$" + trader.Supply.itemSlots[i].item.currentPrice.ToString("0.0");
        }

        factionText.text = settlement.GetComponent<SettlementInfo>().faction.name;

        if (FactionManager.instance.CheckIfEnemy(Player.playerInstance.GetComponent<PlayerFaction>().faction, settlement.GetComponent<SettlementInfo>().faction))
        {
            factionText.color = new Color32(23, 192, 235, 255);
        } 
        else if (FactionManager.instance.CheckIfAlly(Player.playerInstance.GetComponent<PlayerFaction>().faction, settlement.GetComponent<SettlementInfo>().faction))
        {
            factionText.color = new Color32(255, 56, 56, 255);
        }
        else
        {
            factionText.color = Color.white;
        }

        yield return new WaitForSeconds(600f);
    }

    public void Travel()
    {
        ledger.OpenTravelBox(settlement);
    }
}
