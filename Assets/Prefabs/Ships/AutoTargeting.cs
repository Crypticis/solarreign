using GT2;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AutoTargeting : MonoBehaviour
{
    public GameObject target;
    GameObject player;
    public float searchInterval = 10f;

    public List<GameObject> enemies;
    [SerializeField] PointDefense pd;
    [SerializeField] TurretAim turretAim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("OrderEnemiesByDistance");
    }

    void Update()
    {
        if (target != null)
        {
            turretAim.AimPosition = target.transform.position;
            pd.IsFiring = true;
        }
        else
        {
            turretAim.AimPosition = Vector3.zero;
            pd.IsFiring = false;
        }
    }

    private IEnumerator OrderEnemiesByDistance()
    {
        while (true)
        {
            SortList();

            yield return new WaitForSeconds(searchInterval);
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

            target = enemies[0];
        }
        catch
        {
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == Player.playerInstance.transform)
            return;

        if (other.gameObject.GetComponent<PlayerFaction>())
        {
            for (int i = 0; i < StatManager.instance.relations.Length; i++)
            {
                if (player.GetComponentInChildren<FleetFaction>().faction == StatManager.instance.relations[i].faction)
                {
                    if (StatManager.instance.relations[i].relation <= 0)
                    {
                        enemies.Add(other.gameObject);
                        SortList();
                        return;
                    }
                }
            }

            if (player.GetComponentInChildren<FleetFaction>().faction.IsEnemy(other.gameObject.GetComponent<FleetFaction>().faction) && !enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
                SortList();
                return;
            }
        }

        if (other.gameObject.GetComponent<FleetFaction>())
        {
            if (player.GetComponentInChildren<FleetFaction>().faction.IsEnemy(other.gameObject.GetComponent<FleetFaction>().faction) && !enemies.Contains(other.gameObject))
            {
                enemies.Add(other.gameObject);
                SortList();
                return;
            }
        }

        if(other.TryGetComponent(out AAMissile missile))
        {
            if (missile.shooter != player)
            {
                enemies.Add(missile.gameObject);
                SortList();
                return;
            }
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (enemies.Contains(other.gameObject))
        {
            enemies.Remove(other.gameObject);
        }
    }
}
