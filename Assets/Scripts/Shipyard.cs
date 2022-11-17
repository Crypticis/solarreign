using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Shipyard : MonoBehaviour
{
    public static Shipyard instance;
    public static UnityAction onShipChanged;


    public GameObject shipModel;
    public RawImage shipDisplay;
    public RenderTexture fighter, cruiser;

    public ShipyardSlot[] shipyardSlots;

    public Button purchaseButton;
    public Button flyButton;

    public PlayerInfoObject playerInfo;
    public TMP_Text nameText;
    public TMP_Text priceText;

    private void Awake()
    {
        instance = this;
    }

    public void UpdateShipyardUI(int index)
    {
        // Visuals
        shipModel.GetComponent<MeshFilter>().sharedMesh = shipyardSlots[index].shipPrefab.GetComponent<MeshFilter>().sharedMesh;

        nameText.text = shipyardSlots[index].shipType.ToString();
        priceText.text = shipyardSlots[index].cost.ToString();

        if (index == 3)
        {
            shipDisplay.texture = cruiser;
        }
        else
        {
            shipDisplay.texture = fighter;
        }

        //List<float> size = new List<float>();

        //size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.z * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.z));
        //size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.x * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.x));
        //size.Add((shipModel.GetComponent<MeshFilter>().transform.localScale.y * shipModel.GetComponent<MeshFilter>().mesh.bounds.size.y));

        //size = size.OrderBy(i => i).ToList();

        //float objectSize = size[0] / 2f;

        //shipModel.GetComponent<MeshFilter>().transform.localScale /= objectSize;

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

    public void FlyShip(int index)
    {
        playerInfo.shipType = shipyardSlots[index].shipType;
        Ticker.Ticker.AddItem("You are now flying the " + shipyardSlots[index].shipType + ".");

        //Player.playerInstance.UpdateModel();

        onShipChanged.Invoke();

        UpdateShipyardUI(index);
    }

    [System.Serializable]
    public struct ShipyardSlot
    {
        public GameObject shipPrefab;
        public float cost;
        public bool isOwned;

        public ShipType shipType;

    }
}
