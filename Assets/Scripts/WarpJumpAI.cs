using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpJumpAI : MonoBehaviour
{
    private Quaternion _lookRotation;
    private Vector3 _direction;
    public Transform target;
    public float rotationSpeed = 30;
    private bool isWarping;


    public void Update()
    {
        if(isWarping == true)
        {
            Align();

            if (Vector3.Distance(target.transform.position, this.transform.position) <= 1000)
            {
                //transform.Find("AI").gameObject.SetActive(true);
                isWarping = false;

                GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    public void Align()
    {
        _direction = (target.position - transform.position).normalized;

        _lookRotation = Quaternion.LookRotation(_direction, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
    }

    public void StartWarp(Transform t)
    {
        target = t;

        isWarping = true;

        Invoke("Warp", 15f);

        Debug.Log("Starting warp");

        //transform.Find("AI").gameObject.SetActive(false);

        GetComponent<BoxCollider>().enabled = false;
    }

    public void Warp()
    {
        //transform.Find("AI").gameObject.SetActive(false);

        transform.position = target.position;
    }
}
