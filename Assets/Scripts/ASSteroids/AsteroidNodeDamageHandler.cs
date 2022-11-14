using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidNodeDamageHandler : DamageHandler
{
    [SerializeField]
    private LootTable itemRewards;
    [SerializeField]
    private Inventory inventory;

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
                Instantiate(deathParticle, transform.position, Quaternion.identity);

            // Rolls loot table to player inventory
            itemRewards.RollTableToInventory(inventory);

            // Destroy Asteroid if no nodes remain
            if ((this.transform.parent.gameObject.transform.childCount) - 1 == 0)
            {
                Destroy(gameObject);
                Destroy(this.transform.parent.gameObject, 5f);
                Ticker.Ticker.AddItem("This asteroid has been depleted");
            }
            // Destroy Node
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
