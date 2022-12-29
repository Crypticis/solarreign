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

    public WeaponGroup[] weaponGroups = new WeaponGroup[4];

    int selectIndex = 0;

    [SerializeField] Sprite[] weaponSprites;

    [SerializeField] Image[] weaponSelectionIcons;

    private void Start()
    {
        //Ship was spawning too slowly causing errors. Invoke ensures that the method slows down.
        Invoke("UpdateWeapons", 1f);
        Invoke("IncrementSelectedIndex", 1.1f);
        //IncrementSelectedIndex();
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

        // Adding guns, cleaned up to not matter the ship type.

        Gun[] guns = shipTransform.GetComponentsInChildren<Gun>();

        //Debug.Log(guns.Length);

        for (int i = 0; i < guns.Length; i++)
        {
            //Debug.Log(guns[i]);

            if (guns[i].gameObject.activeInHierarchy && !guns[i].GetComponent<MiningDrillLauncher>() && !guns[i].GetComponent<PointDefense>())
            {
                //Debug.Log("Adding gun");
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
