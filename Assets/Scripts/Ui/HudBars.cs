using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudBars : MonoBehaviour
{
    public DamageHandler damageHandler;
    public Player player;

    public Slider[] bars;

    void Update()
    {
        //Energy bar
        bars[0].value = player.energy / player.maxEnergy;
        //Shield bar
        bars[1].value = damageHandler.currentShield / damageHandler.maxShield;
        //Health bar
        bars[2].value = damageHandler.health / damageHandler.maxHealth;
    }
}
