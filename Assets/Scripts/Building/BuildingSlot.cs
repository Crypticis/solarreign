//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.UI;

//public class BuildingSlot : MonoBehaviour, IPointerExitHandler
//{
//    public Sprite[] sprites;
//    Image Icon;
//    public GameObject dropDownGameObject;
//    public TMP_Dropdown dropdown;
//    public Building building;
//    public float powerplantCost = 1000f;
//    public float taxcollectionCost = 1000f;
//    public float farmCost = 1000f;

//    public Player player;

//    private void Start()
//    {
//        Icon = transform.Find("Icon").GetComponent<Image>();
//        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
//    }

//    void Update()
//    {
//        if (building.currentBuilding == BuildingType.none)
//        {
//            Icon.sprite = sprites[0];
//        } else if (building.currentBuilding == BuildingType.powerplant)
//        {
//            Icon.sprite = sprites[1];
//        } else if (building.currentBuilding == BuildingType.taxcollection)
//        {
//            Icon.sprite = sprites[2];
//        } else if (building.currentBuilding == BuildingType.farm)
//        {
//            Icon.sprite = sprites[3];
//        }
//    }

//    public void ShowDropDown()
//    {
//        if (dropDownGameObject.activeSelf)
//        {
//            dropDownGameObject.SetActive(false);
//        }
//        else
//        {
//            dropDownGameObject.SetActive(true);
//        }
//    }

//    public void SelectItem()
//    {
//        if(dropdown.value == 0)
//        {
//            building.currentBuilding = BuildingType.none;
//        } 

//        if (dropdown.value == 1)
//        {
//            if (StatManager.instance.currentMoney >= powerplantCost)
//            {
//                building.currentBuilding = BuildingType.powerplant;
//                StatManager.instance.currentMoney -= powerplantCost;
//            }
//            else
//            {
//                Ticker.Ticker.AddItem((string.Format("Not enough money for Power Plant. Only have ${0}. Need ${1}.", (StatManager.instance.currentMoney).ToString(), (powerplantCost).ToString())), 5f, Color.white);
//            }
//        }

//        if (dropdown.value == 2)
//        {
//            if (StatManager.instance.currentMoney >= taxcollectionCost)
//            {
//                building.currentBuilding = BuildingType.taxcollection;
//                StatManager.instance.currentMoney -= taxcollectionCost;
//            }
//            else
//            {
//                Ticker.Ticker.AddItem((string.Format("Not enough money for Tax Collection Office. Only have ${0}. Need ${1}.", (StatManager.instance.currentMoney).ToString(), (taxcollectionCost).ToString())), 5f, Color.white);
//            }
//        }

//        if (dropdown.value == 3)
//        {
//            if (StatManager.instance.currentMoney >= farmCost)
//            {
//                building.currentBuilding = BuildingType.farm;
//                StatManager.instance.currentMoney -= farmCost;
//            }
//            else
//            {
//                Ticker.Ticker.AddItem((string.Format("Not enough money for Farm. Only have ${0}. Need ${1}.", (StatManager.instance.currentMoney).ToString(), (farmCost).ToString())), 5f, Color.white);
//            }
//        }
//    }

//    public void OnPointerExit(PointerEventData eventData)
//    {
//        ((IPointerExitHandler)dropdown).OnPointerExit(eventData);
//    }
//}
