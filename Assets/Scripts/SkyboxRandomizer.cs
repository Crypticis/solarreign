using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRandomizer : MonoBehaviour
{
    public Material[] skyboxes;

    void Start()
    {
        RenderSettings.skybox = skyboxes[Random.Range(0, skyboxes.Length)];
    }
}
