using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObjectRandomizer : MonoBehaviour
{

    public GameObject[] celestialObjects;

    void Start()
    {
        int index = Random.Range(0, celestialObjects.Length);

        celestialObjects[index].SetActive(true);
    }
}
