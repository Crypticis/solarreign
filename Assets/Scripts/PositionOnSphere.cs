using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionOnSphere : MonoBehaviour
{
    public SphereCollider sphereCollider;
    private bool _active;
    private void OnValidate() => _active = sphereCollider != default;
    private void Start()
    {
        // As an example, just position the gameObject that this example component is attached to
        PositionSpawnedOnSphereCollider(gameObject);
    }
    private void PositionSpawnedOnSphereCollider(GameObject targetGameObject)
    {
        if (!_active) return;

        var positionOutsideSphere = Random.onUnitSphere * sphereCollider.radius;
        targetGameObject.transform.position = positionOutsideSphere;
    }
}