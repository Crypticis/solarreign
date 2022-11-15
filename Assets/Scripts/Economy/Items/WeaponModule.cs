using GNB;
using UnityEngine;

public class WeaponModule : MonoBehaviour
{
    public Weapon weapon;

    public AAHardpoint hardpoint;
    public Gun gun;
    public Laser laser;
    public MiningDrillLauncher mining;

    public void SetBlaster()
    {
        hardpoint.enabled = false;
        gun.enabled = true;
        if (laser)
            laser.enabled = false;
        mining.enabled = false;
    }

    public void SetMissile()
    {
        hardpoint.enabled = true;
        gun.enabled = false;
        if (laser)
            laser.enabled = false;
        mining.enabled = false;
    }

    public void SetLaser()
    {
        if (laser)
            laser.enabled = true;
        hardpoint.enabled = false;
        gun.enabled = false;
        mining.enabled = false;
    }

    public void SetMining()
    {
        if (laser)
            laser.enabled = false;
        hardpoint.enabled = false;
        gun.enabled = false;
        mining.enabled = true;
    }
}