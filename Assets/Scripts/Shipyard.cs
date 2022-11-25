using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shipyard : MonoBehaviour
{
    public static UnityAction onShipChanged;

    [Header("Functionality")]

    [SerializeField] SpaceStationUI stationUI;
    [SerializeField] Transform buttonRoot;
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] PlayerInfoObject playerInfo;
    public List<ShipyardSlot> shipyardSlots = new();

    [SerializeField] NPCDatabaseObject database;

    [Header("UI")]

    [SerializeField] RenderTexture shipRawImage;
    [SerializeField] Transform shipSpawnRoot;
    [SerializeField] Button purchaseButton;
    [SerializeField] Button flyButton;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text priceText;
    [SerializeField] TMP_Text descText;

    private Transform spawnedShipModel;

    //Initial setup of ships from list
    public void SetupShipyardUI()
    {
        for (int i = 0; i < shipyardSlots.Count; i++)
        {
            Destroy(shipyardSlots[i].button.gameObject);
        }

        shipyardSlots.Clear();

        for (int i = 0; i < database.shipinfos.Length; i++)
        {
            var j = i;

            GameObject shipyardButton = Instantiate(buttonPrefab, buttonRoot);
            shipyardButton.GetComponent<Button>().onClick.AddListener(() => UpdateShipyardUI(j));

            var shipyardSlot = new ShipyardSlot
            {
                shipInfo = database.shipinfos[i],
                cost = (database.shipinfos[i].baseCost * stationUI.spaceStation.settlementObject.ShipPriceModifierPerSettlementType(stationUI.spaceStation.settlementObject.stationType)),
                isOwned = database.shipinfos[i].isOwned,
                shipType = database.shipinfos[i].shipType,
                button = shipyardButton,
            };

            shipyardButton.GetComponentInChildren<TMP_Text>().text = /*"[" + database.shipinfos[i].shipType + "] " + */database.shipinfos[i].Name;

            shipyardSlots.Add(shipyardSlot);
        }
    }

    // Sorts by ship type
    public void SetupShipyardUI(int shipTypeIndex)
    {
        for (int i = 0; i < shipyardSlots.Count; i++)
        {
            Destroy(shipyardSlots[i].button.gameObject);
        }

        shipyardSlots.Clear();

        for (int i = 0; i < database.shipinfos.Length; i++)
        {
            if (((int)database.shipinfos[i].shipType) != shipTypeIndex)
            {
                continue;
            }

            var j = i;

            GameObject shipyardButton = Instantiate(buttonPrefab, buttonRoot);
            shipyardButton.GetComponent<Button>().onClick.AddListener(() => UpdateShipyardUI(j));

            var shipyardSlot = new ShipyardSlot
            {
                shipInfo = database.shipinfos[i],
                cost = (database.shipinfos[i].baseCost * stationUI.spaceStation.settlementObject.ShipPriceModifierPerSettlementType(stationUI.spaceStation.settlementObject.stationType)),
                isOwned = database.shipinfos[i].isOwned,
                shipType = database.shipinfos[i].shipType,
                button = shipyardButton,
            };

            shipyardButton.GetComponentInChildren<TMP_Text>().text = /*"[" + database.shipinfos[i].shipType + "] " + */database.shipinfos[i].Name;

            shipyardSlots.Add(shipyardSlot);
        }
    }

    // Sorts by ship faction
    public void SetupShipyardUI(string factionName)
    {
        for (int i = 0; i < shipyardSlots.Count; i++)
        {
            Destroy(shipyardSlots[i].button.gameObject);
        }

        shipyardSlots.Clear();

        for (int i = 0; i < database.shipinfos.Length; i++)
        {
            if ((database.shipinfos[i].faction.name) != factionName)
                continue;

            var j = i;

            GameObject shipyardButton = Instantiate(buttonPrefab, buttonRoot);
            shipyardButton.GetComponent<Button>().onClick.AddListener(() => UpdateShipyardUI(j));

            var shipyardSlot = new ShipyardSlot
            {
                shipInfo = database.shipinfos[i],
                cost = (database.shipinfos[i].baseCost * stationUI.spaceStation.settlementObject.ShipPriceModifierPerSettlementType(stationUI.spaceStation.settlementObject.stationType)),
                isOwned = database.shipinfos[i].isOwned,
                shipType = database.shipinfos[i].shipType,
                button = shipyardButton,
            };

            shipyardButton.GetComponentInChildren<TMP_Text>().text = /*"[" + database.shipinfos[i].shipType + "] " + */database.shipinfos[i].Name;

            shipyardSlots.Add(shipyardSlot);
        }
    }

    // Selects a new ship to display in the shipyard overview
    public void UpdateShipyardUI(int index)
    {
        // Visuals

        nameText.text = shipyardSlots[index].shipInfo.Name;
        priceText.text = shipyardSlots[index].cost.ToString();
        descText.text = shipyardSlots[index].shipInfo.description.ToString();

        if (spawnedShipModel)
            Destroy(spawnedShipModel.gameObject);
        
        spawnedShipModel = Instantiate(database.GetShip[shipyardSlots[index].shipInfo.ID], shipSpawnRoot).transform;

        switch (shipyardSlots[index].shipInfo.shipType)
        {
            case ShipType.corvette:

                spawnedShipModel.gameObject.LeanScale(new Vector3(.45f, .45f, .45f), 0).setIgnoreTimeScale(true);

                break;
            case ShipType.destroyer:

                spawnedShipModel.gameObject.LeanScale(new Vector3(.35f, .35f, .35f), 0).setIgnoreTimeScale(true);

                break;
            case ShipType.cruiser:

                spawnedShipModel.gameObject.LeanScale(new Vector3(.25f, .25f, .25f), 0).setIgnoreTimeScale(true);

                break;
            case ShipType.battlecruiser:

                spawnedShipModel.gameObject.LeanScale(new Vector3(.15f, .15f, .15f), 0).setIgnoreTimeScale(true);

                break;
            case ShipType.battleship:

                spawnedShipModel.gameObject.LeanScale(new Vector3(.1f, .1f, .1f), 0).setIgnoreTimeScale(true);

                break;
            case ShipType.dreadnought:

                spawnedShipModel.gameObject.LeanScale(new Vector3(.05f, .05f, .05f), 0).setIgnoreTimeScale(true);

                break;
            default:
                break;
        }

        //Functionality

        purchaseButton.onClick.RemoveAllListeners();
        flyButton.onClick.RemoveAllListeners();

        purchaseButton.onClick.AddListener(() => PurchaseShip(index));
        flyButton.onClick.AddListener(() => FlyShip(index));

        if (shipyardSlots[index].isOwned == true)
        {
            purchaseButton.interactable = false;
            flyButton.interactable = true;
        }
        else
        {
            purchaseButton.interactable = true;
            flyButton.interactable = false;
        }
    }

    //Makes the ship owned
    public void PurchaseShip(int index)
    {
        if(StatManager.instance.currentMoney >= shipyardSlots[index].cost)
        {
            shipyardSlots[index].isOwned = true;
            StatManager.instance.currentMoney -= shipyardSlots[index].cost;
            Ticker.Ticker.AddItem("You have purchased " + shipyardSlots[index].shipType);
        }
        else
        {
            var temp = (shipyardSlots[index].cost - StatManager.instance.currentMoney);
            Ticker.Ticker.AddItem("You do not have enough money to purchase this ship. You need " + temp + " more.");
        }

        UpdateShipyardUI(index);
    }

    //Makes the player fly the ship.
    public void FlyShip(int index)
    {
        playerInfo.shipType = shipyardSlots[index].shipType;
        playerInfo.shipID = shipyardSlots[index].shipInfo.ID;

        Ticker.Ticker.AddItem("You are now flying the " + shipyardSlots[index].shipType + ".");

        onShipChanged.Invoke();

        UpdateShipyardUI(index);
    }

    [System.Serializable]
    public class ShipyardSlot
    {
        public ShipInfoObject shipInfo;
        public float cost;
        public bool isOwned;

        public ShipType shipType;

        public GameObject button;
    }
}
