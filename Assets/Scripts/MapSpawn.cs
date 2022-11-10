using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapSpawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] spawnPoints;

    public static MapSpawn instance;

    private void Awake()
    {
        instance = this;
    }

    //public void SpawnGSF()
    //{
    //    Player.playerInstance.gameObject.transform.position = spawnPoints[0].position;
    //}

    //public void SpawnZG()
    //{
    //    Player.playerInstance.gameObject.transform.position = spawnPoints[1].position;
    //}

    public void SpawnNeutral()
    {

    }
}
