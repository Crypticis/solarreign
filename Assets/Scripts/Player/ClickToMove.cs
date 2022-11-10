using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class ClickToMove : MonoBehaviour
{
    public LayerMask whatCanBeClickedOn;

    private NavMeshAgent myAgent;

    public GameObject travelMarker;

    public CameraRotateZoom cameraRotate;

    public Transform travelTarget;

    public LineRenderer lr; 

    void Start()
    {
        Cursor.visible = true;
        myAgent = GetComponent<NavMeshAgent>();

        travelMarker.transform.position = transform.position;
    }

    void Update()
    {
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, travelMarker.transform.position);

        if (travelTarget)
        {
            myAgent.SetDestination(travelTarget.position);

            if(travelTarget.GetComponent<HighlightPlus.HighlightEffect>())
                travelTarget.GetComponent<HighlightPlus.HighlightEffect>().highlighted = true;

            travelMarker.transform.position = travelTarget.transform.position;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            cameraRotate.ResetCamera();
        }

        //if (myAgent.remainingDistance < 1f && !Input.GetKey(KeyCode.T))
        //{
        //    Time.timeScale = 0f;
        //} 
        //else if(Input.GetKey(KeyCode.T))
        //{
        //    Time.timeScale = GameManager.instance.speedMultiplier;
        //} 
        //else
        //{
        //    Time.timeScale = GameManager.instance.speedMultiplier;
        //}

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Time.timeScale = GameManager.instance.speedMultiplier;

            if(travelTarget)
                travelTarget.GetComponent<HighlightPlus.HighlightEffect>().highlighted = false;

            travelTarget = null;
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast (myRay, out hitInfo, 10000, whatCanBeClickedOn))
            {
                if (hitInfo.collider.gameObject.GetComponent<WorldInteraction>() && !EventSystem.current.IsPointerOverGameObject())
                {
                    travelTarget = hitInfo.collider.gameObject.transform;
                    travelMarker.transform.position = hitInfo.collider.gameObject.transform.position;
                    cameraRotate.ResetCamera();
                    travelTarget.GetComponent<HighlightPlus.HighlightEffect>().HitFX();
                }
                else if(!EventSystem.current.IsPointerOverGameObject())
                {
                    myAgent.SetDestination(hitInfo.point);
                    travelMarker.transform.position = new Vector3(hitInfo.point.x, 2f, hitInfo.point.z);
                    cameraRotate.ResetCamera();
                }
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                if (AudioManager.instance)
                {
                    AudioManager.instance.Play("Click2");
                }
            }
        }
    }
}
