using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public Transform[] spawns;

    public void Start()
    {
        for (int i = 0; i < spawns.Length; i++)
        {
            Instantiate(prefabs[Random.Range(0, prefabs.Length)], new Vector3(spawns[i].position.x + Random.Range(-5000, 5000), 0, spawns[i].position.z + Random.Range(-5000, 5000)), Quaternion.identity, this.transform);
        }
    }
}
