using GNB;
using UnityEngine;

public class ShipInfo : MonoBehaviour
{
    public ShipInfoObject info;

    public WeaponModule[] weaponModules;

    public DamageHandler damageHandler;

    private void Awake()
    {
        weaponModules = GetComponentsInChildren<WeaponModule>();

        for (int i = 0; i < weaponModules.Length; i++)
        {
            weaponModules[i].weapon = info.weapons[i];

            if (info.weapons[i].weaponType == WeaponType.missile)
            {
                weaponModules[i].SetMissile();
            }
            else if (info.weapons[i].weaponType == WeaponType.energy)
            {
                weaponModules[i].SetBlaster();
            }
            else if (info.weapons[i].weaponType == WeaponType.laser)
            {
                weaponModules[i].SetLaser();
            }
            else if (info.weapons[i].weaponType == WeaponType.mining)
            {
                weaponModules[i].SetMining();
            }
        }

        for (int i = 0; i < info.defenses.Length; i++)
        {
            damageHandler.maxHealth += 100;
        }
    }
}