using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateZoom : MonoBehaviour
{
    public Transform PlayerTransform;
    public bool RotateAroundPlayer = false;
    public bool LookAtPlayer = true;
    public float RotationsSpeed = 5f;
    private Vector3 _cameraOffset;
    public float zoomSpeed = 10f;
    public Transform rotateOrigin;

    public Cinemachine.CinemachineFreeLook cm;

    public bool isMovable = true;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
    }

    void LateUpdate()
    {
        if (isMovable)
        {
            if (Input.GetMouseButtonDown(1))
            {
                RotateAroundPlayer = true;
            }

            if (Input.GetMouseButtonUp(1))
            {
                RotateAroundPlayer = false;
            }

            if (RotateAroundPlayer)
            {
                //Quaternion camTurnAngleX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
                //Quaternion camTurnAngleY = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, -Vector3.right);

                //_cameraOffset = camTurnAngleX * camTurnAngleY * _cameraOffset;

                cm.m_XAxis.Value = Input.GetAxis("Mouse X") * RotationsSpeed;

                cm.m_Orbits[0].m_Height += -Input.GetAxis("Mouse Y") * RotationsSpeed * 5;
            }

            if (LookAtPlayer)
            {
                //Vector3 newPos = PlayerTransform.position + _cameraOffset;

                //float scrollInput = Input.GetAxis("Mouse ScrollWheel");

                ////_cameraOffset.y = Mathf.Clamp(_cameraOffset.y, 0, 360);

                //_cameraOffset += Camera.main.transform.forward * scrollInput * zoomSpeed;

                ////newPos.y = Mathf.Clamp(newPos.y, 25f, 1250f);

                //transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
                //transform.LookAt(PlayerTransform);

                float scrollInput = Input.GetAxis("Mouse ScrollWheel");

                cm.m_Orbits[1].m_Height += -scrollInput * zoomSpeed / 3;
                cm.m_Orbits[1].m_Radius += -scrollInput * zoomSpeed;

                rotateOrigin.position = PlayerTransform.position;
            }
            else
            {
                Vector3 newPos = rotateOrigin.position + _cameraOffset;

                float scrollInput = Input.GetAxis("Mouse ScrollWheel");

                //_cameraOffset.y = Mathf.Clamp(_cameraOffset.y, 0f, 1250f);

                ///_cameraOffset.y = Mathf.Clamp(_cameraOffset.y, 0, 360);

                _cameraOffset += this.GetComponent<Camera>().transform.forward * scrollInput * zoomSpeed;
                cm.m_Orbits[1].m_Height += -scrollInput * zoomSpeed / 3;
                cm.m_Orbits[1].m_Radius += -scrollInput * zoomSpeed;

                transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
                transform.LookAt(rotateOrigin);
            }

            //cm.m_Orbits[0].m_Height = Mathf.Clamp(cm.m_Orbits[0].m_Height, 10, 20000);
            //cm.m_Orbits[0].m_Radius = Mathf.Clamp(cm.m_Orbits[0].m_Radius, 0, 20000);
        }
    }

    public void ResetCamera()
    {
        LookAtPlayer = true;
    }
}
