using GNB;
using UnityEngine;

public class WeaponModule : MonoBehaviour
{
    public Weapon weapon;

    public AAHardpoint hardpoint;
    public Gun gun;
    public Laser laser;
    public MiningDrillLauncher mining;

    [SerializeField] GameObject[] weaponTurrets;

    public void SetBlaster()
    {
        for (int i = 0; i < weaponTurrets.Length; i++)
        {
            if (i != 0)
                weaponTurrets[i].SetActive(false);
            else
                weaponTurrets[i].SetActive(true);
        }



        //hardpoint.enabled = false;
        //gun.enabled = true;
        //if (laser)
        //    laser.enabled = false;
        //mining.enabled = false;
    }

    public void SetMissile()
    {
        for (int i = 0; i < weaponTurrets.Length; i++)
        {
            if (i != 1)
                weaponTurrets[i].SetActive(false);
            else
                weaponTurrets[i].SetActive(true);
        }

        //hardpoint.enabled = true;
        //gun.enabled = false;
        //if (laser)
        //    laser.enabled = false;
        //mining.enabled = false;
    }

    public void SetLaser()
    {
        for (int i = 0; i < weaponTurrets.Length; i++)
        {
            if (i != 2)
                weaponTurrets[i].SetActive(false);
            else
                weaponTurrets[i].SetActive(true);
        }

        //if (laser)
        //    laser.enabled = true;
        //hardpoint.enabled = false;
        //gun.enabled = false;
        //mining.enabled = false;
    }

    public void SetMining()
    {
        for (int i = 0; i < weaponTurrets.Length; i++)
        {
            if (i != 3)
                weaponTurrets[i].SetActive(false);
            else
                weaponTurrets[i].SetActive(true);
        }

        //if (laser)
        //    laser.enabled = false;
        //hardpoint.enabled = false;
        //gun.enabled = false;
        //mining.enabled = true;
    }
}