using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiRotation : MonoBehaviour
{

    public float rotateTime;
    public bool y, x, z;

    // Update is called once per frame
    void Update()
    {
        if(y)
            transform.Rotate(0, (360 / rotateTime) * Time.unscaledDeltaTime, 0, Space.Self);

        if (x)
            transform.Rotate((360 / rotateTime) * Time.unscaledDeltaTime, 0, 0, Space.Self);

        if (z)
            transform.Rotate(0, 0, (360 / rotateTime) * Time.unscaledDeltaTime, Space.Self);
    }
}
