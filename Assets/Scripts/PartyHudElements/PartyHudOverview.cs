using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PartyHudOverview : MonoBehaviour
{
    public PlayerFleet fleet;
    public GameObject slotPrefab;
    public Transform partyHudRoot;

    public List<PartyHudSlot> slots = new();

    void Awake()
    {
        for (int i = 0; i < fleet.fleet.Count; i++)
        {
            CreatePartyOverviewSlot(fleet.fleet[i]);
        }

        StartCoroutine("CheckPartyStatus");
    }

    public IEnumerator CheckPartyStatus()
    {
        while (true)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].ship == null)
                {
                    Destroy(slots[i]);
                    continue;
                }

                if (slots[i].ship.ship.TryGetComponent<DamageHandler>(out DamageHandler damageHandler))
                {
                    if (damageHandler.currentShield < damageHandler.maxShield || damageHandler.health < damageHandler.maxHealth)
                    {
                        slots[i].gameObject.SetActive(true);
                    }
                    else
                    {
                        slots[i].gameObject.SetActive(false);
                    }
                }
            }

            RefreshList();

            yield return new WaitForSeconds(5f);
        }
    }

    public void RefreshList()
    {
        for (int i = slots.Count - 1; i >= 0; i--)
        {
            if (slots[i] == null)
            {
                slots[i] = slots[slots.Count - 1];
                slots.RemoveAt(slots.Count - 1);
            }
        }
    }

    public void CreatePartyOverviewSlot(FleetShip ship)
    {
        PartyHudSlot slot = Instantiate(slotPrefab, partyHudRoot).GetComponent<PartyHudSlot>();
        slot.ship = ship;

        slots.Add(slot);
    }
}
