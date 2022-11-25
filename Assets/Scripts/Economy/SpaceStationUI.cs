using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpaceStationUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text ownerText;
    public TMP_Text npcOwnerText;
    public TMP_Text foodText;
    public TMP_Text energyText;
    public TMP_Text securityText;
    public TMP_Text prosperityText;
    public TMP_Text housingText;
    public TMP_Text populationText;

    //public Button dockButton;
    public Button buildButton;
    public Button shopButton;
    public Button recruitButton;
    public Button attackButton;
    public Button buyStationButton;
    public Button ShipyardButton;
    public Button mechanicButton;
    public Button productionButton;

    public SpaceStation spaceStation;
    public BuildingUI buildingUI;
    public GameObject HUD;
    public GameObject shopUI;
    public GameObject rectuitmentUI;
    public GameObject shipyardUI;
    public GameObject mechanicUI;
    public GameObject productionUI;
    public GameObject bountyUI;

    public CameraPan camPan;
    public CameraRotateZoom crz;

    public Camera fighterCam, cruiserCam;

    public GameObject mechanicRenderCam;
    public GameObject shipyardRenderCam;

    public GameObject interactText;

    void Update()
    {
        if (spaceStation)
        {
            interactText.SetActive(false);

            if (spaceStation.GetComponent<SettlementInfo>().faction.playerInFaction == false)
            {
                for (int i = 0; i < spaceStation.GetComponent<SettlementInfo>().faction.enemies.Count; i++)
                {
                    if (spaceStation.GetComponent<SettlementInfo>().faction.enemies[i].playerInFaction )
                    {
                        recruitButton.interactable = false;
                        shopButton.interactable = false;
                        buyStationButton.interactable = false;
                        attackButton.interactable = true;
                        ShipyardButton.interactable = false;
                        mechanicButton.interactable = false;
                        productionButton.interactable = false;
                    }
                    else
                    {
                        recruitButton.interactable = true;
                        shopButton.interactable = true;
                        attackButton.interactable = false;
                        buyStationButton.interactable = false;
                        ShipyardButton.interactable = true;
                        mechanicButton.interactable = true;
                        productionButton.interactable = true;
                    }
                }

                if (spaceStation.settlementObject.isPlayerOwned == false)
                    buyStationButton.interactable = false;

                buildButton.interactable = false;
                //dockButton.interactable = false;
            } 
            else
            {
                //dockButton.interactable = false;
                if (spaceStation.settlementObject.isPlayerOwned)
                {
                    buildButton.interactable = true;
                } 
                else
                {
                    buildButton.interactable = false;
                }
                shopButton.interactable = true;
                recruitButton.interactable = true;
                attackButton.interactable = false;
                ShipyardButton.interactable = true;
            }

            nameText.text = spaceStation.GetComponent<SettlementInfo>().Name;
            ownerText.text = spaceStation.GetComponent<SettlementInfo>().faction.name;

            if (spaceStation.GetComponent<SettlementInfo>().npc)
                npcOwnerText.text = spaceStation.GetComponent<SettlementInfo>().npc.Name;
            else
                npcOwnerText.text = "None";

            buildingUI.spaceStation = spaceStation;

            if (spaceStation.settlementObject.energy < 1000)
            {
                energyText.text = (spaceStation.settlementObject.energy).ToString("0");
            }
            else if (spaceStation.settlementObject.energy < 1000000)
            {
                energyText.text = (spaceStation.settlementObject.energy / 1000).ToString("0.000k");
            }
            else
            {
                energyText.text = (spaceStation.settlementObject.energy / 1000000).ToString("0.000m");
            }

            if (spaceStation.settlementObject.prosperity < 1000)
            {
                prosperityText.text = (spaceStation.settlementObject.prosperity).ToString("0");
            }
            else if (spaceStation.settlementObject.prosperity < 1000000)
            {
                prosperityText.text = (spaceStation.settlementObject.prosperity / 1000).ToString("0.000k");
            }
            else
            {
                prosperityText.text = (spaceStation.settlementObject.prosperity / 1000000).ToString("0.000m");
            }

            if (spaceStation.settlementObject.housing < 1000)
            {
                housingText.text = (spaceStation.settlementObject.housing).ToString("0");
            }
            else if (spaceStation.settlementObject.housing < 1000000)
            {
                housingText.text = (spaceStation.settlementObject.housing / 1000).ToString("0.000k");
            }
            else
            {
                housingText.text = (spaceStation.settlementObject.housing / 1000000).ToString("0.000m");
            }

            if (spaceStation.settlementObject.population < 1000)
            {
                populationText.text = (spaceStation.settlementObject.population).ToString("0");
            }
            else if (spaceStation.settlementObject.population < 1000000)
            {
                populationText.text = (spaceStation.settlementObject.population / 1000).ToString("0.000k");
            }
            else
            {
                populationText.text = (spaceStation.settlementObject.population / 1000000).ToString("0.000m");
            }

            securityText.text = spaceStation.settlementObject.security.ToString();
        }
        else
        {
            nameText.text = "";
            foodText.text = "";
            energyText.text = "";
            securityText.text = "";
            prosperityText.text = "";
            housingText.text = "";
            populationText.text = "";
            buildingUI.spaceStation = null;
        }
    }

    public void Dock()
    {
        GameManager.instance.Save();
        //SceneManager.LoadScene(spaceStation.buildIndexOfScene);
        Time.timeScale = 1f;
    }

    public void Build()
    {
        CloseUI();

        if (buildingUI.gameObject.activeSelf)
        {
            buildingUI.gameObject.SetActive(false);
            buildingUI.spaceStation = null;
        } 
        else
        {
            buildingUI.gameObject.SetActive(true);
            buildingUI.spaceStation = spaceStation;
        }
    }

    public void Recruitment()
    {
        CloseUI();

        if (rectuitmentUI.activeSelf)
        {
            rectuitmentUI.SetActive(false);
        }
        else
        {
            rectuitmentUI.SetActive(true);
            rectuitmentUI.GetComponentInChildren<RecruitmentUI>().UpdateUI();
            rectuitmentUI.GetComponentInChildren<PilotRecruitmentUI>().UpdateUI();
        }
    }

    public void OpenStore()
    {
        CloseUI();

        spaceStation.GetComponent<SettlementTrader>().UpdateSlots();

        if (shopUI.activeSelf)
        {
            shopUI.SetActive(false);
        }
        else
        {
            shopUI.SetActive(true);
        }
    }

    public void Leave()
    {
        //Set in PlayerMap
        camPan.isMovable = true;
        crz.isMovable = true;

        CloseUI();

        spaceStation = null;

        cruiserCam.enabled = false;
        fighterCam.enabled = false;

        if (this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
            Time.timeScale = 1f;
            //interactText.SetActive(true);
            //HUD.SetActive(true);
            //buildingUI.gameObject.SetActive(false);
            //buildingUI.spaceStation = null;
        }
    }

    public void CloseUI()
    {
        if (shopUI.activeSelf)
        {
            shopUI.SetActive(false);
        }

        if (buildingUI.gameObject.activeSelf)
        {
            buildingUI.gameObject.SetActive(false);
        }

        if (rectuitmentUI.activeSelf)
        {
            rectuitmentUI.SetActive(false);
        }

        if (shipyardUI.activeSelf)
        {
            shipyardUI.SetActive(false);
            shipyardRenderCam.SetActive(false);
        }

        if (mechanicUI.gameObject.activeSelf)
        {
            mechanicUI.gameObject.SetActive(false);
            mechanicRenderCam.SetActive(false);
        }

        if (productionUI.gameObject.activeSelf)
        {
            productionUI.gameObject.SetActive(false);
        }

        if (bountyUI.gameObject.activeSelf)
        {
            bountyUI.gameObject.SetActive(false);
        }
    }

    public void BuyStation()
    {
        if(StatManager.instance.currentMoney > spaceStation.priceToPurchase)
        {
            spaceStation.GetComponent<SettlementInfo>().faction = Player.playerInstance.GetComponent<FleetFaction>().faction;
            spaceStation.settlementObject.isPlayerOwned = true;

            Ticker.Ticker.AddItem("You have purchased " + spaceStation.GetComponent<SettlementInfo>().Name + " station.");
        } 
        else
        {
            Ticker.Ticker.AddItem("You do not have enough money to purchase this station. You need " + (spaceStation.priceToPurchase - StatManager.instance.currentMoney) + " to purchase.");
        }
    }

    public void Shipyard()
    {
        CloseUI();

        if (shipyardUI.gameObject.activeSelf)
        {
            shipyardUI.gameObject.SetActive(false);
        } 
        else
        {
            shipyardUI.gameObject.SetActive(true);
            //shipyardUI.GetComponent<Shipyard>().UpdateShipyardUI(0);
            shipyardRenderCam.SetActive(true);
        }
    }

    public void Mechanic()
    {
        CloseUI();

        if (mechanicUI.gameObject.activeSelf)
        {
            mechanicUI.gameObject.SetActive(false);
        }
        else
        {
            mechanicUI.gameObject.SetActive(true);
            mechanicUI.GetComponent<MechanicUI>().UpdateInventoryModules();
            mechanicRenderCam.SetActive(true);
        }
    }

    public void Production()
    {
        CloseUI();

        if (productionUI.gameObject.activeSelf)
        {
            productionUI.gameObject.SetActive(false);
        }
        else
        {
            productionUI.gameObject.SetActive(true);
        }
    }

    public void Bounty()
    {
        CloseUI();

        if (bountyUI.gameObject.activeSelf)
        {
            bountyUI.gameObject.SetActive(false);
        }
        else
        {
            bountyUI.gameObject.SetActive(true);
        }
    }
}
