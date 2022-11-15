using GNB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningDrill : Bullet
{
    public ParticleSystem impactFXObject;
    public GameObject impactObject;
    public bool isImpacted = false;

    public float tetherDistance = 100;

    public LineRenderer lr;

    public void Update()
    {
        if(isImpacted == true && impactObject)
        {
            impactObject.GetComponent<AsteroidNodeDamageHandler>().TakeDamage(bulletDamage, shooter, this.gameObject);
            TimeToLive = 9999;
        }

        if(!impactObject && isImpacted)
        {
            Reload();
        }

        if(Vector3.Distance(Player.playerInstance.transform.position, this.transform.position) > tetherDistance)
        {
            Reload();
        }

        lr.SetPosition(0, this.transform.position);
        lr.SetPosition(1, Player.playerInstance.transform.position);
    }

    public override void DestroyBulletFromImpact(Vector3 impactedPoint, Quaternion impactRotation)
    {
        if (ImpactFXPrefab != null && !impactFXObject)
        {
            impactFXObject = Instantiate(ImpactFXPrefab, impactedPoint, impactRotation);
            impactFXObject.gameObject.transform.SetParent(this.transform);
            impactFXObject.Play();
        }

        CleanUpTrails();
        if (impactObject)
        {
            gameObject.transform.SetParent(impactObject.transform);
            AudioManager.instance.Play("Mining Drill");
        }
    }

    public override void ExplodeBullet(Vector3 explodePosition, Quaternion explodeRotation)
    {
        //if (ExplodeFXPrefab != null)
         //   Instantiate(ImpactFXPrefab, explodePosition, explodeRotation).Play();

        HandleExplosionDamage(explodePosition);

        CleanUpTrails();
        Reload();
    }

    public override void HandleImpactDamage(RaycastHit hitInfo)
    {
        if (hitInfo.collider.GetComponent<AsteroidNodeDamageHandler>())
        {
            impactObject = hitInfo.collider.gameObject;

            isImpacted = true;
        }
    }
    public void Reload()
    {
        gun.ReloadAmmo();
        AudioManager.instance.Stop("Mining Drill");
        Destroy(gameObject);
    }
}
