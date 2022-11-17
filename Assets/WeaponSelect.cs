using GNB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] PlayerInfoObject playerInfoObject;
    //public ShipInfo[] shipInfos;

    [SerializeField] Transform[] weaponRoots;

    public WeaponGroup activeWeaponGroup;

    [SerializeField] WeaponGroup[] weaponGroups = new WeaponGroup[4];

    int selectIndex = 0;

    [SerializeField] Sprite[] weaponSprites;

    [SerializeField] Image[] weaponSelectionIcons;

    private void Start()
    {
        UpdateWeapons();
        IncrementSelectedIndex();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            IncrementSelectedIndex();
        }

        MechanicUI.onShipModuleChange += UpdateWeapons;
    }

    //increases selected index which selects weapon group. 
    public void IncrementSelectedIndex()
    {
        //UpdateWeapons();

        if (selectIndex + 1 <= weaponGroups.Length - 1)
        {
            selectIndex++;

            if(weaponGroups[selectIndex].weapons.Count == 0)
            {
                IncrementSelectedIndex();
                return;
            }

            weaponSelectionIcons[0].sprite = weaponSprites[selectIndex - 1];
            weaponSelectionIcons[1].sprite = weaponSprites[selectIndex];

            if(selectIndex + 1 <= weaponGroups.Length - 1)
                weaponSelectionIcons[2].sprite = weaponSprites[selectIndex + 1];
            else
                weaponSelectionIcons[2].sprite = weaponSprites[0];
        }
        else
        {
            selectIndex = 0;

            if (weaponGroups[0].weapons.Count == 0)
            {
                IncrementSelectedIndex();
                return;
            }

            weaponSelectionIcons[0].sprite = weaponSprites[weaponGroups.Length - 1];
            weaponSelectionIcons[1].sprite = weaponSprites[selectIndex];
            weaponSelectionIcons[2].sprite = weaponSprites[selectIndex + 1];

            if(weaponGroups[weaponGroups.Length - 1].weapons.Count == 0)
            {
                weaponSelectionIcons[0].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, .25f);
            }
            else
            {
                weaponSelectionIcons[0].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, 1f);
            }

            if (weaponGroups[selectIndex + 1].weapons.Count == 0)
            {
                weaponSelectionIcons[2].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, .25f);
            }
            else
            {
                weaponSelectionIcons[2].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, 1f);
            }
        }

        GrowSprites();
        SelectWeaponGroup(selectIndex);
        activeWeaponGroup = weaponGroups[selectIndex];
    }

    void GrowSprites()
    {
        for (int i = 0; i < weaponSelectionIcons.Length; i++)
        {
            LeanTween.scale(weaponSelectionIcons[i].gameObject, new Vector3(1.1f, 1.1f, 1.1f), .1f).setEaseInBounce();
        }

        Invoke("ShrinkSprite", .25f);
    }

    void ShrinkSprite()
    {
        for (int i = 0; i < weaponSelectionIcons.Length; i++)
        {
            LeanTween.scale(weaponSelectionIcons[i].gameObject, new Vector3(1, 1, 1), .1f);
        }
    }

    //Updates the available weapons by checking the child of the weapon transform on ships. 
    public void UpdateWeapons()
    {
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            weaponGroups[i].weapons.Clear();
        }

        switch (playerInfoObject.shipType)
        {
            case ShipType.fighter:

                for (int i = 0; i < weaponRoots[0].childCount; i++)
                {
                    if(weaponRoots[0].GetChild(i).GetComponent<Gun>() && weaponRoots[0].GetChild(i).GetComponent<Gun>().enabled && !weaponRoots[0].GetChild(i).GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(weaponRoots[0].GetChild(i).gameObject);
                    }

                    if (weaponRoots[0].GetChild(i).GetComponent<AAHardpoint>() && weaponRoots[0].GetChild(i).GetComponent<AAHardpoint>().enabled)
                    {
                        weaponGroups[1].weapons.Add(weaponRoots[0].GetChild(i).gameObject);
                    }

                    if (weaponRoots[0].GetChild(i).GetComponent<Laser>() && weaponRoots[0].GetChild(i).GetComponent<Laser>().enabled)
                    {
                        weaponGroups[2].weapons.Add(weaponRoots[0].GetChild(i).gameObject);
                    }

                    if (weaponRoots[0].GetChild(i).GetComponent<MiningDrillLauncher>() && weaponRoots[0].GetChild(i).GetComponent<MiningDrillLauncher>().enabled)
                    {
                        weaponGroups[3].weapons.Add(weaponRoots[0].GetChild(i).gameObject);
                    }
                }

                break;

            case ShipType.heavyFighter:

                for (int i = 0; i < weaponRoots[1].childCount; i++)
                {
                    if (weaponRoots[1].GetChild(i).GetComponent<Gun>() && weaponRoots[1].GetChild(i).GetComponent<Gun>().enabled && !weaponRoots[1].GetChild(i).GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(weaponRoots[1].GetChild(i).gameObject);
                    }

                    if (weaponRoots[1].GetChild(i).GetComponent<AAHardpoint>() && weaponRoots[1].GetChild(i).GetComponent<AAHardpoint>().enabled)
                    {
                        weaponGroups[1].weapons.Add(weaponRoots[1].GetChild(i).gameObject);
                    }

                    if (weaponRoots[1].GetChild(i).GetComponent<Laser>() && weaponRoots[1].GetChild(i).GetComponent<Laser>().enabled)
                    {
                        weaponGroups[2].weapons.Add(weaponRoots[1].GetChild(i).gameObject);
                    }

                    if (weaponRoots[1].GetChild(i).GetComponent<MiningDrillLauncher>() && weaponRoots[1].GetChild(i).GetComponent<MiningDrillLauncher>().enabled)
                    {
                        weaponGroups[3].weapons.Add(weaponRoots[1].GetChild(i).gameObject);
                    }
                }

                break;

            case ShipType.cruiser:

                Gun[] guns = weaponRoots[2].GetComponentsInChildren<Gun>();

                for (int i = 0; i < guns.Length; i++)
                {
                    if (guns[i].enabled && !guns[i].GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(guns[i].gameObject);
                    }
                }

                AAHardpoint[] hardpoints = weaponRoots[2].GetComponentsInChildren<AAHardpoint>();

                for (int i = 0; i < hardpoints.Length; i++)
                {
                    if (hardpoints[i].enabled)
                    {
                        weaponGroups[1].weapons.Add(hardpoints[i].gameObject);
                    }
                }

                Laser[] lasers = weaponRoots[2].GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    if (lasers[i].enabled)
                    {
                        weaponGroups[2].weapons.Add(lasers[i].gameObject);
                    }
                }

                MiningDrillLauncher[] miningdrills = weaponRoots[2].GetComponentsInChildren<MiningDrillLauncher>();

                for (int i = 0; i < miningdrills.Length; i++)
                {
                    if (miningdrills[i].enabled)
                    {
                        weaponGroups[3].weapons.Add(miningdrills[i].gameObject);
                    }
                }

                break;

            case ShipType.bomber:

                for (int i = 0; i < weaponRoots[1].childCount; i++)
                {
                    if (weaponRoots[3].GetChild(i).GetComponent<Gun>() && weaponRoots[3].GetChild(i).GetComponent<Gun>().enabled && !weaponRoots[3].GetChild(i).GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(weaponRoots[3].GetChild(i).gameObject);
                    }

                    if (weaponRoots[3].GetChild(i).GetComponent<AAHardpoint>() && weaponRoots[3].GetChild(i).GetComponent<AAHardpoint>().enabled)
                    {
                        weaponGroups[1].weapons.Add(weaponRoots[3].GetChild(i).gameObject);
                    }

                    if (weaponRoots[3].GetChild(i).GetComponent<Laser>() && weaponRoots[3].GetChild(i).GetComponent<Laser>().enabled)
                    {
                        weaponGroups[2].weapons.Add(weaponRoots[3].GetChild(i).gameObject);
                    }

                    if (weaponRoots[3].GetChild(i).GetComponent<MiningDrillLauncher>() && weaponRoots[3].GetChild(i).GetComponent<MiningDrillLauncher>().enabled)
                    {
                        weaponGroups[3].weapons.Add(weaponRoots[3].GetChild(i).gameObject);
                    }
                }

                break;
        }
    }

    //Recieves index and disables all weapons which are not of the selected indexes weapon group.
    public void SelectWeaponGroup(int index)
    {
        for (int i = 0; i < weaponGroups.Length; i++)
        {
            if(i != index)
            {
                //Disables all weapons which are not in the group.
                foreach (var weapon in weaponGroups[i].weapons)
                {
                    if(weapon.GetComponentInChildren<Gun>())
                    {
                        weapon.GetComponentInChildren<Gun>().enabled = false;
                    }

                    if (weapon.GetComponentInChildren<AAHardpoint>())
                    {
                        weapon.GetComponentInChildren<AAHardpoint>().enabled = false;
                    }

                    if (weapon.GetComponent<Laser>())
                    {
                        weapon.GetComponent<Laser>().enabled = false;
                    }

                    if (weapon.GetComponent<MiningDrillLauncher>())
                    {
                        weapon.GetComponent<MiningDrillLauncher>().enabled = false;
                    }
                }
            }
            else
            {
                //Enables the selected weapon group.
                foreach (var weapon in weaponGroups[i].weapons)
                {
                    if (weapon.GetComponentInChildren<Gun>())
                    {
                        weapon.GetComponentInChildren<Gun>().enabled = true;
                    }

                    if (weapon.GetComponentInChildren<AAHardpoint>())
                    {
                        weapon.GetComponentInChildren<AAHardpoint>().enabled = true;
                    }

                    if (weapon.GetComponent<Laser>())
                    {
                        weapon.GetComponent<Laser>().enabled = true;
                    }

                    if (weapon.GetComponent<MiningDrillLauncher>())
                    {
                        weapon.GetComponent<MiningDrillLauncher>().enabled = true;
                    }
                }
            }
        }
    }

    [System.Serializable]
    public class WeaponGroup
    {
        public List<GameObject> weapons = new List<GameObject>();
    }
}
