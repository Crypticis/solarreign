using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidDamageHandler : DamageHandler
{
    public LootTable itemRewards;
    public Inventory inventory;

    void Start()
    {
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public override void TakeDamage(float damageAmt, GameObject shooter, GameObject projectile)
    {
        health = Mathf.Clamp(health, 0f, maxHealth);

        // Do damage
        if (shooter != null && shooter == player)
        {
            shooter.GetComponent<HitmarkerManager>().Hit();
            health -= damageAmt * 5;
        }

        // Check if Asteroid is destroyed. Blow it up and gib player stuffs
        if (health <= 0 && shooter == player)
        {
            if (deathParticle != null)
                Instantiate(deathParticle, this.transform.position, Quaternion.identity);

            // Rolls loot table to player inventory
            itemRewards.RollTableToInventory(inventory);

            // Shake screen hehe
             ScreenShake.instance.TriggerShake(0.2f, 0.4f);

            // Destroy Asteroid
            Ticker.Ticker.AddItem("TESTING");
            Destroy(gameObject);
        }
    }
}
