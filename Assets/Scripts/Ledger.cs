using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class Ledger : MonoBehaviour
{

    public GameObject[] settlements;
    public Transform ledgerUI;
    public GameObject slotPrefab;
    public Scrollbar scrollBar;

    public List<GameObject> slots = new();

    float timer = 0f;
    public int lastChanged = -1;

    public GameObject travelBox;

    void Start()
    {
        settlements = GameObject.FindGameObjectsWithTag("Settlement");

        for (int i = 0; i < settlements.Length; i++)
        {
            var slot = Instantiate(slotPrefab, ledgerUI);
            slot.GetComponent<LedgerSlot>().trader = settlements[i].GetComponent<Shop>();
            slot.GetComponent<LedgerSlot>().settlement = settlements[i];
            slot.GetComponent<LedgerSlot>().ledger = this;

            slots.Add(slot);
        }
    }

    public void Update()
    {
        if(timer <= 0)
        {
            timer = 0;
        }

        timer -= Time.unscaledDeltaTime;
    }

    public void SortBy(int index)
    {
        AudioManager.instance.Play("Click2");
        scrollBar.value = 0;

        if(timer <= 0 || index != lastChanged)
        {
            slots = slots.OrderBy(item => item.GetComponent<LedgerSlot>().trader.Supply.itemSlots[index].item.currentPrice).ToList();
            slots.Reverse();
            lastChanged = index;
            timer = 1f;
        }
        else if (index == lastChanged)
        {
            slots = slots.OrderBy(item => item.GetComponent<LedgerSlot>().trader.Supply.itemSlots[index].item.currentPrice).ToList();
            lastChanged = index;
        }


        for (int i = 0; i < slots.Count; i++)
        {
            slots[i].transform.SetSiblingIndex(i);
        }
    }
    public void OpenTravelBox(GameObject settlement)
    {
        AudioManager.instance.Play("Click2");
        travelBox.SetActive(true);
        travelBox.transform.Find("Yes").GetComponent<Button>().onClick.AddListener(() => SetTarget(settlement));
        travelBox.transform.Find("Settlement Name").GetComponent<TMP_Text>().text = settlement.GetComponent<SettlementInfo>().Name;
    }

    public void CloseTravelBox()
    {
        AudioManager.instance.Play("Click2");
        travelBox.SetActive(false);
        travelBox.transform.Find("Yes").GetComponent<Button>().onClick.RemoveAllListeners();
    }

    public void SetTarget(GameObject settlement)
    {
        AudioManager.instance.Play("Click2");
        Player.playerInstance.GetComponent<ClickToMove>().travelTarget = settlement.transform;
        CloseTravelBox();
        GameManager.instance.Ledger();
    }

    //public float CalculateTravelTime(GameObject settlement)
    //{
    //    var d = Vector3.Distance(Player.playerInstance.transform.position, settlement.transform.position);
    //    var speed = Player.playerInstance.GetComponent<NavMeshAgent>().speed;

    //    var t = (d / speed);

    //    return (t / GameManager.instance.speedMultiplier);  
    //}
}
