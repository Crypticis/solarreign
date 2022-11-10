using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianShipSpawner : MonoBehaviour
{
	public Transform spawnLocation;

	public GameObject[] stations;
	public GameObject[] civilianShips;
	public float spawnIntervalMax = 10f;
	public float spawnIntervalMin = 5f;
	public float maxShips = 5f;
	public float minShips = 1f;
	GameObject ship;

	private IEnumerator coroutine;

	void Start()
	{
		if(stations.Length > 0)
        {
			coroutine = ShipSpawn();
			StartCoroutine(coroutine);
		}
	}

	private IEnumerator ShipSpawn()
	{
		while (true)
		{
			if (civilianShips != null && maxShips != 0)
			{

				float numberOfShips = Random.Range(minShips, maxShips);

				for (int i = 0; i < numberOfShips; i++)
				{
					ship = Instantiate(civilianShips[Random.Range(0, civilianShips.Length)], spawnLocation.position, Quaternion.identity);

					GameObject go = stations[Random.Range(0, stations.Length)];

					if(go != this)
                    {
						ship.GetComponent<GoForStation>().station = go;
					} else
                    {
						go = stations[Random.Range(0, stations.Length)];
					}
				}
			}
			yield return new WaitForSeconds(Random.Range(spawnIntervalMin, spawnIntervalMax));
		}
	}
}
