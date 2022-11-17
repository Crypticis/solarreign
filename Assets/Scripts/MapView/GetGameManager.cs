using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetGameManager : MonoBehaviour
{
    public BattleManager battleManager;
    public GameManager gameManager;
    public Player playerMap;
    public Fleet fleet;

    public void Awake()
    {
        gameManager = GameManager.instance;
        battleManager = BattleManager.instance;
        playerMap = Player.playerInstance;

        fleet = GetComponent<Fleet>();
    }

    public void Update()
    {
        if(gameManager == null)
        {
            gameManager = GameManager.instance;
        }

        if (battleManager == null)
        {
            battleManager = BattleManager.instance;
        }

        if (playerMap == null)
        {
            playerMap = Player.playerInstance;
        }
    }

    public void Save()
    {
        gameManager.Save();
    }

    public void Load()
    {
        gameManager.Load();
    }

    public void LoadBattleScene(int index)
    {
        //battleManager.LoadBattleScene(index);
        SceneManager.LoadScene(index);
    }

    public void LoadBattle()
    {
        //Debug.Log("1");

        gameManager.Save();

        // stops here? I think its fixed now.

        battleManager.LoadFleet(fleet);
        battleManager.LoadPlayerFleet();
        //gameManager.SetEnemy(gameObject);

        SceneManager.LoadScene("BattleScene1");
    }

    public void LoadSettlementFleet(SpaceStationUI spaceStationUI)
    {
        battleManager.LoadSettlementFleet(spaceStationUI.spaceStation.gameObject.GetComponent<SettlementFleet>());
    }

    public void LoadPlayerFleet()
    {
        battleManager.LoadPlayerFleet();
    }

    public void LoadSiege(SpaceStationUI spaceStationUI)
    {
        //gameManager.SetSiege(spaceStationUI.spaceStation.gameObject.GetComponent<SettlementInfo>());
    }

    public void CloseUI()
    {
        //playerMap.CloseUI();
    }

    public void PlayerHasWon() 
    {
        gameManager.playerHasWon = true;
    }

    public void PlayerHasLost()
    {
        gameManager.playerHasLost = true;
    }

    //public void SetEnemy(GameObject npc)
    //{
    //    gameManager.SetEnemy(npc);
    //}

    //public void OpenSettings()
    //{
    //    gameManager.OpenSettingsMenu();
    //}

    //public void OpenControls()
    //{
    //    gameManager.OpenControlsMenu();
    //}

    public void OpenGameOver()
    {
        gameManager.OpenGameOverMenu();
    }

    public void MainMenu()
    {
        gameManager.LoadMainMenu();
    }

    public void ExitGame()
    {
        gameManager.ExitGame();
    }

    public void ShowControls()
    {
        Close();
        if (!gameManager.controlsMenu.activeSelf)
        {
            gameManager.controlsMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ShowPause()
    {
        Close();
        if (!gameManager.pauseMenu.activeSelf)
        {
            gameManager.pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void ShowSettings()
    {
        Close();
        if (!gameManager.settingsMenu.activeSelf)
        {
            gameManager.settingsMenu.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Close()
    {
        //Time.timeScale = GameManager.instance.speedMultiplier;
        GameManager.instance.CloseUI();
    }

    public void Surrender()
    {
        SurrenderHandler.instance.Surrender(gameObject);
    }

}
