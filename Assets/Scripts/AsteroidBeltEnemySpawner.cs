using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidBeltEnemySpawner : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    int maxEnemies = 0;
    public NPCDatabaseObject database;

    void Start()
    {
        Invoke("Spawn", 5f);
    }

    public void Spawn()
    {
        switch (database.GetSystemDangerRating(SceneManager.GetActiveScene().name))
        {
            case 0:

                maxEnemies = 5;

                break;

            case 1:

                maxEnemies = 3;

                break;

            case 2:

                maxEnemies = 2;

                break;

            case 3:

                maxEnemies = 1;

                break;

            case 4:

                maxEnemies = 0;

                break;

            case 5:

                maxEnemies = 0;

                break;
        }

        StartCoroutine("CheckEnemyList");
    }

    IEnumerator CheckEnemyList()
    {
        while (true)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i] == null)
                {
                    enemies[i] = enemies[enemies.Count - 1];
                    enemies.RemoveAt(enemies.Count - 1);
                }
            }

            if(enemies.Count < maxEnemies)
            {
                SpawnEnemy();
            }

            yield return new WaitForSeconds(60f);
        }
    }

    void SpawnEnemy()
    {
        var pirate = PirateSpawner.instance.SpawnPirateAt(this.transform);
        enemies.Add(pirate);
    }
}
