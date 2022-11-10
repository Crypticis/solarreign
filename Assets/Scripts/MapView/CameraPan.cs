using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    private float dist;
    private Vector3 MouseStart;
    private Vector3 derp;
    public CameraRotateZoom CameraRotateZoom;
    public float RotationsSpeed = 5f;
    public Transform rotateOrigin;
    public float panSpeed = 500f;
    public float panBorderThickness = 10f;
    //public Vector2 panLimit;

    public bool isMovable = true;

    public float scrollSpeed = 20f;
    void Start()
    {
        dist = transform.position.z;  // Distance camera is above map
    }

    void Update()
    {
        //if (Input.GetMouseButtonDown(2)) 
        //{
        //    MouseStart = new Vector3(Input.mousePosition.x, dist, Input.mousePosition.y);
        //    MouseStart = Camera.main.ScreenToWorldPoint(MouseStart);
        //    MouseStart.y = transform.position.y;
        //    CameraRotateZoom.LookAtPlayer = false;
        //}
        //else if (Input.GetMouseButton(2))
        //{
        //    var MouseMove = new Vector3(Input.mousePosition.x, dist, Input.mousePosition.y);
        //    MouseMove = Camera.main.ScreenToWorldPoint(MouseMove);
        //    MouseMove.y = transform.position.y;
        //    transform.position = transform.position - (MouseMove - MouseStart);
        //}

        if (isMovable)
        {
            if (Input.GetButton("Boost"))
            {
                panSpeed = 1000f;
            }
            else
            {
                panSpeed = 500f;
            }

            Vector3 pos = transform.position;
            Vector3 rotatePos = rotateOrigin.position;

            if (Input.GetKey(KeyCode.W)/* || Input.mousePosition.y >= Screen.height - panBorderThickness*/)
            {
                CameraRotateZoom.LookAtPlayer = false;
                pos.z += panSpeed * Time.unscaledDeltaTime;
                rotatePos += transform.forward * (panSpeed * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.S)/* || Input.mousePosition.y <= panBorderThickness*/)
            {
                CameraRotateZoom.LookAtPlayer = false;
                pos.z -= panSpeed * Time.unscaledDeltaTime;
                rotatePos -= transform.forward * (panSpeed * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.D)/* || Input.mousePosition.x >= Screen.width - panBorderThickness*/)
            {
                CameraRotateZoom.LookAtPlayer = false;
                pos.x += panSpeed * Time.unscaledDeltaTime;
                rotatePos += transform.right * (panSpeed * Time.unscaledDeltaTime);
            }
            if (Input.GetKey(KeyCode.A)/* || Input.mousePosition.x <= panBorderThickness*/)
            {
                CameraRotateZoom.LookAtPlayer = false;
                pos.x -= panSpeed * Time.unscaledDeltaTime;
                rotatePos -= transform.right * (panSpeed * Time.unscaledDeltaTime);
            }

            rotatePos.y = Mathf.Clamp(rotatePos.y, 0, 0);
            rotateOrigin.position = rotatePos;
            transform.position = Vector3.Lerp(transform.position, pos, 1f);
        }

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    CameraRotateZoom.LookAtPlayer = false;
        //    pos.y += panSpeed * Time.unscaledDeltaTime;
        //    rotatePos.y += panSpeed * Time.unscaledDeltaTime;
        //}
        //if (Input.GetKey(KeyCode.LeftControl))
        //{
        //    CameraRotateZoom.LookAtPlayer = false;
        //    pos.y -= panSpeed * Time.unscaledDeltaTime;
        //    rotatePos.y -= panSpeed * Time.unscaledDeltaTime;
        //}
    }
}
