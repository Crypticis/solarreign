using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance;

    public GameObject main;
    public GameObject factionSelect;
    public GameObject nameSelect;
    public GameObject backgroundSelect;
    public GameObject credits;
    public NPCDatabaseObject database;

    public PlayerInfoObject playerInfo;
    public TMP_InputField nameInput;

    private void Awake()
    {
        instance = this;
        Cursor.visible = true;
        Time.timeScale = 1f;
    }

    public void Start()
    {
        //MusicManager.instance.StopAll(MusicManager.instance.battleMusic);
        //MusicManager.instance.StopAll(MusicManager.instance.sounds);

        MusicManager.instance.PlayRandomMusic();
    }

    public void NewGame()
    {
        database.ResetNPC();
        database.ResetInventories();
        database.AddDefaultInventories();
        database.ResetShipInfo();
        playerInfo.shipType = ShipType.fighter;
        FactionManager.instance.ResetFactions();
        //QuestManager.instance.ResetQuests();
        AudioManager.instance.Play("Click");
        StatManager.instance.playerStatsObject.currentMoney = 10000f;
        SceneManager.LoadScene(1);
        GameManager.instance.isNeedingLoading = false;
        GameManager.instance.isNewGame = true;

        StatManager.instance.CompleteBackground();
    }

    public void LoadGame()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(1);
        GameManager.instance.isNeedingLoading = true;
    }

    public void CloseUI()
    {
        if (SettingsMenu.instance.controlsUI.activeSelf)
        {
            SettingsMenu.instance.settingsUI.GetComponent<EnlargePanel>().Shrink();
            SettingsMenu.instance.controlsUI.SetActive(false);
        }

        if (main.activeSelf)
        {
            main.SetActive(false);
        }

        if (SettingsMenu.instance.settingsUI.activeSelf)
        {
            SettingsMenu.instance.settingsUI.SetActive(false);
        }

        if (factionSelect.activeSelf)
        {
            factionSelect.SetActive(false);
        }

        if (backgroundSelect.activeSelf)
        {
            backgroundSelect.SetActive(false);
        }

        if (nameSelect.activeSelf)
        {
            nameSelect.SetActive(false);
        }

        if (credits.activeSelf)
        {
            credits.SetActive(false);
        }
    }

    public void ShowControls()
    {
        AudioManager.instance.Play("Click");
        CloseUI();
        if (!SettingsMenu.instance.controlsUI.activeSelf)
        {
            SettingsMenu.instance.controlsUI.SetActive(true);
            SettingsMenu.instance.controlsUI.GetComponent<EnlargePanel>().Enlarge();
        }
    }

    public void ShowMain()
    {
        AudioManager.instance.Play("Click");
        CloseUI();
        if (!main.activeSelf)
        {
            main.SetActive(true);
        }
    }

    public void ShowSettings()
    {
        AudioManager.instance.Play("Click");
        CloseUI();
        if (!SettingsMenu.instance.settingsUI.activeSelf)
        {
            SettingsMenu.instance.settingsUI.SetActive(true);
            SettingsMenu.instance.settingsUI.GetComponent<EnlargePanel>().Enlarge();
        }
    }

    public void ShowCredits()
    {
        AudioManager.instance.Play("Click");

        if (!credits.activeSelf)
        {
            CloseUI();
            credits.SetActive(true);
        }
        else
        {
            CloseUI();
            LeanTween.alphaCanvas(factionSelect.GetComponentInParent<CanvasGroup>(), 0f, .25f);
            credits.SetActive(false);
            ShowMain();
        }
    }

    public void ToggleFactionSelect()
    {
        AudioManager.instance.Play("Click");

        if (!factionSelect.activeSelf)
        {
            CloseUI();
            factionSelect.SetActive(true);
        } 
        else
        {
            LeanTween.alphaCanvas(factionSelect.GetComponentInParent<CanvasGroup>(), 0f, .25f);
            Invoke("CloseFactionSelect", .25f);
            Invoke("ShowMain", .25f);
        }
    }

    public void ToggleNameSelect()
    {
        AudioManager.instance.Play("Click");

        if (!nameSelect.activeSelf)
        {
            CloseUI();
            nameSelect.SetActive(true);
        }
        else
        {
            LeanTween.alphaCanvas(nameSelect.GetComponentInParent<CanvasGroup>(), 0f, .25f);
            Invoke("CloseNameSelect", .25f);
            Invoke("ShowMain", .25f);
        }
    }

    public void ToggleBackgroundSelect()
    {
        AudioManager.instance.Play("Click");

        if (!backgroundSelect.activeSelf)
        {
            CloseUI();
            backgroundSelect.SetActive(true);
        }
        else
        {
            LeanTween.alphaCanvas(nameSelect.GetComponentInParent<CanvasGroup>(), 0f, .25f);
            Invoke("CloseBackgroundSelect", .25f);
            Invoke("ShowMain", .25f);
        }
    }

    public void LoadMainMenu()
    {
        AudioManager.instance.Play("Click");
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        AudioManager.instance.Play("Click");
        Application.Quit();
    }

    public void CloseFactionSelect()
    {
        factionSelect.SetActive(false);
    }

    public void CloseNameSelect()
    {
        nameSelect.SetActive(false);
    }

    public void CloseBackgroundSelect()
    {
        backgroundSelect.SetActive(false);
    }

    public void SetName()
    {
        if(nameInput.text.Length > 0)
        {
            playerInfo.Name = nameInput.text;
        }
        else
        {
            playerInfo.Name = "Player";
        }
    }
}
