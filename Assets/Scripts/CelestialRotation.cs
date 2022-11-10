using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialRotation : MonoBehaviour
{

    public float rotateTime;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, (360 / rotateTime) * Time.fixedDeltaTime, 0, Space.Self);
    }
}
