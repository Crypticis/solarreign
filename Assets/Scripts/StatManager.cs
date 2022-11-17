using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StatManager : MonoBehaviour
{
    [Header("Stats & Proficiences")]
    public int skillpoints;
    public Level level;

    [Header("Proficiences")]
    public Level Piloting;
    public Level Missile;
    public Level Projectile;
    public Level Energy;
    public Level Trade;

    [Header("Stats")]
    public int commandLevel = 0;
    public int tacticsLevel = 0;
    public int logisticsLevel = 0;
    public int productionLevel = 0;

    [Header("Fleet Stuff")]
    public float currentInFleet;
    public float maxInFleet;

    [Header("Inventory")]
    public Inventory playerInventory;

    [Header("Currency")]
    public float currentMoney = 0f;

    [Header("Voting Stuff")]
    public float influence;
    public int votes;

    [Header("Starting Stats")]
    public int startingPiloting;
    public int startingMissile;
    public int startingProjectile;
    public int startingEnergy;
    public int startingTrade;

    public int startingCommandLevel = 0;
    public int startingTacticsLevel = 0;
    public int startingLogisticsLevel = 0;
    public int startingProductionLevel = 0;

    public SelectCharacter.CharacterType selectedCharType;

    [Header("Relations")]
    public Relation[] relations;


    public static StatManager instance;

    //public PlayerStatsObject playerStatsObject;

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
        level = new Level(1, OnLevelUp);
        Piloting = new Level(1, OnSkillUp);
        Missile = new Level(1, OnSkillUp);
        Projectile = new Level(1, OnSkillUp);
        Energy = new Level(1, OnSkillUp);
        Trade = new Level(1, OnSkillUp);
    }

    public void UpdateMoney(float amount)
    {
        currentMoney += amount;

        UpdateMoneyReadout();
    }

    public void UpdateMoneyReadout()
    {
        moneyReadout.text = currentMoney.ToString();
    }

    public void Update()
    {
        if (!moneyReadout && SceneManager.GetActiveScene().buildIndex == 1)
        {
            moneyReadout = GameObject.Find("HUD").transform.GetChild(0).GetComponent<TMP_Text>();
        }

        if (moneyReadout)
        {
            moneyReadout.text = string.Format("${0}", (currentMoney.ToString("n2")));
        }
    }

    public void OnLevelUp()
    {
        Ticker.Ticker.AddItem((string.Format("Leveled up. Now level {0}.", (level.currentLevel).ToString(), (skillpoints).ToString())), 5f, Color.white);
        Ticker.Ticker.AddItem(string.Format("{0} Available skillpoints.", (level.currentLevel).ToString(), 5f, Color.white));

        skillpoints++;
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
        commandLevel = 0 + startingCommandLevel;
        tacticsLevel = 0 + startingTacticsLevel;
        logisticsLevel = 0 + startingLogisticsLevel;
        productionLevel = 0 + startingProductionLevel;

        Piloting.currentLevel += startingPiloting;
        Missile.currentLevel += startingMissile;
        Projectile.currentLevel += startingProjectile;
        Energy.currentLevel += startingEnergy;
        Trade.currentLevel += startingTrade;

        startingCommandLevel = 0;
        startingTacticsLevel = 0;
        startingLogisticsLevel = 0;
        startingProductionLevel = 0;
        startingPiloting = 0;
        startingMissile = 0;
        startingProjectile = 0;
        startingEnergy = 0;
        startingTrade = 0;
    }


    public string GeneratePilotName()
    {
        string name = "";

        name = pilotNames[Random.Range(0, pilotNames.Length)];

        return name;
    }

    public Relation GetRelation(Faction faction)
    {
        for (int i = 0; i < relations.Length; i++)
        {
            if (relations[i].faction == faction)
                return relations[i];
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
