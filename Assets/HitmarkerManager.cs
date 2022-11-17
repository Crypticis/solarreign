using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitmarkerManager : MonoBehaviour
{

    public GameObject hitmarker;

    // Update is called once per frame
    public void Hit()
    {
        hitmarker.SetActive(true);
        AudioManager.instance.Play("HitMarker");
        Invoke("DisableHit", .01f);
    }

    void DisableHit()
    {
        hitmarker.SetActive(false);
    }
}
