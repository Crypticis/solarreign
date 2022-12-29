using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GT2;
using GNB;


public class LineOfSightMulti : MonoBehaviour
{
    public GameObject target;
    public bool needInFront;

    public Gun[] guns;
    public TurretAim[] turrets;
    public AAHardpoint[] hardpoints;
    public Laser[] lasers;

    public bool hasGun = false;
    public bool hasTurret = false;
    public bool hasHardpoint = false;
    public bool hasLasers = false;

    public float shootingRange = 100f;

    Rigidbody rigidbody;

    void Start()
    {
        guns = GetComponentsInChildren<Gun>();
        lasers = GetComponentsInChildren<Laser>();
        turrets = GetComponentsInChildren<TurretAim>();
        hardpoints = GetComponentsInChildren<AAHardpoint>();
        rigidbody = GetComponentInParent<Rigidbody>();

        if (hardpoints.Length > 0)
            hasHardpoint = true;
        if (guns.Length > 0)
            hasGun = true;
        if (turrets.Length > 0)
            hasTurret = true;
        if (lasers.Length > 0)
            hasLasers = true;
    }

    public void FixedUpdate()
    {
        Vector3 velocity = rigidbody.velocity;

        if (target != null)
        {
            if (hasGun)
                for (int i = 0; i < guns.Length; i++)
                {
                    guns[i].GimbalTarget = target.transform.position;
                }

            if (hasTurret)
                for (int i = 0; i < turrets.Length; i++)
                {
                    turrets[i].AimPosition = target.transform.position;
                }

            if (needInFront)
            {
                if (HaveLineOfSight() && InFront())
                {
                    if (hasGun)
                        for (int i = 0; i < guns.Length; i++)
                        {
                            guns[i].IsFiring = true;
                        }
                    if (hasHardpoint)
                        for (int i = 0; i < hardpoints.Length; i++)
                        {
                            hardpoints[i].Launch(target.transform, velocity);
                        }
                }
                else
                {
                    if (hasGun)
                        for (int i = 0; i < guns.Length; i++)
                        {
                            guns[i].IsFiring = false;
                        }
                }
            }
            else
            {
                if (HaveLineOfSight())
                {
                    if (hasGun && !hasTurret)
                        for (int i = 0; i < guns.Length; i++)
                        {
                            guns[i].IsFiring = true;
                        }
                    if (hasHardpoint && !hasTurret)
                        for (int i = 0; i < hardpoints.Length; i++)
                        {
                            hardpoints[i].Launch(target.transform, velocity);
                        }

                    //if (hasGun && hasTurret)
                    //{
                    //    for (int i = 0; i < turrets.Length; i++)
                    //    {
                    //        if (turrets[i].isAimed)
                    //        {
                    //            guns[i].IsFiring = true;
                    //        }
                    //    }
                    //}

                    //if (hasLasers && hasTurret)
                    //{
                    //    for (int i = 0; i < turrets.Length; i++)
                    //    {
                    //        if (turrets[i].isAimed)
                    //        {
                    //            lasers[i].isFiring = true;
                    //        }
                    //    }
                    //}

                    if (hasTurret)
                    {
                        for (int i = 0; i < turrets.Length; i++)
                        {
                            if (turrets[i].GetComponentInChildren<Gun>() && turrets[i].IsAimed)
                            {
                                turrets[i].GetComponentInChildren<Gun>().IsFiring = true;
                            }

                            if (turrets[i].GetComponentInChildren<AAHardpoint>() && turrets[i].IsAimed)
                            {
                                turrets[i].GetComponentInChildren<AAHardpoint>().Launch(target.transform, velocity);
                            }

                            if (turrets[i].GetComponentInChildren<Laser>() && turrets[i].IsAimed)
                            {
                                turrets[i].GetComponentInChildren<Laser>().isFiring = true;
                            }
                        }
                    }
                }
                else
                {
                    if (hasGun)
                        for (int i = 0; i < guns.Length; i++)
                        {
                            guns[i].IsFiring = false;
                        }

                    if(hasLasers)
                        for (int i = 0; i < lasers.Length; i++)
                        {
                            lasers[i].isFiring = false;
                        }
                }
            }
        }
    }

    bool HaveLineOfSight()
    {
        if (target == null)
            return false;

        RaycastHit hit;
        Vector3 direction = target.transform.position - transform.position;


        if (Physics.Raycast(transform.position, direction, out hit, shootingRange))
        {
            if (hit.transform == target.transform)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    bool InFront()
    {
        if (target == null)
            return false;

        Vector3 directionToTarget = target.transform.position - transform.position;
        float angle = Vector3.Angle(directionToTarget, transform.forward);

        if (angle < 45)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void ClearTargets()
    {
        if (hasTurret)
            for (int i = 0; i < turrets.Length; i++)
            {
                turrets[i].AimPosition = Vector3.zero;
            }
    }
}
