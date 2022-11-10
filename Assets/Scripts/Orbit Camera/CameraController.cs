using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLFlight;

public class CameraController : MonoBehaviour
{
    CameraRig cameraRig;
    OrbitCamera orbit;
    ShipInput player;
    // Start is called before the first frame update
    void Start()
    {
        cameraRig = GetComponent<CameraRig>();
        orbit = GetComponent<OrbitCamera>();
        player = orbit.player.gameObject.GetComponent<ShipInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("CameraView"))
        {
            if (cameraRig.enabled)
            {
                cameraRig.enabled = false;
                orbit.enabled = true;
                player.usingOrbit = true;
                player.rollSensitivity = 0f;
            } else
            {
                orbit.enabled = false;
                cameraRig.enabled = true;
                player.usingOrbit = false;
                player.rollSensitivity = 0.5f;
            }
        }
    }
}
