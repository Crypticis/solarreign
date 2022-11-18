using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNB;
using GT2;
using System.Linq;
using UnitySteer.Behaviors;

public class Targeting : MonoBehaviour
{
    public GameObject target;
    public GameObject player;
    LineOfSightSingle[] los;
    LineOfSightMulti lineOfSight;
    public float searchInterval = 10f;

    public List<GameObject> enemies;

    public CommandMode commandMode = CommandMode.manual;
    public CombatStyle combatStyle;
    public CombatState combatState;

    public float distanceTimer;
    public float disengageTimer;
    public float disengageDistance = 30f;

    [Header("Steering Behaviors")]
    public SteerForPursuit pursue;
    public SteerForWander wander;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        los = GetComponentsInChildren<LineOfSightSingle>();
        lineOfSight = GetComponentInChildren<LineOfSightMulti>();

        pursue = GetComponentInChildren<SteerForPursuit>();
        wander = GetComponentInChildren<SteerForWander>();

        StartCoroutine("OrderEnemiesByDistance");
    }

    void Update()
    {
        switch (combatState)
        {
            case CombatState.engage:

                disengageTimer = 2f;

                if(target)
                    pursue.enabled = true;
                else
                    pursue.enabled = false;

                pursue.Weight = 5;

                break;

            case CombatState.disengage:

                pursue.enabled = true;
                pursue.Weight = -5;

                wander.enabled = true;

                break;
        }

        if (target != null)
        {
            distanceTimer -= Time.deltaTime;
            disengageTimer -= Time.deltaTime;

            if (disengageTimer <= 0 && target)
            {
                combatState = CombatState.engage;
            }

            if (combatStyle == CombatStyle.strafe)
            {
                if (Vector3.Distance(target.transform.position, this.transform.position) <= disengageDistance)
                {
                    if (distanceTimer <= 0)
                    {
                        combatState = CombatState.disengage;
                    }
                }
                else
                {
                    distanceTimer = 2f;
                }
            }

            pursue.Quarry = target.GetComponent<DetectableObject>();
            //pursue.GameObjects.Add(target);
            //evade.GameObjects.Clear();
            //evade.GameObjects.Add(target);

            foreach (LineOfSightSingle lineofsight in los)
            {
                lineofsight.target = target;
            }

            if (lineOfSight)
                lineOfSight.target = target;
        }
        else
        {
            //evade.GameObjects.Clear();


            foreach (LineOfSightSingle lineofsight in los)
            {
                lineofsight.target = null;
            }

            if (lineOfSight)
            {
                lineOfSight.ClearTargets();
            }
        }
    }

    private IEnumerator OrderEnemiesByDistance()
    {
        while (true)
        {
            SortList();

            if (commandMode == CommandMode.automatic)
            {
                yield return new WaitForSeconds(searchInterval);
            }
        }
    }


    public virtual void FindClosestEnemy()
    {
        try
        {
            if (enemies.Count >= 1 && !target)
            {
                enemies = enemies.OrderBy(x => (x.transform.position - this.transform.position).sqrMagnitude).ToList();
            }

            if (combatState == CombatState.engage)
                target = enemies[0];
        }
        catch
        {
            return;
        }
    }

    public virtual void SortList()
    {
        try
        {
            if (enemies.Count > 1)
                enemies = enemies.OrderBy(x => (x.transform.position - this.transform.position).sqrMagnitude).ToList();

            if (combatState == CombatState.engage)
                target = enemies[0];
        }
        catch
        {
            return;
        }
    }

    public void SetTarget(GameObject objectToTarget)
    {
        target = objectToTarget;
    }

    public void RemoveTarget()
    {
        target = null;
    }

    public enum CombatStyle
    {
        strafe,
        orbit
    }
    public enum CombatState
    {
        engage,
        disengage,
    }
}

public enum CommandMode
{
    automatic,
    manual
}
