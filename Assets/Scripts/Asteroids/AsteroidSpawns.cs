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
    [SerializeField]
    private Transform prefabShonyx;

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
                    asteroid = Instantiate(prefabNeutral[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot, transform) as Transform;
                } 
                else if (rand > 5 && rand <= 20)
                {
                    asteroid = Instantiate(prefabRich[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(prefabPure[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                break;

            case 4:

                rand = Random.Range(1, 100);

                if (rand <= 7)
                {
                    asteroid = Instantiate(prefabNeutral[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 7 && rand <= 23)
                {
                    asteroid = Instantiate(prefabRich[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(prefabPure[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                break;

            case 3:

                rand = Random.Range(1, 100);

                if (rand <= 9)
                {
                    asteroid = Instantiate(prefabNeutral[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 9 && rand <= 26)
                {
                    asteroid = Instantiate(prefabRich[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(prefabPure[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;

            case 2:

                rand = Random.Range(1, 100);

                if (rand <= 11)
                {
                    asteroid = Instantiate(prefabNeutral[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 11 && rand <= 29)
                {
                    asteroid = Instantiate(prefabRich[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(prefabPure[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;

            case 1:

                rand = Random.Range(1, 100);

                if (rand <= 13)
                {
                    asteroid = Instantiate(prefabNeutral[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 13 && rand <= 32)
                {
                    asteroid = Instantiate(prefabRich[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(prefabPure[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
                }

                break;

            case 0:

                rand = Random.Range(1, 100);

                if (rand <= 15)
                {
                    asteroid = Instantiate(prefabNeutral[Random.Range(0, prefabNeutral.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else if (rand > 15 && rand <= 35)
                {
                    asteroid = Instantiate(prefabRich[Random.Range(0, prefabRich.Length)], spawnPos, spawnRot, transform) as Transform;
                }
                else
                {
                    asteroid = Instantiate(prefabPure[Random.Range(0, prefabPure.Length)], spawnPos, spawnRot, transform) as Transform;
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

        // Random nums for node and crystal spawn chance, amount, type
        int numCrystals = Random.Range(3, 5);
        int numNodes = Random.Range(2, 5);
        int[] rand = new int[3];

        for (int i = 0; i < rand.Length; i++)
            rand[i] = Random.Range(0, 100);

        // 8% chance of a crystallized asteroid
        if (rand[0] < 8)
        {
            if (parentAsteroid.TryGetComponent<Rigidbody>(out Rigidbody r))
            {
                for (int i = 0; i < numCrystals; i++)
                {
                    direction = r.velocity = Random.onUnitSphere * 150;
                    Physics.Raycast(parentAsteroid.position + direction, -direction, out RaycastHit hit);
                    Instantiate(prefabShonyx, hit.point, Quaternion.identity, parentAsteroid).transform.up = hit.normal;
                }
            }
        }
        else
        {
            if (parentAsteroid.TryGetComponent<AsteroidInfo>(out AsteroidInfo info))
            {
                // 2 - 5 ore nodes may spawn
                for (int i = 0; i < numNodes; i++)
                {
                    switch (info.type)
                    {
                        case (AsteroidType.pure):
                            if (rand[1] <= 60)
                            {
                                node = prefabNodePure;
                            }
                            else
                            {
                                node = prefabNodeRich;
                            }
                            break;
                        case (AsteroidType.rich):
                            if (rand[1] <= 20)
                            {
                                node = prefabNodePure;
                            }
                            else if (rand[1] > 20 && rand[1] <= 75)
                            {
                                node = prefabNodeRich;
                            }
                            else
                            {
                                node = prefabNodeNeutral;
                            }
                            break;
                        case (AsteroidType.neutral):
                            if (rand[1] <= 5)
                            {
                                node = prefabNodePure;
                            }
                            else if (rand[1] > 5 && rand[1] <= 25)
                            {
                                node = prefabNodeRich;
                            }
                            else
                            {
                                node = prefabNodeNeutral;
                            }
                            break;
                    }

                    // Spawn ore node on asteroid
                    if (parentAsteroid.TryGetComponent<Rigidbody>(out Rigidbody r))
                    {
                        direction = r.velocity = Random.onUnitSphere * 150;
                        Physics.Raycast(parentAsteroid.position + direction, -direction, out RaycastHit hit);
                        Instantiate(node, hit.point, Quaternion.identity, parentAsteroid).transform.up = hit.normal;
                    }

                }

                // Spawn crystal on asteroid
                if (parentAsteroid.TryGetComponent<Rigidbody>(out Rigidbody b))
                {
                    if (rand[2] < 16)
                    {
                        direction = b.velocity = Random.onUnitSphere * 150;
                        Physics.Raycast(parentAsteroid.position + direction, -direction, out RaycastHit hit);
                        Instantiate(prefabShonyx, hit.point, Quaternion.identity, parentAsteroid).transform.up = hit.normal;
                    }
                }
            }
        }
    }
}
