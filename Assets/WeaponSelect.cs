using GNB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSelect : MonoBehaviour
{
    [SerializeField] PlayerInfoObject playerInfoObject;
    //public ShipInfo[] shipInfos;

    [SerializeField] Transform shipTransform;

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

        for (int i = 0; i < weaponGroups[2].weapons.Count; i++)
        {
            weaponGroups[2].weapons[i].GetComponent<Laser>().DisableLaser();
            weaponGroups[2].weapons[i].GetComponent<Laser>().isFiring = false;
        }

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
                weaponSelectionIcons[0].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, .1f);
            }
            else
            {
                weaponSelectionIcons[0].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, 1f);
            }

            if (weaponGroups[selectIndex + 1].weapons.Count == 0)
            {
                weaponSelectionIcons[2].color = new Color(weaponSelectionIcons[2].color.r, weaponSelectionIcons[2].color.g, weaponSelectionIcons[2].color.b, .1f);
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
            case ShipType.corvette:

                Gun[] guns = shipTransform.GetComponentsInChildren<Gun>();

                for (int i = 0; i < guns.Length; i++)
                {
                    if (guns[i].gameObject.activeInHierarchy && !guns[i].GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(guns[i].gameObject);
                    }
                }

                AAHardpoint[] hardpoints = shipTransform.GetComponentsInChildren<AAHardpoint>();

                for (int i = 0; i < hardpoints.Length; i++)
                {
                    if (hardpoints[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[1].weapons.Add(hardpoints[i].gameObject);
                    }
                }

                Laser[] lasers = shipTransform.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers.Length; i++)
                {
                    if (lasers[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[2].weapons.Add(lasers[i].gameObject);
                    }
                }

                MiningDrillLauncher[] miningdrills = shipTransform.GetComponentsInChildren<MiningDrillLauncher>();

                for (int i = 0; i < miningdrills.Length; i++)
                {
                    if (miningdrills[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[3].weapons.Add(miningdrills[i].gameObject);
                    }
                }

                break;

            case ShipType.destroyer:

                Gun[] guns1 = shipTransform.GetComponentsInChildren<Gun>();

                for (int i = 0; i < guns1.Length; i++)
                {
                    if (guns1[i].gameObject.activeInHierarchy && !guns1[i].GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(guns1[i].gameObject);
                    }
                }

                AAHardpoint[] hardpoints1 = shipTransform.GetComponentsInChildren<AAHardpoint>();

                for (int i = 0; i < hardpoints1.Length; i++)
                {
                    if (hardpoints1[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[1].weapons.Add(hardpoints1[i].gameObject);
                    }
                }

                Laser[] lasers1 = shipTransform.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers1.Length; i++)
                {
                    if (lasers1[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[2].weapons.Add(lasers1[i].gameObject);
                    }
                }

                MiningDrillLauncher[] miningdrills1 = shipTransform.GetComponentsInChildren<MiningDrillLauncher>();

                for (int i = 0; i < miningdrills1.Length; i++)
                {
                    if (miningdrills1[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[3].weapons.Add(miningdrills1[i].gameObject);
                    }
                }

                break;

            case ShipType.cruiser:

                Gun[] guns2 = shipTransform.GetComponentsInChildren<Gun>();

                for (int i = 0; i < guns2.Length; i++)
                {
                    if (guns2[i].gameObject.activeInHierarchy && !guns2[i].GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(guns2[i].gameObject);
                    }
                }

                AAHardpoint[] hardpoints2 = shipTransform.GetComponentsInChildren<AAHardpoint>();

                for (int i = 0; i < hardpoints2.Length; i++)
                {
                    if (hardpoints2[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[1].weapons.Add(hardpoints2[i].gameObject);
                    }
                }

                Laser[] lasers2 = shipTransform.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers2.Length; i++)
                {
                    if (lasers2[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[2].weapons.Add(lasers2[i].gameObject);
                    }
                }

                MiningDrillLauncher[] miningdrills2 = shipTransform.GetComponentsInChildren<MiningDrillLauncher>();

                for (int i = 0; i < miningdrills2.Length; i++)
                {
                    if (miningdrills2[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[3].weapons.Add(miningdrills2[i].gameObject);
                    }
                }

                break;

            case ShipType.battlecruiser:

                Gun[] guns3 = shipTransform.GetComponentsInChildren<Gun>();

                for (int i = 0; i < guns3.Length; i++)
                {
                    if (guns3[i].gameObject.activeInHierarchy && !guns3[i].GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(guns3[i].gameObject);
                    }
                }

                AAHardpoint[] hardpoints3 = shipTransform.GetComponentsInChildren<AAHardpoint>();

                for (int i = 0; i < hardpoints3.Length; i++)
                {
                    if (hardpoints3[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[1].weapons.Add(hardpoints3[i].gameObject);
                    }
                }

                Laser[] lasers3 = shipTransform.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers3.Length; i++)
                {
                    if (lasers3[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[2].weapons.Add(lasers3[i].gameObject);
                    }
                }

                MiningDrillLauncher[] miningdrills3 = shipTransform.GetComponentsInChildren<MiningDrillLauncher>();

                for (int i = 0; i < miningdrills3.Length; i++)
                {
                    if (miningdrills3[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[3].weapons.Add(miningdrills3[i].gameObject);
                    }
                }

                break;

            case ShipType.dreadnought:

                Gun[] guns4 = shipTransform.GetComponentsInChildren<Gun>();

                for (int i = 0; i < guns4.Length; i++)
                {
                    if (guns4[i].gameObject.activeInHierarchy && !guns4[i].GetComponent<MiningDrillLauncher>())
                    {
                        weaponGroups[0].weapons.Add(guns4[i].gameObject);
                    }
                }

                AAHardpoint[] hardpoints4 = shipTransform.GetComponentsInChildren<AAHardpoint>();

                for (int i = 0; i < hardpoints4.Length; i++)
                {
                    if (hardpoints4[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[1].weapons.Add(hardpoints4[i].gameObject);
                    }
                }

                Laser[] lasers4 = shipTransform.GetComponentsInChildren<Laser>();

                for (int i = 0; i < lasers4.Length; i++)
                {
                    if (lasers4[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[2].weapons.Add(lasers4[i].gameObject);
                    }
                }

                MiningDrillLauncher[] miningdrills4 = shipTransform.GetComponentsInChildren<MiningDrillLauncher>();

                for (int i = 0; i < miningdrills4.Length; i++)
                {
                    if (miningdrills4[i].gameObject.activeInHierarchy)
                    {
                        weaponGroups[3].weapons.Add(miningdrills4[i].gameObject);
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
