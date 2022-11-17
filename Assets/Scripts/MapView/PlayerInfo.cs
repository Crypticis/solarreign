using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public PlayerInfoObject playerInfoObject;

    public GameObject fighter;
    public GameObject heavyFighter;
    public GameObject bomber;
    public GameObject cruiser;
    public GameObject camObject;

    public HealthUI healthUI;

    DamageHandler playerHealth;
    public FLFlight.ShipPhysics shipPhysics;

    public ShipDefaultValues[] shipDefaults;

    public void Awake()
    {
        if (playerInfoObject.shipType == ShipType.fighter)
        {
            fighter.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, .75f, -4);
            shipPhysics.linearForce = shipDefaults[0].linearForce;
            shipPhysics.angularForce = shipDefaults[0].angularForce;
            healthUI.playerMeshFilter = fighter.GetComponentInChildren<MeshFilter>();
        }
        else if (playerInfoObject.shipType == ShipType.heavyFighter)
        {
            heavyFighter.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, .75f, -4);
            shipPhysics.linearForce = shipDefaults[1].linearForce;
            shipPhysics.angularForce = shipDefaults[1].angularForce;
            healthUI.playerMeshFilter = heavyFighter.GetComponentInChildren<MeshFilter>();
        }
        else if (playerInfoObject.shipType == ShipType.bomber)
        {
            bomber.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, .75f, -4);
            shipPhysics.linearForce = shipDefaults[2].linearForce;
            shipPhysics.angularForce = shipDefaults[2].angularForce;
            healthUI.playerMeshFilter = bomber.GetComponentInChildren<MeshFilter>();
        }
        else if (playerInfoObject.shipType == ShipType.cruiser)
        {
            cruiser.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, 1.5f, -7);
            shipPhysics.linearForce = shipDefaults[3].linearForce;
            shipPhysics.angularForce = shipDefaults[3].angularForce;
            healthUI.playerMeshFilter = cruiser.GetComponentInChildren<MeshFilter>();
        }
    }

    private void Update()
    {
        Shipyard.onShipChanged += UpdateShip;
    }

    public void UpdateShip()
    {
        if (playerInfoObject.shipType == ShipType.fighter)
        {
            fighter.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, .75f, -4);
            shipPhysics.linearForce = shipDefaults[0].linearForce;
            shipPhysics.angularForce = shipDefaults[0].angularForce;
            healthUI.playerMeshFilter = fighter.GetComponentInChildren<MeshFilter>();
        }
        else if (playerInfoObject.shipType == ShipType.heavyFighter)
        {
            heavyFighter.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, .75f, -4);
            shipPhysics.linearForce = shipDefaults[1].linearForce;
            shipPhysics.angularForce = shipDefaults[1].angularForce;
            healthUI.playerMeshFilter = heavyFighter.GetComponentInChildren<MeshFilter>();
        }
        else if (playerInfoObject.shipType == ShipType.bomber)
        {
            bomber.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, .75f, -4);
            shipPhysics.linearForce = shipDefaults[2].linearForce;
            shipPhysics.angularForce = shipDefaults[2].angularForce;
            healthUI.playerMeshFilter = bomber.GetComponentInChildren<MeshFilter>();
        }
        else if (playerInfoObject.shipType == ShipType.cruiser)
        {
            cruiser.SetActive(true);
            camObject.transform.localPosition = new Vector3(0, 1.5f, -7);
            shipPhysics.linearForce = shipDefaults[3].linearForce;
            shipPhysics.angularForce = shipDefaults[3].angularForce;
            healthUI.playerMeshFilter = cruiser.GetComponentInChildren<MeshFilter>();
        }
    }

    [System.Serializable]
    public struct ShipDefaultValues
    {
        public ShipType shipType;
        public Vector3 linearForce;
        public Vector3 angularForce;
    }
}
