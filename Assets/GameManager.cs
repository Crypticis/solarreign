using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //public SystemInfo systemInfo;

    //private IEnumerator coroutine;

    public Inventory inventory;
    public PlayerInfoObject playerInfo;

    [Header("Game Info")]

    public int speedMultiplier = 1;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject inventoryMenu;
    public GameObject FactionMenu;
    public GameObject SkillMenu;
    public GameObject questMenu;
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public GameObject gameOverMenu;
    public GameObject partyMenu;
    public GameObject ledgerMenu;

    [Header("Save Information")]

    public bool isNeedingLoading = false;

    public List<GameObject> NPC = new List<GameObject>();
    //public List<GameObject> mobNPC = new List<GameObject>();
    public GameObject[] settlements;

    public NPCDatabaseObject database;

    public GameObject[] mobPrefabs;

    [Header("Battle Information")]

    public int IdOfEnemyToDestroy;
    public bool playerHasWon, playerHasLost;
    public string nameOfSettlementToConquer;

    public int oreToAdd;
    public int gemsToAdd;
    public int iceToAdd;

    public Item ore;
    public Item gem;

    public bool onMap = false;

    public string previousLocation;
    public string currentLocation;

    public bool isNewGame = false;

    //public Transform[] spawnPoints; 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        SaveSystem.Init();
        NPC = GameObject.FindGameObjectsWithTag("NPC").ToList();

        settlements = GameObject.FindGameObjectsWithTag("Settlement");
    }

    public void UpdatePreviousScene(string previousScene)
    {
        previousLocation = previousScene;
    }
    public void UpdateCurrentScene(string currentScene)
    {
        currentLocation = currentScene;
    }

    private void Update()
    {
        Application.targetFrameRate = 300;

        if (Input.GetButtonDown("Pause") && pauseMenu)
        {
            Pause();
        }

        if (Input.GetButtonDown("Inventory") && inventoryMenu)
        {
            Inventory();
        }

        if (Input.GetButtonDown("Faction") && FactionMenu)
        {
            Faction();
        }

        if (Input.GetButtonDown("SkillTree") && SkillMenu)
        {
            SkillTree();
        }

        if (Input.GetButtonDown("Quest") && questMenu)
        {
            Mission();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            Save();
        }

        if (Input.GetKeyDown(KeyCode.F9))
        {
            Load();
        }
    }

    #region UI Stuff

    public void Pause()
    {
        if (pauseMenu.activeSelf)
        {
            CloseUI();
            pauseMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;

            if (Player.playerInstance)
                Player.playerInstance.controlsEnabled = false;
        }
    }

    public void Inventory()
    {
        if (inventoryMenu.activeSelf)
        {
            CloseUI();
            inventoryMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            inventoryMenu.SetActive(true);
            //Time.timeScale = 0f;
            Player.playerInstance.controlsEnabled = false;
        }
    }

    public void Faction()
    {
        if (FactionMenu.activeSelf)
        {
            CloseUI();
            FactionMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            FactionMenu.SetActive(true);
            //Time.timeScale = 0f;
            Player.playerInstance.controlsEnabled = false;
        }
    }

    public void SkillTree()
    {
        SkillMenu.GetComponentInChildren<SkillTree>().UpdateSkills();

        if (SkillMenu.activeSelf)
        {
            CloseUI();
            SkillMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            SkillMenu.SetActive(true);
            //Time.timeScale = 0f;
            Player.playerInstance.controlsEnabled = false;
        }
    }

    public void Mission()
    {
        if (questMenu.activeSelf)
        {
            CloseUI();
            questMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            questMenu.SetActive(true);
            //Time.timeScale = 0f;
            Player.playerInstance.controlsEnabled = false;
        }
    }

    public void Party()
    {
        partyMenu.GetComponent<PartyUI>().UpdatePartyUI();

        if (partyMenu.activeSelf)
        {
            CloseUI();
            partyMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            partyMenu.SetActive(true);
            Time.timeScale = 0f;
            Player.playerInstance.controlsEnabled = false;
        }
    }

    public void Ledger()
    {
        if (ledgerMenu.activeSelf)
        {
            CloseUI();
            ledgerMenu.SetActive(false);
        }
        else
        {
            CloseUI();
            ledgerMenu.SetActive(true);
            //Time.timeScale = 0f;
            Player.playerInstance.controlsEnabled = false;
        }
    }

    public void OpenSettingsMenu()
    {
        CloseUI();
        SettingsMenu.instance.OpenSettings();
        Time.timeScale = 0f;
    }

    public void OpenControlsMenu()
    {
        CloseUI();
        SettingsMenu.instance.OpenControls();
        Time.timeScale = 0f;
    }
    public void OpenGameOverMenu()
    {
        gameOverMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }


    public void CloseUI()
    {
        if(SceneManager.GetActiveScene().name == "WorldMap")
            Time.timeScale = GameManager.instance.speedMultiplier;
        else
            Time.timeScale = 1;

        if (Player.playerInstance)
            Player.playerInstance.controlsEnabled = true;

        if (questMenu && questMenu.activeSelf)
        {
            questMenu.SetActive(false);
        }

        if (SkillMenu && SkillMenu.activeSelf)
        {
            SkillMenu.SetActive(false);
        }

        if (FactionMenu && FactionMenu.activeSelf)
        {
            FactionMenu.SetActive(false);
        }

        if (inventoryMenu && inventoryMenu.activeSelf)
        {
            inventoryMenu.SetActive(false);
        }

        if (pauseMenu && pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
        }

        if (gameOverMenu && gameOverMenu.activeSelf)
        {
            gameOverMenu.SetActive(false);
        }

        if (partyMenu && partyMenu.activeSelf)
        {
            partyMenu.SetActive(false);
        }

        if (ledgerMenu && ledgerMenu.activeSelf)
        {
            ledgerMenu.SetActive(false);
        }
    }

    #endregion

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        NPC = GameObject.FindGameObjectsWithTag("NPC").ToList();
        settlements = GameObject.FindGameObjectsWithTag("Settlement");

        if(DialogueManager.instance != null)
            DialogueManager.instance.EndDialogue();

        database.UpdateID();

        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            if(!isNewGame)
                if (saveObject.sceneName == scene.name)
                {
                    Load();
                }
                else
                {
                    if(Player.playerInstance != null)
                        LoadPlayer();
                }
        }

        if (isNeedingLoading)
        {
            GameManager.instance.Load();
            isNeedingLoading = false;
        }
        else
        {
            if(isNewGame)
            {
                TutorialManager.instance.OpenTutorial();
                isNewGame = false;
            }
        }

        if (scene.buildIndex > 0)
        {
            for (int i = 0; i < GameObject.Find("Menus").transform.childCount; i++)
            {
                if(GameObject.Find("Menus").transform.GetChild(i).name == "Pause")
                {
                    pauseMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if(GameObject.Find("Menus").transform.GetChild(i).name == "Inventory")
                {
                    inventoryMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if (GameObject.Find("Menus").transform.GetChild(i).name == "Faction")
                {
                    FactionMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if (GameObject.Find("Menus").transform.GetChild(i).name == "Stats")
                {
                    SkillMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if (GameObject.Find("Menus").transform.GetChild(i).name == "Quest")
                {
                    questMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if (GameObject.Find("Menus").transform.GetChild(i).name == "Game Over")
                {
                    gameOverMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if (GameObject.Find("Menus").transform.GetChild(i).name == "Party")
                {
                    partyMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }

                if (GameObject.Find("Menus").transform.GetChild(i).name == "Ledger")
                {
                    ledgerMenu = GameObject.Find("Menus").transform.GetChild(i).gameObject;
                }
            }

            if (SettingsMenu.instance)
            {
                settingsMenu = SettingsMenu.instance.settingsUI;
                controlsMenu = SettingsMenu.instance.controlsUI;
            }
        } 
    }

    public delegate void ResourceEventHandler(string resource, SpaceStation station);
    public static event ResourceEventHandler OnResourceRequest;

    public static void ResourceRequest(string resource, SpaceStation station)
    {
        if (OnResourceRequest != null)
            OnResourceRequest(resource, station);
    }

    // Saving

    public void Save()
    {
        NPC = GameObject.FindGameObjectsWithTag("NPC").ToList();

        Vector3 playerPosition = Player.playerInstance.transform.position;

        Vector3 cameraPosition = Camera.main.transform.position;

        #region Player Stats

        float money = StatManager.instance.playerStatsObject.currentMoney;

        float influenceTemp = StatManager.instance.playerStatsObject.influence;

        int votesTemp = StatManager.instance.playerStatsObject.votes;

        StatObject levelStat = new StatObject
        {
            currentLevel = StatManager.instance.playerStatsObject.level.currentLevel,
            experience = StatManager.instance.playerStatsObject.level.experience,
            maxExp = StatManager.instance.playerStatsObject.level.MAX_EXP,
            maxLevel = StatManager.instance.playerStatsObject.level.MAX_LEVEL
        };

        StatObject pilotingStat = new StatObject
        {
            currentLevel = StatManager.instance.playerStatsObject.Piloting.currentLevel,
            experience = StatManager.instance.playerStatsObject.Piloting.experience,
            maxExp = StatManager.instance.playerStatsObject.Piloting.MAX_EXP,
            maxLevel = StatManager.instance.playerStatsObject.Piloting.MAX_LEVEL
        };

        StatObject missileStat = new StatObject
        {
            currentLevel = StatManager.instance.playerStatsObject.Missile.currentLevel,
            experience = StatManager.instance.playerStatsObject.Missile.experience,
            maxExp = StatManager.instance.playerStatsObject.Missile.MAX_EXP,
            maxLevel = StatManager.instance.playerStatsObject.Missile.MAX_LEVEL
        };

        StatObject projectileStat = new StatObject
        {
            currentLevel = StatManager.instance.playerStatsObject.Projectile.currentLevel,
            experience = StatManager.instance.playerStatsObject.Projectile.experience,
            maxExp = StatManager.instance.playerStatsObject.Projectile.MAX_EXP,
            maxLevel = StatManager.instance.playerStatsObject.Projectile.MAX_LEVEL
        };

        StatObject energyStat = new StatObject
        {
            currentLevel = StatManager.instance.playerStatsObject.Energy.currentLevel,
            experience = StatManager.instance.playerStatsObject.Energy.experience,
            maxExp = StatManager.instance.playerStatsObject.Energy.MAX_EXP,
            maxLevel = StatManager.instance.playerStatsObject.Energy.MAX_LEVEL
        };

        StatObject tradeStat = new StatObject
        {
            currentLevel = StatManager.instance.playerStatsObject.Trade.currentLevel,
            experience = StatManager.instance.playerStatsObject.Trade.experience,
            maxExp = StatManager.instance.playerStatsObject.Trade.MAX_EXP,
            maxLevel = StatManager.instance.playerStatsObject.Trade.MAX_LEVEL
        };

        FleetObject playerFleetObject = new FleetObject
        {
            hasPilots = new bool[Player.playerInstance.fleet.fleet.Count],
            names = new string[Player.playerInstance.fleet.fleet.Count],
            shipNames = new List<string>(),
            levels = new StatObject[Player.playerInstance.fleet.fleet.Count],
            skillpoints = new int[Player.playerInstance.fleet.fleet.Count],
            skills = new FleetSkillObject[Player.playerInstance.fleet.fleet.Count],
        };

        PilotObject pilotObject = new PilotObject
        {
            names = new string[Player.playerInstance.fleet.unusedPilots.Count],
            levels = new StatObject[Player.playerInstance.fleet.unusedPilots.Count],
            skillpoints = new int[Player.playerInstance.fleet.unusedPilots.Count],
            skills = new FleetSkillObject[Player.playerInstance.fleet.unusedPilots.Count],
        };

        #endregion

        for (int i = 0; i < Player.playerInstance.fleet.fleet.Count; i++)
        {
            playerFleetObject.shipNames.Add(Player.playerInstance.fleet.fleet[i].ship.name);

            playerFleetObject.hasPilots[i] = Player.playerInstance.fleet.fleet[i].hasPilot;

            if (playerFleetObject.hasPilots[i])
            {
                playerFleetObject.names[i] = Player.playerInstance.fleet.fleet[i].pilot.name;

                playerFleetObject.levels[i].currentLevel = Player.playerInstance.fleet.fleet[i].pilot.level.currentLevel;
                playerFleetObject.levels[i].experience = Player.playerInstance.fleet.fleet[i].pilot.level.experience;
                playerFleetObject.levels[i].maxExp = Player.playerInstance.fleet.fleet[i].pilot.level.MAX_EXP;
                playerFleetObject.levels[i].maxLevel = Player.playerInstance.fleet.fleet[i].pilot.level.MAX_LEVEL;
                playerFleetObject.skillpoints[i] = Player.playerInstance.fleet.fleet[i].pilot.skillpoints;

                playerFleetObject.skills[i].skills = new List<int>();
                playerFleetObject.skills[i].skills.Add(Player.playerInstance.fleet.fleet[i].pilot.speedSkill);
                playerFleetObject.skills[i].skills.Add(Player.playerInstance.fleet.fleet[i].pilot.firespeedSkill);
                playerFleetObject.skills[i].skills.Add(Player.playerInstance.fleet.fleet[i].pilot.durabilitySkill);
                playerFleetObject.skills[i].skills.Add(Player.playerInstance.fleet.fleet[i].pilot.damageSkill);
                playerFleetObject.skills[i].skills.Add(Player.playerInstance.fleet.fleet[i].pilot.rangeSkill);
            }
        }

        for (int i = 0; i < Player.playerInstance.fleet.unusedPilots.Count; i++)
        {
            pilotObject.names[i] = Player.playerInstance.fleet.unusedPilots[i].name;

            pilotObject.levels[i].currentLevel = Player.playerInstance.fleet.unusedPilots[i].level.currentLevel;
            pilotObject.levels[i].experience = Player.playerInstance.fleet.unusedPilots[i].level.experience;
            pilotObject.levels[i].maxExp = Player.playerInstance.fleet.unusedPilots[i].level.MAX_EXP;
            pilotObject.levels[i].maxLevel = Player.playerInstance.fleet.unusedPilots[i].level.MAX_LEVEL;
            pilotObject.skillpoints[i] = Player.playerInstance.fleet.unusedPilots[i].skillpoints;

            pilotObject.skills[i].skills = new List<int>();
            pilotObject.skills[i].skills.Add(Player.playerInstance.fleet.unusedPilots[i].speedSkill);
            pilotObject.skills[i].skills.Add(Player.playerInstance.fleet.unusedPilots[i].firespeedSkill);
            pilotObject.skills[i].skills.Add(Player.playerInstance.fleet.unusedPilots[i].durabilitySkill);
            pilotObject.skills[i].skills.Add(Player.playerInstance.fleet.unusedPilots[i].damageSkill);
            pilotObject.skills[i].skills.Add(Player.playerInstance.fleet.unusedPilots[i].rangeSkill);
        }

        var playerInv = new List<ItemSlot>();

        for (int i = 0; i < database.inventories[0].itemSlots.Count; i++)
        {
            ItemSlot itemSlot = new ItemSlot
            {
                //type = database.inventories[0].itemSlots[i].item.type,
                amount = database.inventories[0].itemSlots[i].amount,
                Name = database.inventories[0].itemSlots[i].item.Name,
            };

            playerInv.Add(itemSlot);
        }

        SaveObject saveObject = new SaveObject
        {
            money = money,
            playerPosition = playerPosition,
            playerFleet = playerFleetObject,
            pilots = pilotObject,
            cameraPosition = cameraPosition,
            command = StatManager.instance.playerStatsObject.commandLevel,
            tactics = StatManager.instance.playerStatsObject.tacticsLevel,
            logistics = StatManager.instance.playerStatsObject.logisticsLevel,
            items = playerInv.ToArray(),
            shipPlayerIsFlying = playerInfo.shipType,
            votes = votesTemp,
            influence = influenceTemp,
            isShipOwned = new bool[4],
            Name = playerInfo.Name,
            isCaptive = SurrenderHandler.instance.isCaptive,
        };

        if (SurrenderHandler.instance.isCaptive)
        {
            saveObject.idOfCaptor = SurrenderHandler.instance.captor.GetComponent<UniqueNPC>().npc.ID;
        }

        saveObject.stats.Add(levelStat);
        saveObject.stats.Add(pilotingStat);
        saveObject.stats.Add(missileStat);
        saveObject.stats.Add(projectileStat);
        saveObject.stats.Add(energyStat);
        saveObject.stats.Add(tradeStat);

        for (int i = 0; i < database.factions.Length; i++)
        {
            FactionObject factionObject = new FactionObject
            {
                factionName = database.factions[i].name,
            };

            if (database.factions[i].playerInFaction)
            {
                factionObject.playerInFaction = 1;
            }
            else
            {
                factionObject.playerInFaction = 0;
            }

            if(database.factions[i].leader)
                factionObject.npcLeaderName = database.factions[i].leader.Name;

            saveObject.factions.Add(factionObject);
        }

        // Unique NPCs

        foreach (GameObject targetGameObject in NPC)
        {
            Fleet fleet = targetGameObject.GetComponent<Fleet>();

            FleetObject fleetObject = new FleetObject
            {
                shipNames = new List<string>(),
            };

            for (int i = 0; i < fleet.fleet.Count; i++)
            {
                fleetObject.shipNames.Add(fleet.fleet[i].ship.name);
            }

            NPCObject npc = new NPCObject
            {
                npcPosition = targetGameObject.transform.position,
                npcFaction = targetGameObject.GetComponent<FleetFaction>().faction.name,
                npcFleet = fleetObject,
            };

            if (targetGameObject.GetComponent<NPCTraderShip>())
            {
                npc.goods = new List<ItemSlot>();

                for (int i = 0; i < targetGameObject.GetComponent<NPCTraderShip>().tradeGoods.Count; i++)
                {
                    var item = new ItemSlot();

                    item.Name = targetGameObject.GetComponent<NPCTraderShip>().tradeGoods[i].item.name;
                    item.type = targetGameObject.GetComponent<NPCTraderShip>().tradeGoods[i].item.type;
                    item.amount = targetGameObject.GetComponent<NPCTraderShip>().tradeGoods[i].amount;

                    npc.goods.Add(item);
                }

                //mobNpc.goods = mob.GetComponent<NPCTraderShip>().tradeGoods;
            }

            saveObject.npcs.Add(npc);
        }

        foreach (SettlementObject settlement in database.settlements)
        {
            var temp = new List<ItemSlot>();

            for (int i = 0; i < settlement.inventory.itemSlots.Count; i++)
            {
                ItemSlot shopSlot = new ItemSlot
                {
                    type = settlement.inventory.itemSlots[i].item.type,
                    amount = settlement.inventory.itemSlots[i].amount,
                };

                temp.Add(shopSlot);
            }

            SettlementSaveObject settlementObject = new SettlementSaveObject
            {
                settlementFaction = settlement.faction.name,
                buildingLevels = settlement.buildingLevels,
                buildTimers = settlement.buildTimers,
                upgrading = settlement.upgrading,
                loyalty = settlement.loyalty,
                energy = settlement.energy,
                security = settlement.security,
                prosperity = settlement.prosperity,
                population = settlement.population,
                shopSlots = temp.ToArray(),
            };

            saveObject.settlements.Add(settlementObject);
        }

        // Shipyard

        var shipyard = GameObject.Find("MainCanvas").GetComponent<ShipyardUI>().shipyard;

        for (int i = 0; i < shipyard.shipyardSlots.Length; i++)
        {
            saveObject.isShipOwned[i] = shipyard.shipyardSlots[i].isOwned;
        }

        // POI

        for (int i = 0; i < POIManager.instance.pois.Count; i++)
        {
            POISaveObject poi = new POISaveObject
            {
                name = POIManager.instance.pois[i].name,
                prefabName = POIManager.instance.pois[i].prefab.name,
                location = POIManager.instance.pois[i].location,
                sceneName = POIManager.instance.pois[i].sceneName,
            };

            if (POIManager.instance.pois[i].prefab.GetComponent<PirateHideout>())
            {
                poi.maxNPC = POIManager.instance.pois[i].maxNPC;
                poi.prefix = POIManager.instance.pois[i].prefix;
            }

            saveObject.poi.Add(poi);
        }

        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);

        Ticker.Ticker.AddItem("Game saved succesfully.");
    }

    public void LoadPlayer()
    {
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            //Check if player should be spawned at a gate.
            GameObject[] gates = GameObject.FindGameObjectsWithTag("Warp Gate");
            for (int i = 0; i < gates.Length; i++)
            {
                if (gates[i].GetComponent<PortalTeleporter>() && gates[i].GetComponent<PortalTeleporter>().nameOfDestination == previousLocation && !isNewGame)
                {
                    Player.playerInstance.transform.position = gates[i].transform.position;
                }
            }

            Player.playerInstance.fleet.fleet.Clear();
            playerInfo.Name = saveObject.Name;

            #region Stats

            StatManager.instance.playerStatsObject.votes = saveObject.votes;
            StatManager.instance.playerStatsObject.influence = saveObject.influence;
            StatManager.instance.playerStatsObject.currentMoney = saveObject.money;
            StatManager.instance.playerStatsObject.commandLevel = saveObject.command;
            StatManager.instance.playerStatsObject.tacticsLevel = saveObject.tactics;
            StatManager.instance.playerStatsObject.logisticsLevel = saveObject.logistics;

            // Level
            StatManager.instance.playerStatsObject.level.currentLevel = saveObject.stats[0].currentLevel;
            StatManager.instance.playerStatsObject.level.experience = saveObject.stats[0].experience;
            StatManager.instance.playerStatsObject.level.MAX_EXP = saveObject.stats[0].maxExp;
            StatManager.instance.playerStatsObject.level.MAX_LEVEL = saveObject.stats[0].maxLevel;

            // Piloting
            StatManager.instance.playerStatsObject.Piloting.currentLevel = saveObject.stats[1].currentLevel;
            StatManager.instance.playerStatsObject.Piloting.experience = saveObject.stats[1].experience;
            StatManager.instance.playerStatsObject.Piloting.MAX_EXP = saveObject.stats[1].maxExp;
            StatManager.instance.playerStatsObject.Piloting.MAX_LEVEL = saveObject.stats[1].maxLevel;

            // Missile
            StatManager.instance.playerStatsObject.Missile.currentLevel = saveObject.stats[2].currentLevel;
            StatManager.instance.playerStatsObject.Missile.experience = saveObject.stats[2].experience;
            StatManager.instance.playerStatsObject.Missile.MAX_EXP = saveObject.stats[2].maxExp;
            StatManager.instance.playerStatsObject.Missile.MAX_LEVEL = saveObject.stats[2].maxLevel;

            // Projectile
            StatManager.instance.playerStatsObject.Projectile.currentLevel = saveObject.stats[3].currentLevel;
            StatManager.instance.playerStatsObject.Projectile.experience = saveObject.stats[3].experience;
            StatManager.instance.playerStatsObject.Projectile.MAX_EXP = saveObject.stats[3].maxExp;
            StatManager.instance.playerStatsObject.Projectile.MAX_LEVEL = saveObject.stats[3].maxLevel;

            // Energy
            StatManager.instance.playerStatsObject.Energy.currentLevel = saveObject.stats[4].currentLevel;
            StatManager.instance.playerStatsObject.Energy.experience = saveObject.stats[4].experience;
            StatManager.instance.playerStatsObject.Energy.MAX_EXP = saveObject.stats[4].maxExp;
            StatManager.instance.playerStatsObject.Energy.MAX_LEVEL = saveObject.stats[4].maxLevel;

            #endregion


            #region Player Fleet

            for (int i = 0; i < saveObject.playerFleet.shipNames.Count; i++)
            {
                GameObject go = Instantiate(database.GetShip[saveObject.playerFleet.shipNames[i]], Player.playerInstance.transform.position, Quaternion.identity);
                Player.playerInstance.fleet.AddToFleet(go);

                Player.playerInstance.fleet.fleet[i].pilot = new Pilot();
                Player.playerInstance.fleet.fleet[i].pilot.ResetLevels();
                Player.playerInstance.fleet.fleet[i].hasPilot = saveObject.playerFleet.hasPilots[i];

                if (saveObject.playerFleet.hasPilots[i])
                {
                    Player.playerInstance.fleet.fleet[i].pilot.name = saveObject.playerFleet.names[i];

                    Player.playerInstance.fleet.fleet[i].pilot.level.currentLevel = saveObject.playerFleet.levels[i].currentLevel;
                    Player.playerInstance.fleet.fleet[i].pilot.level.experience = saveObject.playerFleet.levels[i].experience;
                    Player.playerInstance.fleet.fleet[i].pilot.level.MAX_EXP = saveObject.playerFleet.levels[i].maxExp;
                    Player.playerInstance.fleet.fleet[i].pilot.level.MAX_LEVEL = saveObject.playerFleet.levels[i].maxLevel;
                    Player.playerInstance.fleet.fleet[i].pilot.skillpoints = saveObject.playerFleet.skillpoints[i];

                    Player.playerInstance.fleet.fleet[i].pilot.speedSkill = saveObject.playerFleet.skills[i].skills[0];
                    Player.playerInstance.fleet.fleet[i].pilot.firespeedSkill = saveObject.playerFleet.skills[i].skills[1];
                    Player.playerInstance.fleet.fleet[i].pilot.durabilitySkill = saveObject.playerFleet.skills[i].skills[2];
                    Player.playerInstance.fleet.fleet[i].pilot.damageSkill = saveObject.playerFleet.skills[i].skills[3];
                    Player.playerInstance.fleet.fleet[i].pilot.rangeSkill = saveObject.playerFleet.skills[i].skills[4];
                }
            }

            for (int i = 0; i < saveObject.pilots.names.Length; i++)
            {
                var pilot = new Pilot();
                Player.playerInstance.fleet.unusedPilots.Add(pilot);
                Player.playerInstance.fleet.unusedPilots[i].ResetLevels();

                Player.playerInstance.fleet.unusedPilots[i].name = saveObject.pilots.names[i];

                Player.playerInstance.fleet.unusedPilots[i].level.currentLevel = saveObject.pilots.levels[i].currentLevel;
                Player.playerInstance.fleet.unusedPilots[i].level.experience = saveObject.pilots.levels[i].experience;
                Player.playerInstance.fleet.unusedPilots[i].level.MAX_EXP = saveObject.pilots.levels[i].maxExp;
                Player.playerInstance.fleet.unusedPilots[i].level.MAX_LEVEL = saveObject.pilots.levels[i].maxLevel;
                Player.playerInstance.fleet.unusedPilots[i].skillpoints = saveObject.pilots.skillpoints[i];

                Player.playerInstance.fleet.unusedPilots[i].speedSkill = saveObject.pilots.skills[i].skills[0];
                Player.playerInstance.fleet.unusedPilots[i].firespeedSkill = saveObject.pilots.skills[i].skills[1];
                Player.playerInstance.fleet.unusedPilots[i].durabilitySkill = saveObject.pilots.skills[i].skills[2];
                Player.playerInstance.fleet.unusedPilots[i].damageSkill = saveObject.pilots.skills[i].skills[3];
                Player.playerInstance.fleet.unusedPilots[i].rangeSkill = saveObject.pilots.skills[i].skills[4];
            }

            #endregion

            // Player Inventory

            for (int i = 0; i < saveObject.items.Length; i++)
            {
                var temp = new Item();

                for (int k = 0; k < database.items.Length; k++)
                {
                    if (database.items[k].Name == saveObject.items[i].Name)
                    {
                        temp = database.items[k];
                    }
                }

                database.inventories[0].AddItem(temp, saveObject.items[i].amount);
            }

            // Shipyard

            var shipyard = GameObject.Find("MainCanvas").GetComponent<ShipyardUI>().shipyard;

            for (int i = 0; i < shipyard.shipyardSlots.Length; i++)
            {
                shipyard.shipyardSlots[i].isOwned = saveObject.isShipOwned[i];
            }

            playerInfo.shipType = saveObject.shipPlayerIsFlying;

            Ticker.Ticker.AddItem("Player data loaded succesfully.");

            for (int i = 0; i < saveObject.poi.Count; i++)
            {
                POIManager.instance.AddPOI(saveObject.poi[i].name, database.GetInteractionPrefab[saveObject.poi[i].prefabName], saveObject.poi[i].location, saveObject.poi[i].sceneName, saveObject.poi[i].prefix, saveObject.poi[i].maxNPC);
            }
        }
    }

    public void Load()
    {
        NPC = GameObject.FindGameObjectsWithTag("NPC").ToList();

        database.ResetInventories();

        string saveString = SaveSystem.Load();
        if (saveString != null)
        {
            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            Player.playerInstance.transform.position = saveObject.playerPosition;
            StatManager.instance.playerStatsObject.currentMoney = saveObject.money;

            Player.playerInstance.fleet.fleet.Clear();

            Camera.main.transform.position = saveObject.cameraPosition;

            playerInfo.Name = saveObject.Name;

            // Stats

            // Level
            StatManager.instance.playerStatsObject.level.currentLevel = saveObject.stats[0].currentLevel;
            StatManager.instance.playerStatsObject.level.experience = saveObject.stats[0].experience;
            StatManager.instance.playerStatsObject.level.MAX_EXP = saveObject.stats[0].maxExp;
            StatManager.instance.playerStatsObject.level.MAX_LEVEL = saveObject.stats[0].maxLevel;

            // Piloting
            StatManager.instance.playerStatsObject.Piloting.currentLevel = saveObject.stats[1].currentLevel;
            StatManager.instance.playerStatsObject.Piloting.experience = saveObject.stats[1].experience;
            StatManager.instance.playerStatsObject.Piloting.MAX_EXP = saveObject.stats[1].maxExp;
            StatManager.instance.playerStatsObject.Piloting.MAX_LEVEL = saveObject.stats[1].maxLevel;

            // Missile
            StatManager.instance.playerStatsObject.Missile.currentLevel = saveObject.stats[2].currentLevel;
            StatManager.instance.playerStatsObject.Missile.experience = saveObject.stats[2].experience;
            StatManager.instance.playerStatsObject.Missile.MAX_EXP = saveObject.stats[2].maxExp;
            StatManager.instance.playerStatsObject.Missile.MAX_LEVEL = saveObject.stats[2].maxLevel;

            // Projectile
            StatManager.instance.playerStatsObject.Projectile.currentLevel = saveObject.stats[3].currentLevel;
            StatManager.instance.playerStatsObject.Projectile.experience = saveObject.stats[3].experience;
            StatManager.instance.playerStatsObject.Projectile.MAX_EXP = saveObject.stats[3].maxExp;
            StatManager.instance.playerStatsObject.Projectile.MAX_LEVEL = saveObject.stats[3].maxLevel;

            // Energy
            StatManager.instance.playerStatsObject.Energy.currentLevel = saveObject.stats[4].currentLevel;
            StatManager.instance.playerStatsObject.Energy.experience = saveObject.stats[4].experience;
            StatManager.instance.playerStatsObject.Energy.MAX_EXP = saveObject.stats[4].maxExp;
            StatManager.instance.playerStatsObject.Energy.MAX_LEVEL = saveObject.stats[4].maxLevel;

            #region Player Fleet

            for (int i = 0; i < saveObject.playerFleet.shipNames.Count; i++)
            {
                GameObject go = Instantiate(database.GetShip[saveObject.playerFleet.shipNames[i]], Player.playerInstance.transform.position, Quaternion.identity);
                Player.playerInstance.fleet.AddToFleet(go);

                Player.playerInstance.fleet.fleet[i].pilot = new Pilot();
                Player.playerInstance.fleet.fleet[i].pilot.ResetLevels();
                Player.playerInstance.fleet.fleet[i].hasPilot = saveObject.playerFleet.hasPilots[i];

                if (saveObject.playerFleet.hasPilots[i])
                {
                    Player.playerInstance.fleet.fleet[i].pilot.name = saveObject.playerFleet.names[i];

                    Player.playerInstance.fleet.fleet[i].pilot.level.currentLevel = saveObject.playerFleet.levels[i].currentLevel;
                    Player.playerInstance.fleet.fleet[i].pilot.level.experience = saveObject.playerFleet.levels[i].experience;
                    Player.playerInstance.fleet.fleet[i].pilot.level.MAX_EXP = saveObject.playerFleet.levels[i].maxExp;
                    Player.playerInstance.fleet.fleet[i].pilot.level.MAX_LEVEL = saveObject.playerFleet.levels[i].maxLevel;
                    Player.playerInstance.fleet.fleet[i].pilot.skillpoints = saveObject.playerFleet.skillpoints[i];

                    Player.playerInstance.fleet.fleet[i].pilot.speedSkill = saveObject.playerFleet.skills[i].skills[0];
                    Player.playerInstance.fleet.fleet[i].pilot.firespeedSkill = saveObject.playerFleet.skills[i].skills[1];
                    Player.playerInstance.fleet.fleet[i].pilot.durabilitySkill = saveObject.playerFleet.skills[i].skills[2];
                    Player.playerInstance.fleet.fleet[i].pilot.damageSkill = saveObject.playerFleet.skills[i].skills[3];
                    Player.playerInstance.fleet.fleet[i].pilot.rangeSkill = saveObject.playerFleet.skills[i].skills[4];
                }
            }

            for (int i = 0; i < saveObject.pilots.names.Length; i++)
            {
                var pilot = new Pilot();
                Player.playerInstance.fleet.unusedPilots.Add(pilot);
                Player.playerInstance.fleet.unusedPilots[i].ResetLevels();

                Player.playerInstance.fleet.unusedPilots[i].name = saveObject.pilots.names[i];

                Player.playerInstance.fleet.unusedPilots[i].level.currentLevel = saveObject.pilots.levels[i].currentLevel;
                Player.playerInstance.fleet.unusedPilots[i].level.experience = saveObject.pilots.levels[i].experience;
                Player.playerInstance.fleet.unusedPilots[i].level.MAX_EXP = saveObject.pilots.levels[i].maxExp;
                Player.playerInstance.fleet.unusedPilots[i].level.MAX_LEVEL = saveObject.pilots.levels[i].maxLevel;
                Player.playerInstance.fleet.unusedPilots[i].skillpoints = saveObject.pilots.skillpoints[i];

                Player.playerInstance.fleet.unusedPilots[i].speedSkill = saveObject.pilots.skills[i].skills[0];
                Player.playerInstance.fleet.unusedPilots[i].firespeedSkill = saveObject.pilots.skills[i].skills[1];
                Player.playerInstance.fleet.unusedPilots[i].durabilitySkill = saveObject.pilots.skills[i].skills[2];
                Player.playerInstance.fleet.unusedPilots[i].damageSkill = saveObject.pilots.skills[i].skills[3];
                Player.playerInstance.fleet.unusedPilots[i].rangeSkill = saveObject.pilots.skills[i].skills[4];
            }

            #endregion

            // Player Inventory

            for (int i = 0; i < saveObject.items.Length; i++)
            {
                var temp = new Item();

                for (int k = 0; k < database.items.Length; k++)
                {
                    if (database.items[k].Name == saveObject.items[i].Name)
                    {
                        temp = database.items[k];
                    }
                }

                database.inventories[0].AddItem(temp, saveObject.items[i].amount);
            }

            // NPCs

            for (int i = 0; i < saveObject.npcs.Count; i++)
            {
                GameObject go = Instantiate(database.GetFleetPrefab[saveObject.npcs[i].prefabName], saveObject.npcs[i].npcPosition, Quaternion.identity);

                Fleet fleet = go.GetComponent<Fleet>();

                go.GetComponent<FleetFaction>().faction = database.GetFaction[saveObject.npcs[i].npcFaction];

                go.GetComponentInChildren<Targeting>().target = null;

                if(go.GetComponent<FleetCommanderAI>())
                    go.GetComponent<FleetCommanderAI>().target = null;

                fleet.fleet.Clear();

                foreach (string shipName in saveObject.npcs[i].npcFleet.shipNames)
                {
                    GameObject ship = Instantiate(database.GetShip[shipName], Player.playerInstance.transform.position, Quaternion.identity);

                    fleet.AddToFleet(go);
                }

                if (go.GetComponent<NPCTraderShip>())
                {
                    for (int j = 0; j < saveObject.npcs[i].goods.Count; j++)
                    {
                        var slot = new global::ItemSlot();

                        for (int k = 0; k < database.items.Length; k++)
                        {
                            if (saveObject.npcs[i].goods[j].Name == database.items[k].name)
                            {
                                slot.item = database.items[k];
                                slot.amount = saveObject.npcs[i].goods[j].amount;
                                go.GetComponent<NPCTraderShip>().tradeGoods.Add(slot);
                            }
                        }
                    }
                }
            }

            // Settlements

            for (int i = 0; i < saveObject.settlements.Count; i++)
            {
                settlements[i].GetComponent<SettlementInfo>().faction = database.GetFaction[saveObject.settlements[i].settlementFaction];
                var space = settlements[i].GetComponent<SpaceStation>();
                space.settlementObject.buildingLevels = saveObject.settlements[i].buildingLevels;
                space.settlementObject.buildTimers = saveObject.settlements[i].buildTimers;
                space.settlementObject.upgrading = saveObject.settlements[i].upgrading;
                space.settlementObject.loyalty = saveObject.settlements[i].loyalty;
                space.settlementObject.energy = saveObject.settlements[i].energy;
                space.settlementObject.security = saveObject.settlements[i].security;
                space.settlementObject.prosperity = saveObject.settlements[i].prosperity;
                space.settlementObject.population = saveObject.settlements[i].population;

                for (int j = 0; j < saveObject.settlements[i].shopSlots.Length; j++)
                {
                    var temp = new Item();

                    for (int k = 0; k < database.items.Length; k++)
                    {
                        if(database.items[k].type == saveObject.settlements[i].shopSlots[j].type)
                        {
                            temp = database.items[k];
                        }
                    }

                    space.GetComponentInParent<SettlementTrader>().inventory.AddItem(temp, saveObject.settlements[i].shopSlots[j].amount);
                }
            }

            for (int i = 0; i < saveObject.factions.Count; i++)
            {
                if(saveObject.factions[i].playerInFaction == 1)
                {
                    database.GetFaction[saveObject.factions[i].factionName].playerInFaction = true;
                } 
                else
                {
                    database.GetFaction[saveObject.factions[i].factionName].playerInFaction = false;
                }

                for (int j = 0; j < NPC.Count; j++)
                {
                    if(NPC[j].GetComponent<UniqueNPC>().npc.Name == saveObject.factions[i].npcLeaderName)
                    {
                        database.GetFaction[saveObject.factions[i].factionName].leader = NPC[j].GetComponent<UniqueNPC>().npc;
                    }
                } 
            }

            StatManager.instance.playerStatsObject.commandLevel = saveObject.command;
            StatManager.instance.playerStatsObject.tacticsLevel = saveObject.tactics;
            StatManager.instance.playerStatsObject.logisticsLevel = saveObject.logistics;
            playerInfo.shipType = saveObject.shipPlayerIsFlying;

            StatManager.instance.playerStatsObject.votes = saveObject.votes;
            StatManager.instance.playerStatsObject.influence = saveObject.influence;

            // Shipyard

            var shipyard = GameObject.Find("MainCanvas").GetComponent<ShipyardUI>().shipyard;

            for (int i = 0; i < shipyard.shipyardSlots.Length; i++)
            {
                shipyard.shipyardSlots[i].isOwned = saveObject.isShipOwned[i];
            }

            //POI

            for (int i = 0; i < saveObject.poi.Count; i++)
            {
                POIManager.instance.AddPOI(saveObject.poi[i].name, database.GetInteractionPrefab[saveObject.poi[i].prefabName], saveObject.poi[i].location, saveObject.poi[i].sceneName, saveObject.poi[i].prefix, saveObject.poi[i].maxNPC);
            }

            //mobNPC = GameObject.FindGameObjectsWithTag("_NPC").ToList();

            Ticker.Ticker.AddItem("Save loaded succesfully.");
        }
    }


    private class SaveObject
    {
        public string sceneName;
        public List<NPCObject> npcs = new List<NPCObject>();
        //public List<MobNPCObject> mobNpcs = new List<MobNPCObject>();

        public List<SettlementSaveObject> settlements = new List<SettlementSaveObject>();
        public List<FactionObject> factions = new List<FactionObject>();
        public List<StatObject> stats = new List<StatObject>();
        public List<POISaveObject> poi = new List<POISaveObject>();

        public int command;
        public int tactics;
        public int logistics;

        public float money;
        public Vector3 playerPosition;
        public Vector3 targetPosition;
        public Vector3 agentDestination;
        public Vector3 cameraPosition;
        public FleetObject playerFleet;
        public PilotObject pilots;
        public string playerFaction;

        public ItemSlot[] items;
        public ShipType shipPlayerIsFlying;
        public bool[] isShipOwned;

        public float influence;
        public int votes;
        public string Name;
        public bool isCaptive;
        public int idOfCaptor;
    }

    [System.Serializable]
    struct FleetObject
    {
        public List<string> shipNames;

        //public PilotObject[] pilots;
        public bool[] hasPilots;

        public string[] names;

        public StatObject[] levels;

        public int[] skillpoints;

        public FleetSkillObject[] skills;
    }

    [System.Serializable]
    struct PilotObject
    {
        public string[] names;

        public StatObject[] levels;

        public int[] skillpoints;

        public FleetSkillObject[] skills;
    }

    [System.Serializable]
    struct NPCObject
    {
        public Vector3 npcPosition;
        public FleetObject npcFleet;
        public string npcFaction;
        public List<ItemSlot> goods;
        public string prefabName;
        //public int isActive;
    }

    [System.Serializable]
    struct MobNPCObject
    {
        public Vector3 npcPosition;
        public FleetObject npcFleet;
        public string npcFaction;
        public int indexOfPrefab;
        public int ID;
        public List<ItemSlot> goods;
    }

    [System.Serializable]
    struct SettlementSaveObject
    {
        public string settlementFaction;

        public ResourceType resource;

        // Building

        public int[] buildingLevels;
        public float[] buildTimers;
        public bool[] upgrading;

        // Resources

        public float loyalty;
        public float energy;
        public float security;
        public float prosperity;
        public float population;

        // Shop

        public ItemSlot[] shopSlots;
    }

    [System.Serializable]
    struct FactionObject
    {
        public string factionName;
        public int playerInFaction;
        public string npcLeaderName;
    }

    [System.Serializable]
    struct StatObject
    {
        public int currentLevel;
        public int experience;
        public int maxExp;
        public int maxLevel;
    }

    [System.Serializable]
    struct ItemSlot
    {
        public string Name;
        public ItemType type;
        public int amount;
    }


    [System.Serializable]
    struct FleetSkillObject
    {
        public List<int> skills;
    }

    [System.Serializable]
    struct POISaveObject
    {
        public string name;
        public string prefabName;
        public Vector3 location;
        public string sceneName;

        //

        public string prefix;
        public int maxNPC;
    }
}
