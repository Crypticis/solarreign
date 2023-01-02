using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ticker;
using GNB;

public class DamageHandler : MonoBehaviour
{
    public DefenseType defenseType;

    [Header("Health Settings")]

    //public float currentArmor;
    //public float maxArmor;

    public float currentShield;
    public float maxShield;
    public float shieldRecoveryTotal;

    public float health;
    public float maxHealth;

    [Header("Drop Settings")]
    public GameObject deathParticle;

    public int experienceReward = 0;
    public int moneyReward = 1000;

    [Header("Player Settings")]

    public bool isPlayer = false;
    public GameObject player;

    [HideInInspector]
    public int amount = 0;

    const float BASE_SHIELD_RECOVERY = .025f;
    public float shieldRecoveryModifier = 0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        StartCoroutine("ShieldRecovery");

        Invoke("SetupValues", 1f);
    }

    void SetupValues()
    {
        health = maxHealth;
        currentShield = maxShield;

        CalculateShieldRecovery();
    }

    public void Update()
    {
        currentShield = Mathf.Clamp(currentShield, 0, maxShield);

        //currentShield += Time.deltaTime * shieldRecovery;
    }

    public void CalculateShieldRecovery()
    {
        shieldRecoveryTotal = maxShield * (BASE_SHIELD_RECOVERY + shieldRecoveryModifier);
    }

    IEnumerator ShieldRecovery()
    {
        while (true)
        {
            currentShield += shieldRecoveryTotal;

            yield return new WaitForSeconds(1f);
        }
    }

    public virtual void TakeDamage(float damageAmt, GameObject shooter, GameObject projectile)
    {
        health = Mathf.Clamp(health, 0f, maxHealth);

        if (shooter != null && shooter == player)
        {
            shooter.GetComponent<HitmarkerManager>().Hit();

            var wepType = projectile.GetComponent<WeaponInfo>();

            if (wepType.weaponType == WeaponType.missile)
            {
                if(defenseType == DefenseType.shield)
                {
                    var damageTotal = damageAmt * 1.1f + (StatManager.instance.Missile.currentLevel * .1f);

                    var shield = currentShield;

                    if (shield > 0)
                    {
                        var temp = shield -= damageTotal;

                        if (temp < 0)
                        {
                            currentShield = 0;
                            health -= Mathf.Abs(temp);
                        }
                        else
                        {
                            currentShield = temp;
                        }
                    }
                    else
                    {
                        health -= damageTotal;
                    }

                    //health -= damageTotal;

                    //shooter.GetComponent<HitmarkerManager>().Hit();

                    Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageTotal).ToString("0"), (this.name).ToString())), 5f, Color.white);
                }
                else
                {
                    var damageTotal = damageAmt + (StatManager.instance.Missile.currentLevel * .1f);

                    var shield = currentShield;

                    if (shield > 0)
                    {
                        var temp = shield -= damageTotal;

                        if (temp < 0)
                        {
                            currentShield = 0;
                            health -= Mathf.Abs(temp);
                        }
                        else
                        {
                            currentShield = temp;
                        }
                    }
                    else
                    {
                        health -= damageTotal;
                    }

                    Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageTotal).ToString("0"), (this.name).ToString())), 5f, Color.white);
                }

                StatManager.instance.Missile.AddExp(5);
            } 
            else if (wepType.weaponType == WeaponType.energy)
            {
                amount++;

                if (defenseType == DefenseType.armor)
                {
                    var damageTotal = damageAmt * 1.1f + (StatManager.instance.Projectile.currentLevel * .1f);

                    var shield = currentShield;

                    if (shield > 0)
                    {
                        var temp = shield -= damageTotal;

                        if (temp < 0)
                        {
                            currentShield = 0;
                            health -= Mathf.Abs(temp);
                        }
                        else
                        {
                            currentShield = temp;
                        }
                    }
                    else
                    {
                        health -= damageTotal;
                    }

                    if (amount >= 20)
                    {
                        Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageTotal * amount).ToString("0"), (this.name).ToString())), 5f, Color.white);
                        amount = 0;
                        StatManager.instance.Projectile.AddExp(2);
                    }
                }
                else
                {
                    var damageTotal = damageAmt + (StatManager.instance.Projectile.currentLevel * .1f);

                    var shield = currentShield;

                    if (shield > 0)
                    {
                        var temp = shield -= damageTotal;

                        if (temp < 0)
                        {
                            currentShield = 0;
                            health -= Mathf.Abs(temp);
                        }
                        else
                        {
                            currentShield = temp;
                        }
                    }
                    else
                    {
                        health -= damageTotal;
                    }

                    if (amount >= 20)
                    {
                        Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageTotal * amount).ToString("0"), (this.name).ToString())), 5f, Color.white);
                        amount = 0;
                        StatManager.instance.Projectile.AddExp(2);
                    }
                }
            }
            else if(wepType.weaponType == WeaponType.laser)
            {
                amount++;

                var damageTotal = (damageAmt + (StatManager.instance.Energy.currentLevel * .1f));


                var shield = currentShield;

                if (shield > 0)
                {
                    var temp = shield -= damageTotal;

                    if (temp < 0)
                    {
                        currentShield = 0;
                        health -= Mathf.Abs(temp);
                    }
                    else
                    {
                        currentShield = temp;
                    }
                }
                else
                {
                    health -= damageTotal;
                }

                if (amount >= 50)
                {
                    Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> did {1} damage to <color=#d63031>{2}</color>", (shooter.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (damageAmt * amount).ToString("0"), (this.name).ToString())), 5f, Color.white);
                    amount = 0;
                    StatManager.instance.Energy.AddExp(2);
                }
            }
        } 
        else
        {
            var shield = currentShield;

            if (shield > 0)
            {
                if ((shield -= damageAmt) < 0f)
                {
                    var overDamage = Mathf.Abs(shield -= damageAmt);

                    shield -= damageAmt;

                    currentShield = shield;

                    health -= overDamage;
                }
                else
                {
                    shield -= damageAmt;

                    currentShield = shield;
                }
            }
            else
            {
                health -= damageAmt;
            }
        }

        if(health <= 0)
        {
            if(shooter != null)
                Defeat(shooter);
        }

        if (isPlayer)
        {
            //HealthUI.instance.UpdateHealth();
        }
    }

    public virtual void Defeat(GameObject killer)
    {
        if (deathParticle != null)
            Instantiate(deathParticle, this.transform.position, Quaternion.identity);

        if (isPlayer == false && killer == player)
        {
            //StatManager.instance.currentMoney += Random.Range(minMoneyReward, maxMoneyReward);

            Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> destroyed <color=#d63031>{1}</color>", (killer.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (GetComponent<HUDElements>().name).ToString())), 5f, Color.white);

            StatManager.instance.level.AddExp(experienceReward);

            if (experienceReward != 0)
                Ticker.Ticker.AddItem(string.Format("Gained <color=#00b894>{0}</color> experience.", (experienceReward).ToString()), 5f, Color.white);

            QuestManager.instance.ObjectiveUpdate(GetComponent<HUDElements>().name);
        }

        if (killer != player && killer.GetComponent<FleetMember>())
        {
            //if(GetComponent<Targeting>().isAlly)
            //    Ticker.Ticker.AddItem(string.Format(shooter.GetComponent<HUDElements>().name + " defeated " + this.GetComponent<HUDElements>().name + "."), 5f, Color.white);
            killer.GetComponent<FleetMember>().level.AddExp(experienceReward);
        }

        Destroy(gameObject);
    }
}

public enum DefenseType
{
    shield,
    armor,
    point_defense
}
