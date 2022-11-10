using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystemChild : MonoBehaviour
{
    TargetingSystem targetingSystem;
    SphereCollider collider;
    public float maxWarpRange;

    void Start()
    {
        targetingSystem = transform.parent.GetComponent<TargetingSystem>();
        collider = GetComponent<SphereCollider>();
    }

    void Update()
    {
        collider.radius = maxWarpRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Celestial Body" || other.tag == "Station")
            targetingSystem.LargeCollisionEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Celestial Body" || other.tag == "Station")
            targetingSystem.LargeCollisionExit(other);
    }
}
