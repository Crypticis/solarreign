using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    public PlayerInfoObject playerInfoObject;

    //public GameObject fighter;
    //public GameObject heavyFighter;
    //public GameObject bomber;
    //public GameObject cruiser;
    public GameObject camObject;

    public Transform shipRoot;

    public HealthUI healthUI;

    DamageHandler playerHealth;
    public FLFlight.ShipPhysics shipPhysics;

    public ShipDefaultValues[] shipDefaults;

    public void Start()
    {
        UpdateShip();
    }

    private void Update()
    {
        Shipyard.onShipChanged += UpdateShip;
    }

    public void UpdateShip()
    {
        for (int i = 0; i < shipRoot.childCount; i++)
        {
            Destroy(shipRoot.GetChild(i).gameObject);
        }

        GameObject ship = Instantiate(GameManager.instance.database.GetShip[playerInfoObject.shipID], shipRoot.transform);

        var missiles = ship.GetComponentsInChildren<AAHardpoint>();

        for (int i = 0; i < missiles.Length; i++)
        {
            missiles[i].ownShip = transform;
        }

        switch (playerInfoObject.shipType)
        {
            case ShipType.corvette:

            //    fighter.SetActive(true);
                camObject.transform.localPosition = new Vector3(0, .75f, -4);
                shipPhysics.linearForce = shipDefaults[0].linearForce;
                shipPhysics.angularForce = shipDefaults[0].angularForce;
                //healthUI.playerMeshFilter = fighter.GetComponentInChildren<MeshFilter>();

                break;
            case ShipType.destroyer:

                //bomber.SetActive(true);
                camObject.transform.localPosition = new Vector3(0, 1f, -5);
                shipPhysics.linearForce = shipDefaults[1].linearForce;
                shipPhysics.angularForce = shipDefaults[1].angularForce;
                //healthUI.playerMeshFilter = bomber.GetComponentInChildren<MeshFilter>();

                break;
            case ShipType.cruiser:

                //cruiser.SetActive(true);
                camObject.transform.localPosition = new Vector3(0, 1.5f, -7);
                shipPhysics.linearForce = shipDefaults[2].linearForce;
                shipPhysics.angularForce = shipDefaults[2].angularForce;
                //healthUI.playerMeshFilter = cruiser.GetComponentInChildren<MeshFilter>();

                break;
            case ShipType.battlecruiser:

                //heavyFighter.SetActive(true);
                camObject.transform.localPosition = new Vector3(0, 2.5f, -9);
                shipPhysics.linearForce = shipDefaults[3].linearForce;
                shipPhysics.angularForce = shipDefaults[3].angularForce;
                //healthUI.playerMeshFilter = heavyFighter.GetComponentInChildren<MeshFilter>();

                break;
            case ShipType.battleship:

                camObject.transform.localPosition = new Vector3(0, 3.5f, -11);
                shipPhysics.linearForce = shipDefaults[4].linearForce;
                shipPhysics.angularForce = shipDefaults[4].angularForce;

                break;
            case ShipType.dreadnought:

                camObject.transform.localPosition = new Vector3(0, 4.5f, -13);
                shipPhysics.linearForce = shipDefaults[5].linearForce;
                shipPhysics.angularForce = shipDefaults[5].angularForce;

                break;
            default:
                break;
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
