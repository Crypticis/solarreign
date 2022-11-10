//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class Station : Interactable
//{
//    public int buildIndexOfScene;
//    public SpaceStationUI spaceStationUI;
//    SpaceStation spaceStation;

//    private void Start()
//    {
//        spaceStation = GetComponent<SpaceStation>();
//    }

//    public override void Interact(PlayerController playerController)
//    {
//        spaceStationUI = playerController.GetComponent<Player>().stationUI.GetComponent<SpaceStationUI>();

//        if (playerController.GetComponent<Player>().stationUI.activeSelf)
//        {
//            playerController.GetComponent<Player>().stationUI.SetActive(false);
//            playerController.GetComponent<Player>().hud.SetActive(true);
//            playerController.GetComponent<Player>().interactText.SetActive(true);
//            Time.timeScale = 1f;
//            spaceStationUI.spaceStation = null;
//            spaceStationUI.station = null;
//            spaceStationUI.buildingUI.gameObject.SetActive(false);
//            Cursor.visible = false;
//        } 
//        else
//        {
//            playerController.GetComponent<Player>().stationUI.SetActive(true);
//            playerController.GetComponent<Player>().hud.SetActive(false);
//            playerController.GetComponent<Player>().interactText.SetActive(false);
//            Time.timeScale = 0f;
//            spaceStationUI.spaceStation = spaceStation;
//            spaceStationUI.station = this;
//            Cursor.visible = true;
//        }
//    }

//    public void Dock()
//    {
//        Debug.Log("Starting scene load");
//        SceneManager.LoadScene(buildIndexOfScene);
//    }

//    public void CloseMenu()
//    {
//        //stationUI.SetActive(false);
//        //Time.timeScale = 1f;
//    }

//    private void OnTriggerExit(Collider other)
//    {
//        //if (other.GetComponent<Player>())
//        //{
//        //    stationUI.SetActive(false);
//        //}
//    }
//}
