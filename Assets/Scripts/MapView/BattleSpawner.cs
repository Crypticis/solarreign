using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class BattleSpawner : MonoBehaviour
{
    public Transform spawn1;
    public Transform spawn2;
    public float spawnRadius = 100f;

    public Color friendlyColor;
    public Color enemyColor;

    public void Awake()
    {
        //Time.timeScale = 1f;
    }

    void Start()
    {
        for (int i = 0; i < BattleManager.instance._enemyFleet.Length; i++)
        {
            GameObject ship = Instantiate(BattleManager.instance._enemyFleet[i].ship, (spawn2.position + Random.insideUnitSphere * Mathf.Sqrt(spawnRadius)), Quaternion.identity);
            var fm = ship.GetComponent<FleetMember>();

            //ship.GetComponent<DamageHandler>().health = BattleManager.instance._enemyFleet[i].health;
            ship.GetComponent<Target>().targetColor = enemyColor;

            if(BattleManager.instance._enemyFleet[i].hasPilot)
            {
                fm.level.currentLevel = BattleManager.instance._enemyFleet[i].pilot.level.currentLevel;
                fm.level.experience = BattleManager.instance._enemyFleet[i].pilot.level.experience;
                fm.level.MAX_EXP = BattleManager.instance._enemyFleet[i].pilot.level.MAX_EXP;
                fm.level.MAX_LEVEL = BattleManager.instance._enemyFleet[i].pilot.level.MAX_LEVEL;

                fm.level.OnLevelUp = fm.OnLevelUp;

                fm.speedSkill = BattleManager.instance._enemyFleet[i].pilot.speedSkill;
                fm.firespeedSkill = BattleManager.instance._enemyFleet[i].pilot.firespeedSkill;
                fm.durabilitySkill = BattleManager.instance._enemyFleet[i].pilot.durabilitySkill;
                fm.damageSkill = BattleManager.instance._enemyFleet[i].pilot.damageSkill;
                fm.rangeSkill = BattleManager.instance._enemyFleet[i].pilot.rangeSkill;
            }

            ship.tag = "Enemy";

            fm.UpdateSkills();
            BattleManager.instance.enemyFleet.Add(ship);
        }

        for (int i = 0; i < BattleManager.instance.playerFleet.Length; i++)
        {
            GameObject ship = Instantiate(BattleManager.instance.playerFleet[i].ship, (spawn1.position + Random.insideUnitSphere * Mathf.Sqrt(spawnRadius)), Quaternion.identity);
            var fm = ship.GetComponent<FleetMember>();

            ship.GetComponent<SteerToFollow>().Target = Player.playerInstance.transform;
            //ship.GetComponent<DamageHandler>().health = BattleManager.instance.playerFleet[i].health;
            //ship.GetComponent<Targeting>().isAlly = true;
            ship.GetComponent<Target>().targetColor = friendlyColor;

            if(BattleManager.instance.playerFleet[i].hasPilot)
            {
                fm.level.currentLevel = BattleManager.instance.playerFleet[i].pilot.level.currentLevel;
                fm.level.experience = BattleManager.instance.playerFleet[i].pilot.level.experience;
                fm.level.MAX_EXP = BattleManager.instance.playerFleet[i].pilot.level.MAX_EXP;
                fm.level.MAX_LEVEL = BattleManager.instance.playerFleet[i].pilot.level.MAX_LEVEL;

                fm.level.OnLevelUp = fm.OnLevelUp;

                fm.speedSkill = BattleManager.instance.playerFleet[i].pilot.speedSkill;
                fm.firespeedSkill = BattleManager.instance.playerFleet[i].pilot.firespeedSkill;
                fm.durabilitySkill = BattleManager.instance.playerFleet[i].pilot.durabilitySkill;
                fm.damageSkill = BattleManager.instance.playerFleet[i].pilot.damageSkill;
                fm.rangeSkill = BattleManager.instance.playerFleet[i].pilot.rangeSkill;
            }

            ship.tag = "Ally";

            fm.UpdateSkills();
            BattleManager.instance.fleet.Add(ship);
        }
    }
}
