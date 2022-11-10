using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDistanceLimiter : MonoBehaviour
{
    private Camera mainCamera;
    private Target target;
    float timeToCheckDistance = 5f;

    void Start()
    {
        mainCamera = Camera.main;
        target = GetComponent<Target>();

        StartCoroutine("CheckDistance");
    }

    IEnumerator CheckDistance()
    {
        while (true)
        {
            float distance = (mainCamera.transform.position - transform.position).magnitude;

            if (target.distanceLimit)
            {
                if (distance >= target.distanceToDisable)
                {
                    target.enabled = false;
                }
                else
                {
                    target.enabled = true;
                }
            }

            yield return new WaitForSeconds(timeToCheckDistance);
        }
    }
}
