using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNB;
using UnitySteer.Behaviors;
using GT2;
using System.Linq;

public class ATargeting : MonoBehaviour
{
    public GameObject target;
    DetectableObject detectableTarget;
    public float targetingRange = 100000f;
    public GameObject player;
    SteerForPursuit movement;
    LineOfSightSingle[] los;
    public float searchInterval = 10f;

    public Faction faction;
    public List<GameObject> enemies;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = GetComponent<SteerForPursuit>(); ;
        los = GetComponentsInChildren<LineOfSightSingle>();
    }

    void Update()
    {
        StartCoroutine("EnemySearch");
        if (target != null)
        {
            detectableTarget = target.GetComponent<DetectableObject>();
            movement.Quarry = detectableTarget;
            //movement.enabled = true;
            foreach (LineOfSightSingle lineofsight in los)
            {
                lineofsight.target = target;
            }
        }
        else
        {
            //movement.enabled = false;
            movement.Quarry = null;
            foreach (LineOfSightSingle lineofsight in los)
            {
                lineofsight.target = null;
            }
        }
    }

    private IEnumerator EnemySearch()
    {
        yield return new WaitForSeconds(searchInterval);
        FindClosestEnemy();
    }


    void FindClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closestEnemy = null;

        //List<string> tags = new List<string>();

        //for (int i = 0; i < faction.enemies.Count; i++)
        //{
        //    if (!tags.Contains(faction.enemies[i].tag))
        //        tags.Add(faction.enemies[i].tag);
        //}

        //GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        //GameObject[] FindGameObjectsWithTags(List<string> tags)
        //{
        //    var all = new List<GameObject>();

        //    foreach (string tag in tags)
        //    {
        //        var temp = GameObject.FindGameObjectsWithTag(tag).ToList();
        //        all = all.Concat(temp).ToList();
        //    }

        //    return all.ToArray();
        //}

        enemies = new List<GameObject>();
        enemies.AddRange(allEnemies);

        for (int i = 0; i < faction.enemies.Count; i++)
        {
            if (faction.enemies[i].playerInFaction)
            {
                enemies.Add(player);
            }
        }

        foreach (GameObject currentEnemy in enemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if (distanceToEnemy < distanceToClosestEnemy && distanceToEnemy <= (targetingRange * targetingRange))
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
                target = closestEnemy;
            }
            if (distanceToEnemy > (targetingRange * targetingRange))
            {
                target = null;
            }
        }
    }
}
