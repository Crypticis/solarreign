using GNB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldHitFX : MonoBehaviour
{
    public GameObject shield;

    public Vector3 localScale;

    public void Start()
    {
        localScale = shield.transform.localScale;
    }

    public void FadeIn()
    {
        shield.LeanScale(localScale * 2, 2);
    }

    public void FadeOut()
    {
        shield.LeanScale(localScale / 2, 2);
    }



    //private Material material;
    //public GameObject ripplesVFX;

    //public void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.GetComponent<Bullet>() || collision.gameObject.GetComponent<AAMissile>() || collision.gameObject.GetComponent<LaserBeam>())
    //    {
    //        var ripples = Instantiate(ripplesVFX, transform) as GameObject;
    //        var psr = ripples.transform.GetChild(0).GetComponent<ParticleSystemRenderer>();
    //        material = psr.material;

    //        material.SetVector("SphereCenter", collision.contacts[0].point);

    //        Destroy(ripples, 2);
    //    }
    //}
}
