using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAI : Laser
{
    public float bonusDamage;

    public override void Start()
    {
        weapon = GetComponentInParent<WeaponModule>();

        for (int i = 0; i < firePoints.Length; i++)
        {
            var laser = Instantiate(laserPrefab, firePoints[i]) as GameObject;
            laser.GetComponent<LaserBeam>().damage += bonusDamage;
            //laser.GetComponent<LaserBeam>().damage = -0.01f;
            spawnedLasers.Add(laser);
        }

        DisableLaser();
    }
}
