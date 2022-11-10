using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyHudSlot : MonoBehaviour
{
    public FleetShip ship;
    public TMP_Text shipName;
    public Slider healthBar;
    public Slider shieldBar;

    public void Start()
    {
        shipName.text = ship.ship.GetComponent<HUDElements>().name;
    }

    void Update()
    {
        if (ship.ship == null)
            Destroy(this);

        healthBar.value = ship.ship.GetComponent<DamageHandler>().health / ship.ship.GetComponent<DamageHandler>().maxHealth;
        shieldBar.value = ship.ship.GetComponent<DamageHandler>().currentShield / ship.ship.GetComponent<DamageHandler>().maxShield;
    }
}
