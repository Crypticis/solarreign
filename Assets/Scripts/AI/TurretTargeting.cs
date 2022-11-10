using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNB;
using UnitySteer.Behaviors;
using GT2;
using System.Linq;

public class TurretTargeting : MonoBehaviour
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
    public List<string> tags = new List<string>();

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        los = GetComponentsInChildren<LineOfSightSingle>();
    }

    void Update()
    {
        Invoke("FindClosestEnemy", 1f);
        StartCoroutine("EnemySearch");

        if (target != null)
        {
            detectableTarget = target.GetComponent<DetectableObject>();
            foreach (LineOfSightSingle lineofsight in los)
            {
                lineofsight.target = target;
            }
        }
        else
        {
            foreach (LineOfSightSingle lineofsight in los)
            {
                lineofsight.target = null;
            }
        }

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            if (enemies[i] == null)
            {
                enemies[i] = enemies[enemies.Count - 1];
                enemies.RemoveAt(enemies.Count - 1);
            }
        }
    }

    private IEnumerator EnemySearch()
    {
        yield return new WaitForSeconds(searchInterval);
        SortList();
    }


    void FindClosestEnemy()
    {
        enemies = new List<GameObject>(BattleManager.instance.enemyFleet);

        if(player)
            enemies.Add(player);

        if (enemies.Count > 0)
        {
            enemies = enemies.OrderBy(x => (x.transform.position - this.transform.position).sqrMagnitude).ToList();
            target = enemies[0];
        }
    }

    public void SortList()
    {
        if(enemies.Count > 0)
        {
            enemies = enemies.OrderBy(x => (x.transform.position - this.transform.position).sqrMagnitude).ToList();
            target = enemies[0];
        }
    }
}
