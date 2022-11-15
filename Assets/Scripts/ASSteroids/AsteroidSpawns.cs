using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AsteroidSpawns : RandomAreaSpawner
{
    [SerializeField]
    private Transform[] prefabNeutral;
    [SerializeField]
    private Transform[] prefabRich;
    [SerializeField]
    private Transform[] prefabPure;
    [SerializeField]
    private Transform prefabNodeNeutral;
    [SerializeField]
    private Transform prefabNodeRich;
    [SerializeField]
    private Transform prefabNodePure;

    public override void CreateAsteroid()
    {
        // INITIALIZE vectors
        Vector3 spawnPos = Vector3.zero;
        // initialize position for asteroid to null until created
        Transform asteroid = null;
        // Random number needed for chances later in method
        int rand;

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
        #region Spawning Asteroids
        switch (NavigationManager.instance.currentSystemDangerRating)
        {
            case 5:

                rand = Random.Range(1, 100);

                if (rand <= 5)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                } 
                else if (rand > 5 && rand <= 20)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                break;

            case 4:

                rand = Random.Range(1, 100);

                if (rand <= 7)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 7 && rand <= 23)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                break;

            case 3:

                rand = Random.Range(1, 100);

                if (rand <= 9)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 9 && rand <= 26)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;

            case 2:

                rand = Random.Range(1, 100);

                if (rand <= 11)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 11 && rand <= 29)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;

            case 1:

                rand = Random.Range(1, 100);

                if (rand <= 13)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 13 && rand <= 32)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;

            case 0:

                rand = Random.Range(1, 100);

                if (rand <= 15)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 15 && rand <= 35)
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(base.prefab[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;
        }
        #endregion
        #region Scaling and Rigidbody
        // Apply scaling.
        float scale = Random.Range(scaleRange.x, scaleRange.y);
        asteroid.localScale = Vector3.one * scale;

        // Apply rigidbody values.
        if (asteroid.TryGetComponent<Rigidbody>(out Rigidbody r))
        {
            if (scaleMass)
                r.mass *= scale * scale * scale;

            r.AddRelativeForce(Random.insideUnitSphere * velocity, ForceMode.VelocityChange);
            r.AddRelativeTorque(Random.insideUnitSphere * angularVelocity * Mathf.Deg2Rad, ForceMode.VelocityChange);
        }
        #endregion
        SpawnNodes(asteroid);
    }

    private void SpawnNodes(Transform parentAsteroid)
    {
        // Position for asteroid node
        Transform node = null;
        // Position of node
        Vector3 direction = Vector3.zero;
        // Random nums for node spawn chance and type
        int rand = Random.Range(2, 5);
        int rand2 = Random.Range(0, 100);
        
        if (parentAsteroid.TryGetComponent<AsteroidInfo>(out AsteroidInfo info))
        {
            for (int i = 0; i < rand; i++)
            {
                switch (info.type)
                {
                    case (AsteroidType.pure):
                        if (rand2 <= 60)
                        {
                            node = prefabNodePure;
                        }
                        else
                        {
                            node = prefabNodeRich;
                        }
                        break;
                    case (AsteroidType.rich):
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
                        break;
                    case (AsteroidType.neutral):
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
                        break;
                }

                /* Creates Sphere of radius 150 around asteroid, plots random point on asteroid,
                    * casts ray out the back in to collide with asteroid and places node on collision point
                    */
                if (parentAsteroid.TryGetComponent<Rigidbody>(out Rigidbody r))
                {
                    direction = r.velocity = Random.onUnitSphere * 150;
                    Physics.Raycast(parentAsteroid.position + direction, -direction, out RaycastHit hit);
                    Instantiate(node, hit.point, Quaternion.identity, parentAsteroid).transform.up = hit.normal;
                }
            }
        }
    }
}
