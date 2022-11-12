using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTraderShip : MonoBehaviour
{
    public List<ItemSlot> tradeGoods = new List<ItemSlot>();

    public CivilianCommanderAI ai;

    public void Awake()
    {
        ai = GetComponent<CivilianCommanderAI>();
    }

    public void AddItems()
    {
        //ai = GetComponent<CivilianCommanderAI>();

        if (ai.originSettlement)
        {
            for (int i = 0; i < ai.originSettlement.GetComponent<SettlementTrader>().inventory.itemSlots.Count; i++)
            {
                var item = new ItemSlot
                {
                    item = ai.originSettlement.GetComponent<SettlementTrader>().inventory.itemSlots[i].item,
                };

                var randomAmount = Random.Range(1, 10);

                if (ai.originSettlement.GetComponent<SettlementTrader>().inventory.itemSlots[i].amount >= randomAmount)
                {
                    item.amount = randomAmount;
                    ai.originSettlement.GetComponent<SettlementTrader>().inventory.itemSlots[i].amount -= randomAmount;
                }
                else
                {
                    item.amount = ai.originSettlement.GetComponent<SettlementTrader>().inventory.itemSlots[i].amount;
                    ai.originSettlement.GetComponent<SettlementTrader>().inventory.itemSlots[i].amount = 0;
                }

                tradeGoods.Add(item);
            }
        }
        else
        {
            for (int i = 0; i < GameManager.instance.database.items.Length; i++)
            {
                var item = new ItemSlot();

                item.item = GameManager.instance.database.items[i];

                var randomAmount = Random.Range(0, 10);

                item.amount = randomAmount;

                tradeGoods.Add(item);
            }
        }  
    }
}
