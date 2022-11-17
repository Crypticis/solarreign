using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUI : MonoBehaviour
{
    public SpaceStation spaceStation;

    public Building[] buildings;

    public void Update()
    {
        for (int i = 0; i < buildings.Length; i++)
        {
            if(spaceStation.settlementObject.upgrading[i] == true)
            {
                buildings[i].GetComponentInChildren<Button>().interactable = false;
                buildings[i].GetComponentInChildren<Slider>().value = Mathf.Abs((spaceStation.settlementObject.buildTimers[i] - spaceStation.defaultTimers[i]) / spaceStation.defaultTimers[i]);
            }
            else
            {
                buildings[i].GetComponentInChildren<Button>().interactable = true;
                buildings[i].GetComponentInChildren<Slider>().value = 0;
            }
        }

        //buildings[0].GetComponentInChildren<Slider>().value = spaceStation.buildTimers[0];
        //buildings[1].GetComponentInChildren<Slider>().value = spaceStation.buildTimers[1];
        //buildings[2].GetComponentInChildren<Slider>().value = spaceStation.buildTimers[2];
        //buildings[3].GetComponentInChildren<Slider>().value = spaceStation.buildTimers[3];
        //buildings[4].GetComponentInChildren<Slider>().value = spaceStation.buildTimers[4];
    }

    public void UpgradeBuilding(int index)
    {
        if (spaceStation.settlementObject.buildingLevels[index] < 5 && StatManager.instance.currentMoney >= buildings[index].costToUpgrade)
        {
            spaceStation.settlementObject.upgrading[index] = true;
            spaceStation.settlementObject.buildTimers[index] = spaceStation.defaultTimers[spaceStation.settlementObject.buildingLevels[index] - 1];
            //spaceStation.buildingLevels[index]++;
            StatManager.instance.currentMoney -= buildings[index].costToUpgrade;
            Ticker.Ticker.AddItem("Started upgrade.");
        }
        else if (StatManager.instance.currentMoney <= buildings[index].costToUpgrade && spaceStation.settlementObject.buildingLevels[index] == 5)
        {
            Ticker.Ticker.AddItem("Not enough money and the building is max level.");
        }
        else if(StatManager.instance.currentMoney <= buildings[index].costToUpgrade)
        {
            Ticker.Ticker.AddItem("Not enough money.");
        }
        else
        {
            Ticker.Ticker.AddItem("Already max level.");
        }
    }
}
