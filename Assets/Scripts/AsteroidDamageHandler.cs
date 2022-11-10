using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamageHandler : DamageHandler
{
    public Item itemReward;
    public Inventory inventory;
    public int minDropAmount;
    public int maxDropAmount;

    void Start()
    {
        health = maxHealth;
        currentShield = maxShield;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void TakeDamage(float damageAmt, GameObject shooter, GameObject projectile)
    {
        health = Mathf.Clamp(health, 0f, maxHealth);

        if (shooter != null && shooter == player)
        {
            shooter.GetComponent<HitmarkerManager>().Hit();

            var wepType = projectile.GetComponent<WeaponInfo>();

            if (wepType.weaponType == WeaponType.missile)
            {
                var damageTotal = damageAmt * 5f + (StatManager.instance.playerStatsObject.Missile.currentLevel * .1f);
                health -= damageTotal;

                Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageTotal).ToString("0"), "Asteroid")), 5f, Color.white);

                StatManager.instance.playerStatsObject.Missile.AddExp(5);
            }
            else if (wepType.weaponType == WeaponType.energy)
            {
                amount++;

                var damageTotal = damageAmt * .5f + (StatManager.instance.playerStatsObject.Projectile.currentLevel * .1f);
                health -= damageTotal;

                if (amount > 50)
                {
                    Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageTotal * amount).ToString("0"), "Asteroid")), 5f, Color.white);
                    amount = 0;
                    StatManager.instance.playerStatsObject.Energy.AddExp(2);
                }
            }
            else if (wepType.weaponType == WeaponType.laser)
            {
                amount++;

                health -= (damageAmt * 5f) + (StatManager.instance.playerStatsObject.Energy.currentLevel * .1f);

                if (amount >= 500)
                {
                    Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageAmt * amount * 2f).ToString("0"), (this.name).ToString())), 5f, Color.white);
                    amount = 0;
                    StatManager.instance.playerStatsObject.Energy.AddExp(1);
                }
            }
        }
        else
        {
            health -= damageAmt;
        }

        if (isPlayer)
        {
            ScreenShake.instance.TriggerShake(0.2f, 0.4f);
        }

        if (health <= 0 && shooter == player)
        {
            if (deathParticle != null)
                Instantiate(deathParticle, this.transform.position, Quaternion.identity);

            var temp = Random.Range(minDropAmount, maxDropAmount);
            inventory.AddItem(itemReward, temp);

            Ticker.Ticker.AddItem("Added " + temp + " of " + itemReward + " to inventory.");
            Ticker.Ticker.AddItem("You now have " + inventory.CheckAmountInInventory(itemReward) + " of " + itemReward + ".");

            if (itemReward == GameManager.instance.gem)
            {
                GameManager.instance.gemsToAdd += temp;
            }

            if (itemReward == GameManager.instance.ore)
            {
                GameManager.instance.oreToAdd += temp;
            }

            Destroy(gameObject);
        }
    }

}
