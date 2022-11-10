using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatManager : MonoBehaviour
{
    public static StatManager instance;

    public PlayerStatsObject playerStatsObject;

    public GameObject SkillTree;

    [Header("Currency")]
    public TMP_Text moneyReadout;

    public string[] pilotNames;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        playerStatsObject.level = new Level(1, OnLevelUp);
        playerStatsObject.Piloting = new Level(1, OnSkillUp);
        playerStatsObject.Missile = new Level(1, OnSkillUp);
        playerStatsObject.Projectile = new Level(1, OnSkillUp);
        playerStatsObject.Energy = new Level(1, OnSkillUp);
        playerStatsObject.Trade = new Level(1, OnSkillUp);
    }

    public void UpdateMoney(float amount)
    {
        playerStatsObject.currentMoney += amount;

        UpdateMoneyReadout();
    }

    public void UpdateMoneyReadout()
    {
        moneyReadout.text = playerStatsObject.currentMoney.ToString();
    }

    public void Update()
    {
        if (!moneyReadout && SceneManager.GetActiveScene().buildIndex == 1)
        {
            moneyReadout = GameObject.Find("HUD").transform.GetChild(0).GetComponent<TMP_Text>();
        }

        if (moneyReadout)
        {
            moneyReadout.text = string.Format("${0}", (playerStatsObject.currentMoney.ToString("n2")));
        }
    }

    public void OnLevelUp()
    {
        Ticker.Ticker.AddItem((string.Format("Leveled up. Now level {0}.", (playerStatsObject.level.currentLevel).ToString(), (playerStatsObject.skillpoints).ToString())), 5f, Color.white);
        Ticker.Ticker.AddItem(string.Format("{0} Available skillpoints.", (playerStatsObject.level.currentLevel).ToString(), 5f, Color.white));

        playerStatsObject.skillpoints++;
    }

    public void OnSkillUp()
    {
        Ticker.Ticker.AddItem("Skill level increased.");
    }

    public void AttributeModified(Attribute attribute)
    {

    }

    public void CompleteBackground()
    {
        playerStatsObject.commandLevel = 0 + playerStatsObject.startingCommandLevel;
        playerStatsObject.tacticsLevel = 0 + playerStatsObject.startingTacticsLevel;
        playerStatsObject.logisticsLevel = 0 + playerStatsObject.startingLogisticsLevel;
        playerStatsObject.productionLevel = 0 + playerStatsObject.startingProductionLevel;

        playerStatsObject.Piloting.currentLevel += playerStatsObject.startingPiloting;
        playerStatsObject.Missile.currentLevel += playerStatsObject.startingMissile;
        playerStatsObject.Projectile.currentLevel += playerStatsObject.startingProjectile;
        playerStatsObject.Energy.currentLevel += playerStatsObject.startingEnergy;
        playerStatsObject.Trade.currentLevel += playerStatsObject.startingTrade;

        playerStatsObject.startingCommandLevel = 0;
        playerStatsObject.startingTacticsLevel = 0;
        playerStatsObject.startingLogisticsLevel = 0;
        playerStatsObject.startingProductionLevel = 0;
        playerStatsObject.startingPiloting = 0;
        playerStatsObject.startingMissile = 0;
        playerStatsObject.startingProjectile = 0;
        playerStatsObject.startingEnergy = 0;
        playerStatsObject.startingTrade = 0;
    }


    public string GeneratePilotName()
    {
        string name = "";

        name = pilotNames[Random.Range(0, pilotNames.Length)];

        return name;
    }

    public Relation GetRelation(Faction faction)
    {
        for (int i = 0; i < playerStatsObject.relations.Length; i++)
        {
            if (playerStatsObject.relations[i].faction == faction)
                return playerStatsObject.relations[i];
        }

        return null;
    }
}

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public StatManager parent;
    //public Attributes type;
    //public ModifiableInt value;

    public void SetParent(StatManager _parent)
    {
        parent = _parent;
        //value = new ModifiableInt(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}

[System.Serializable]
public class Relation
{
    public int relation;
    public Faction faction;
}
