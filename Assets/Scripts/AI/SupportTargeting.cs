using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNB;
using UnitySteer.Behaviors;
using GT2;
using System.Linq;

public class SupportTargeting : Targeting
{
    //public override void FindClosestEnemy()
    //{
    //    if (!isAlly)
    //    {
    //        enemies = new List<GameObject>(BattleManager.instance.enemyFleet);
    //    }
    //    else
    //    {
    //        enemies = new List<GameObject>(BattleManager.instance.fleet);
    //        enemies.Add(GameObject.FindGameObjectWithTag("Player"));
    //    }

    //    if (enemies.Count >= 1)
    //    {
    //        try
    //        {
    //            enemies = enemies.OrderBy(x => x.GetComponent<DamageHandler>().health / x.GetComponent<DamageHandler>().maxHealth).ToList();
    //            enemies.Remove(this.gameObject);
    //        }
    //        catch
    //        {
    //            return;
    //        }
    //        //enemies.Reverse();
    //    }

    //    if (!isDisengaging && enemies.Count >= 1)
    //        target = enemies[0];
    //}

    public override void SortList()
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
}
