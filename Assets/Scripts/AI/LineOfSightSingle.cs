using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GT2;
using GNB;


public class LineOfSightSingle : MonoBehaviour
{
    public GameObject target;
    public bool needInFront;

    public Gun gun;
    public TurretAim turret;
    public AAHardpoint hardpoint;

    public bool hasGun = false;
    public bool hasTurret = false;
    public bool hasHardpoint = false;

    public float shootingRange = 100f;

    Rigidbody rigidbody;

    void Start()
    {
        gun = GetComponentInChildren<Gun>();
        turret = GetComponentInChildren<TurretAim>();
        hardpoint = GetComponentInChildren<AAHardpoint>();
        rigidbody = GetComponentInParent<Rigidbody>();

        if (hardpoint)
            hasHardpoint = true;
        if (gun)
            hasGun = true;
        if (turret)
            hasTurret = true;
    }

    public void Update()
    {
        Vector3 velocity = rigidbody.velocity;

        if (target != null)
        {
            if(hasGun)
                gun.GimbalTarget = target.transform.position;
            if(hasTurret)
                turret.AimPosition = target.transform.position;
        }
        else
        {
            if(hasTurret)
                turret.AimPosition = Vector3.zero;
        }

        if (needInFront)
        {
            if (HaveLineOfSight() && InFront())
            {
                if(hasGun)
                    gun.IsFiring = true;
                if(hasHardpoint)
                    hardpoint.Launch(target.transform, velocity);
            }
            else
            {
                if(hasGun)
                    gun.IsFiring = false;
            }
        } else
        {
            if (HaveLineOfSight())
            {
                if (hasGun && !hasTurret)
                    gun.IsFiring = true;
                if (hasHardpoint)
                    hardpoint.Launch(target.transform, velocity);
                if (hasGun && hasTurret)
                {
                    if(turret.isAimed)
                        gun.IsFiring = true;
                }
            }
            else
            {
                if (hasGun)
                    gun.IsFiring = false;
            }
        }
    }

    bool HaveLineOfSight()
    {
        if (target == null)
            return false;

        RaycastHit hit;
        Vector3 direction = target.transform.position - transform.position;

        if (hasHardpoint)
        {
            if (Physics.Raycast(hardpoint.transform.position, direction, out hit, shootingRange))
            {
                if (hit.transform == target.transform)
                {
                    Debug.DrawRay(hardpoint.transform.position, direction, Color.red);
                    return true;
                }
                else
                {
                    Debug.DrawRay(hardpoint.transform.position, direction, Color.blue);
                    return false;
                }
            }
        }
        if (hasGun)
        {
            if (Physics.Raycast(gun.transform.position, direction, out hit, shootingRange))
            {
                if (hit.transform == target.transform)
                {
                    Debug.DrawRay(gun.transform.position, direction, Color.red);
                    return true;
                }
                else
                {
                    Debug.DrawRay(gun.transform.position, direction, Color.blue);
                    return false;
                }
            }
        }
        if (hasTurret)
        {
            if (Physics.Raycast(turret.transform.position, direction, out hit, shootingRange))
            {
                if (hit.transform == target.transform)
                {
                    Debug.DrawRay(turret.transform.position, direction, Color.red);
                    return true;
                }
                else
                {
                    Debug.DrawRay(turret.transform.position, direction, Color.blue);
                    return false;
                }
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
            Debug.DrawLine(transform.position, target.transform.position, Color.blue, 1f);
            return true;
        }
        else
        {
            Debug.DrawLine(transform.position, target.transform.position, Color.red, 1f);
            return false;
        }
    }
}
