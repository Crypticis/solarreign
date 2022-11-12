using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidSpawns : RandomAreaSpawner
{
    public Transform[] prefabNeutral;
    public Transform[] prefabRich;
    public Transform[] prefabPure;
    public Transform prefabNodeNeutral;
    public Transform prefabNodeRich;
    public Transform prefabNodePure;

    public override void CreateAsteroid()
    {
        Vector3 spawnPos = Vector3.zero;
        Vector3 direction;
        
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

        // 
        Transform t = null;
        Transform prefab = null;
        int rand;
        int rand2;
        

        switch (NavigationManager.instance.currentSystemDangerRating)
        {
            case 5:

                rand = Random.Range(1, 100);

                if (rand <= 5)
                {
                    prefab = base.prefab[Random.Range(0, prefabPure.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                } 
                else if (rand > 5 && rand <= 20)
                {
                    prefab = base.prefab[Random.Range(0, prefabRich.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else
                {
                    prefab = base.prefab[Random.Range(0, prefabNeutral.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                break;

            case 4:

                rand = Random.Range(1, 100);

                if (rand <= 7)
                {
                    prefab = base.prefab[Random.Range(0, prefabPure.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else if (rand > 7 && rand <= 23)
                {
                    prefab = base.prefab[Random.Range(0, prefabRich.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else
                {
                    prefab = base.prefab[Random.Range(0, prefabNeutral.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                break;

            case 3:

                rand = Random.Range(1, 100);

                if (rand <= 9)
                {
                    prefab = base.prefab[Random.Range(0, prefabPure.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else if (rand > 9 && rand <= 26)
                {
                    prefab = base.prefab[Random.Range(0, prefabRich.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else
                {
                    prefab = base.prefab[Random.Range(0, prefabNeutral.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                break;

            case 2:

                rand = Random.Range(1, 100);

                if (rand <= 11)
                {
                    prefab = base.prefab[Random.Range(0, prefabPure.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else if (rand > 11 && rand <= 29)
                {
                    prefab = base.prefab[Random.Range(0, prefabRich.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else
                {
                    prefab = base.prefab[Random.Range(0, prefabNeutral.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }

                break;

            case 1:

                rand = Random.Range(1, 100);

                if (rand <= 13)
                {
                    prefab = base.prefab[Random.Range(0, prefabPure.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else if (rand > 13 && rand <= 32)
                {
                    prefab = base.prefab[Random.Range(0, prefabRich.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else
                {
                    prefab = base.prefab[Random.Range(0, prefabNeutral.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }

                break;

            case 0:

                rand = Random.Range(1, 100);

                if (rand <= 15)
                {
                    prefab = base.prefab[Random.Range(0, prefabPure.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else if (rand > 15 && rand <= 35)
                {
                    prefab = base.prefab[Random.Range(0, prefabRich.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
                }
                else
                {
                    prefab = base.prefab[Random.Range(0, prefabNeutral.Length)];
                    t = Instantiate(prefab, spawnPos, spawnRot) as Transform;
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

        // Spawns 2 - 5 Nodes on asteroid
        rand = Random.Range(2, 4);
        // Determines what type of node will spawn on the asteroid
        rand2 = Random.Range(0, 100);

        // Position for asteroid node
        Transform node = null;

        for (int i = 0; i < rand; i++)
        {
            // Determines node spawn based on asteroid type
            if (prefabPure.Contains(prefab))
            {
                if (rand2 <= 60)
                {
                    node = prefabNodePure;
                }
                else
                {
                    node = prefabNodeRich;
                }
            }
            else if (prefabRich.Contains(prefab))
            {
                if (rand2 <= 20)
                {
                    node = prefabNodePure;
                }
                else if (rand2 > 20 && rand2 < 75)
                {
                    node = prefabNodeRich;
                }
                else
                {
                    node = prefabNodeNeutral;
                }  
            }
            else if (prefabNeutral.Contains(prefab))
            {
                if (rand2 <= 5)
                {
                    node = prefabNodePure;
                }
                else if (rand2 > 5 && rand2 <= 25)
                {
                    node = prefabNodeRich;
                }
                else
                {
                    node = prefabNodeNeutral;
                }
            }

            /* Creates Sphere of radius 150 around asteroid, plots random point on asteroid,
             * casts ray out the back in to collide with asteroid and places node on collision point
             */
            direction = r.velocity = Random.onUnitSphere * 150;
            Physics.Raycast(spawnPos + direction, -direction, out RaycastHit hit);
            Instantiate(node, hit.point, Quaternion.identity, t).transform.up = hit.normal;
        }
    }
}
