using GNB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModule : MonoBehaviour
{
    public Weapon weapon;

    public AAHardpoint hardpoint;
    public Gun gun;
    public Laser laser;

    public void SetBlaster()
    {
        hardpoint.enabled = false;
        gun.enabled = true;
        if (laser)
            laser.enabled = false;
    }

    public void SetMissile()
    {
        hardpoint.enabled = true;
        gun.enabled = false;
        if (laser)
            laser.enabled = false;
    }

    public void SetLaser()
    {
        if (laser)
            laser.enabled = true;
        hardpoint.enabled = false;
        gun.enabled = false;
    }
}
