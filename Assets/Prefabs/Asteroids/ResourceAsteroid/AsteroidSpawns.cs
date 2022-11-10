using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawns : RandomAreaSpawner
{
    public Transform[] prefabNeutral;
    public Transform[] prefabRich;
    public Transform[] prefabVRich;

    public override void CreateAsteroid()
    {
        Vector3 spawnPos = Vector3.zero;

        // Create random position based on specified shape and range.
        if (spawnShape == RandomSpawnerShape.Box)
        {
            spawnPos.x = Random.Range(-range, range) * shapeModifiers.x;
            spawnPos.y = Random.Range(-range, range) * shapeModifiers.y;
            spawnPos.z = Random.Range(-range, range) * shapeModifiers.z;
        }
        else if (spawnShape == RandomSpawnerShape.Sphere)
        {
            spawnPos = Random.insideUnitSphere * range;
            spawnPos.x *= shapeModifiers.x;
            spawnPos.y *= shapeModifiers.y;
            spawnPos.z *= shapeModifiers.z;
        }
        else if (spawnShape == RandomSpawnerShape.HollowSphere)
        {
            spawnPos = Random.insideUnitSphere * range;
            spawnPos.x *= shapeModifiers.x;
            spawnPos.y *= shapeModifiers.y;
            spawnPos.z *= shapeModifiers.z;
        }

        // Offset position to match position of the parent gameobject.
        spawnPos += transform.position;

        // Apply a random rotation if necessary.
        Quaternion spawnRot = (randomRotation) ? Random.rotation : Quaternion.identity;

        if (spawnShape == RandomSpawnerShape.HollowSphere)
        {
            if (sphereCollider.bounds.Contains(spawnPos))
            {
                CreateNewAstroid();
                return;
            }
        }

        // Create the object and set the parent to this gameobject for scene organization.
        // Switch case for NavigationManager danger rating.

        Transform t = null;
        int num;

        switch (NavigationManager.instance.currentSystemDangerRating)
        {
            case 5:

                num = Random.Range(1, 10);

                if (num > 9)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot) as Transform;
                }
                else
                {
                    t = Instantiate(prefab[Random.Range(0, prefab.Length)], spawnPos, spawnRot) as Transform;
                }

                break;

            case 4:

                num = Random.Range(1, 10);

                if (num > 8)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot) as Transform;
                }
                else
                {
                    t = Instantiate(prefab[Random.Range(0, prefab.Length)], spawnPos, spawnRot) as Transform;
                }

                break;

            case 3:

                num = Random.Range(1, 10);

                if (num > 8)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else if (num > 2)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot) as Transform;
                }
                else
                {
                    t = Instantiate(prefab[Random.Range(0, prefab.Length)], spawnPos, spawnRot) as Transform;
                }

                break;

            case 2:

                num = Random.Range(1, 10);

                if (num > 9)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabVRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else if (num > 4)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else
                {
                    t = Instantiate(prefab[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot) as Transform;
                }

                break;

            case 1:

                num = Random.Range(1, 10);

                if (num > 9)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabVRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else if (num > 3)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else
                {
                    t = Instantiate(prefab[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot) as Transform;
                }

                break;

            case 0:

                num = Random.Range(1, 10);

                if(num > 8)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabVRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else if(num > 2)
                {
                    t = Instantiate(prefab[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot) as Transform;
                }
                else
                {
                    t = Instantiate(prefab[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot) as Transform;
                }

                break;
        }

        t.SetParent(transform);
        asteroids.Add(t.gameObject);

        // Apply scaling.
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        t.localScale = Vector3.one * scale;

        // Apply rigidbody values.
        Rigidbody r = t.GetComponent<Rigidbody>();
        if (r)
        {
            if (scaleMass)
                r.mass *= scale * scale * scale;

            r.AddRelativeForce(Random.insideUnitSphere * velocity, ForceMode.VelocityChange);
            r.AddRelativeTorque(Random.insideUnitSphere * angularVelocity * Mathf.Deg2Rad, ForceMode.VelocityChange);
        }
    }
}
