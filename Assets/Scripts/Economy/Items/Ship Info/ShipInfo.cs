using GNB;
using UnityEngine;

public class ShipInfo : MonoBehaviour
{
    public ShipInfoObject info;

    public WeaponModule[] weaponModules;

    public DefenseModule[] defenseModules;

    DamageHandler damageHandler;

    [ContextMenu("Rename gameObject to ShipInfoObject name")]
    void RenameObject()
    {
        this.gameObject.name = info.Name;
    }

    private void Awake()
    {
        UpdateModules();
    }

    private void Update()
    {
        MechanicUI.onShipModuleChange += UpdateModules;
    }

    public void UpdateModules()
    {
        SetDamageHandlerToPlayer();

        weaponModules = GetComponentsInChildren<WeaponModule>();

        defenseModules = GetComponentsInChildren<DefenseModule>();

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

        for (int i = 0; i < defenseModules.Length; i++)
        {
            defenseModules[i].defense = info.defenses[i];

            if (info.defenses[i].defenseType == DefenseType.point_defense)
            {
                defenseModules[i].SetPointDefense();
            }
            else
            {
                defenseModules[i].SetNotPointDefense();
            }
        }

        for (int i = 0; i < info.defenses.Length; i++)
        {
            if(info.defenses[i].defenseType == DefenseType.armor)
                damageHandler.maxHealth += 100;

            if (info.defenses[i].defenseType == DefenseType.shield)
                damageHandler.maxShield += 50;
        }

        switch (info.shipType)
        {
            case ShipType.corvette:
                damageHandler.maxHealth += 50;
                break;
            case ShipType.destroyer:
                damageHandler.maxHealth += 75;
                break;
            case ShipType.cruiser:
                damageHandler.maxHealth += 100;
                break;
            case ShipType.battlecruiser:
                damageHandler.maxHealth += 125;
                break;
            case ShipType.battleship:
                damageHandler.maxHealth += 200;
                break;
            case ShipType.dreadnought:
                damageHandler.maxHealth += 500;
                break;
            default:
                break;
        }
    }

    public void SetDamageHandlerToPlayer()
    {
        damageHandler = Player.playerInstance.GetComponent<DamageHandler>();
    }
}