using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{

    public GameObject laserPrefab;
    public Transform[] firePoints;

    public WeaponModule weapon;

    //public GameObject[] spawnedLasers;

    public List<GameObject> spawnedLasers = new List<GameObject>();

    public bool isFiring;
    public AudioSource audioSource;

    public virtual void Start()
    {
        weapon = GetComponentInParent<WeaponModule>();

        for (int i = 0; i < firePoints.Length; i++)
        {
            var laser = Instantiate(laserPrefab, firePoints[i]) as GameObject;
            laser.GetComponent<LaserBeam>().damage = weapon.weapon.damage;
            spawnedLasers.Add(laser);
        }

        DisableLaser();
    }

    void Update()
    {
        if (isFiring)
        {
            EnableLaser();
        }
        else
        {
            DisableLaser();
        }

        if (isFiring)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
    }

    public void EnableLaser()
    {
        for (int i = 0; i < spawnedLasers.Count; i++)
        {
            spawnedLasers[i].SetActive(true);
        }
        //spawnedLaser.SetActive(true);
    }

    public void DisableLaser()
    {
        for (int i = 0; i < spawnedLasers.Count; i++)
        {
            spawnedLasers[i].SetActive(false);
        }
        //spawnedLaser.SetActive(false);
    }

    public void UpdateLaser()
    {
        //if(firePoint != null)
        //{
        //    spawnedLaser.transform.position = firePoint.position;
        //}

        for (int i = 0; i < spawnedLasers.Count; i++)
        {
            spawnedLasers[i].transform.position = firePoints[i].position;
        }


        //set a raycast out from beam, if it hits something set the last point of the line to the raycast hit point.
    }
}
