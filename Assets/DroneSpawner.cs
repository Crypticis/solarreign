using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawner : MonoBehaviour
{
    public int droneAmount;
    public Transform droneSpawnPoint;
    public GameObject dronePrefab;

    public float spawnInterval;
    public float timer;

    public Color allyColor;
    public Color enemyColor;

    Targeting targeting;

    void Start()
    {
        targeting = GetComponent<Targeting>();
        SpawnDrone();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && droneAmount > 0)
        {
            SpawnDrone();
        }
    }

    void SpawnDrone()
    {
        droneAmount--;
        GameObject drone = Instantiate(dronePrefab, droneSpawnPoint.position, Quaternion.identity);
        //if (targeting.isAlly)
        //{
        //    drone.tag = "Ally";
        //    drone.GetComponent<Targeting>().isAlly = true;
        //    drone.GetComponent<Target>().targetColor = allyColor;
        //}
        //else
        //{
        //    drone.tag = "Enemy";
        //    drone.GetComponent<Targeting>().isAlly = false;
        //    drone.GetComponent<Target>().targetColor = enemyColor;
        //}

        timer = spawnInterval;
    }
}
