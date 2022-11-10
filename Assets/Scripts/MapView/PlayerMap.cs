 using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMap : PlayerController
{
    public PlayerInfoObject playerInfo;

    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    public Interactable interactable;

    public ClickToMove ClickToMove;

    public GameObject locationUI;
    public GameObject enemyUI;
    public GameObject allyUI;
    public NavMeshAgent navMeshAgent;

    public static PlayerMap playerMap;
    public PlayerFleet fleet;
    public TMP_Text nameText;

    public MeshFilter meshFilter;
    public GameObject[] ships;

    private void Awake()
    {
        playerMap = this;
    }

    public void Start()
    {
        nameText.text = playerInfo.Name;
        UpdateModel();
    }

    public void UpdateModel()
    {
        //Debug.Log("Model Updating");

        if (playerInfo.shipType == ShipType.fighter)
        {
            meshFilter.sharedMesh = ships[0].GetComponent<MeshFilter>().sharedMesh;
        }
        else if (playerInfo.shipType == ShipType.heavyFighter)
        {
            meshFilter.sharedMesh = ships[1].GetComponent<MeshFilter>().sharedMesh;
        }
        else if (playerInfo.shipType == ShipType.bomber)
        {
            meshFilter.sharedMesh = ships[2].GetComponent<MeshFilter>().sharedMesh;
        }
        else if (playerInfo.shipType == ShipType.cruiser)
        {
            meshFilter.sharedMesh = ships[3].GetComponent<MeshFilter>().sharedMesh;
        }

        List<float> size = new List<float>();

        size.Add((meshFilter.GetComponent<MeshFilter>().transform.localScale.z * meshFilter.GetComponent<MeshFilter>().mesh.bounds.size.z));
        size.Add((meshFilter.GetComponent<MeshFilter>().transform.localScale.x * meshFilter.GetComponent<MeshFilter>().mesh.bounds.size.x));
        size.Add((meshFilter.GetComponent<MeshFilter>().transform.localScale.y * meshFilter.GetComponent<MeshFilter>().mesh.bounds.size.y));

        size = size.OrderBy(i => i).ToList();

        float objectSize = size[0] / 5f;

        meshFilter.GetComponent<MeshFilter>().transform.localScale /= objectSize;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            if (other.gameObject.transform == ClickToMove.travelTarget && locationUI.activeSelf == false)
            {
                interactable = other.GetComponent<Interactable>();
                interactable.Interact(this);

                ClickToMove.travelTarget = null;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //CloseUI();
    }

    public void ShowLocationUI(SpaceStation spaceStation)
    {
        locationUI.GetComponent<SpaceStationUI>().spaceStation = spaceStation;
        locationUI.SetActive(true);
        ClickToMove.enabled = false;
        navMeshAgent.isStopped = true;


        locationUI.GetComponent<SpaceStationUI>().camPan.isMovable = false;
        locationUI.GetComponent<SpaceStationUI>().crz.isMovable = false;

        

        Time.timeScale = 0f;
    }

    public void ShowEnemyUI()
    {
        //enemyUI.SetActive(true);
        ClickToMove.enabled = false;
        navMeshAgent.isStopped = true;
        //Time.timeScale = 0f;
    }

    public void ShowAllyUI()
    {
        navMeshAgent.isStopped = true;
        Time.timeScale = 0f;
    }

    public void CloseUI()
    {
        ClickToMove.enabled = true;
        Time.timeScale = 1f;
        if (locationUI.activeSelf)
        {
            locationUI.SetActive(false);
        }

        //if (enemyUI.activeSelf)
        //{
        //    enemyUI.SetActive(false);
        //}

        //if (allyUI.activeSelf)
        //{
        //    allyUI.SetActive(false);
        //}

        //dialogueUI.CloseDialogueBox();
        navMeshAgent.isStopped = false;
        ClickToMove.travelTarget = null;
    }
}
