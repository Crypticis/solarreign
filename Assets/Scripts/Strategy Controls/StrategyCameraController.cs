using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyCameraController : MonoBehaviour
{
    public float panSpeed = 20f;
    public float panBorderThickness = 10f;
    //public Vector2 panLimit;

    public float scrollSpeed = 20f;
    //public float minY = 20f;
    //public float maxY = 120f;


    void Update()
    {
        if (Input.GetButton("Boost"))
        {
            panSpeed = 60f;
        }
        else
        {
            panSpeed = 30f;
        }

        Vector3 pos = transform.position;

        if(Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            pos.y += panSpeed * Time.unscaledDeltaTime;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            pos.y -= panSpeed * Time.unscaledDeltaTime;
        }

        //pos.y -= Input.mouseScrollDelta.y * scrollSpeed * 100f * Time.deltaTime;
        //pos.x = Mathf.Clamp(pos.x, -panLimit.x, panLimit.x);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //pos.z = Mathf.Clamp(pos.z, -panLimit.y, panLimit.y);

        transform.position = pos;
    }
}
