using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingUI buildingUI;
    public TMP_Text levelText;
    public int slotIndex;
    public float costToUpgrade;

    void Update()
    {
        if (buildingUI.spaceStation)
        {
            if (levelText.text != buildingUI.spaceStation.settlementObject.buildingLevels[slotIndex].ToString())
            {
                levelText.text = buildingUI.spaceStation.settlementObject.buildingLevels[slotIndex].ToString();
            }
        } 
        else
        {
            levelText.text = "0";
        }
    }

    public void Upgrade()
    {
        buildingUI.UpgradeBuilding(slotIndex);
    }
}

