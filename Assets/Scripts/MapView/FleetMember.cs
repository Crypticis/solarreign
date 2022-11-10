using GNB;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class FleetMember : MonoBehaviour
{
    public float power;
    public float upkeep;
    public float fuelUpkeep;

    public Level level;
    public int skillpoints;

    public int speedSkill;
    public int firespeedSkill;
    public int durabilitySkill;
    public int damageSkill;
    public int rangeSkill;

    public Gun[] guns;
    public LaserAI[] lasers;
    public AAHardpoint[] missiles;
    public DamageHandler damageHandler;
    public LineOfSightMulti lineOfSight;
    public AutonomousVehicle vehicle;

    public void UpdateSkills()
    {
        guns = GetComponentsInChildren<Gun>();
        lasers = GetComponentsInChildren<LaserAI>();
        missiles = GetComponentsInChildren<AAHardpoint>();
        damageHandler = GetComponentInChildren<DamageHandler>();
        lineOfSight = GetComponentInChildren<LineOfSightMulti>();
        vehicle = GetComponentInChildren<AutonomousVehicle>();

        vehicle.MaxSpeed += 0.5f * speedSkill;

        lineOfSight.shootingRange += 10 * rangeSkill;

        damageHandler.maxHealth += 10 * durabilitySkill;

        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].FireDelay -= .0025f * firespeedSkill;
            guns[i].damageBonus += 0.25f * damageSkill;
        }

        for (int i = 0; i < lasers.Length; i++)
        {
            lasers[i].bonusDamage += .001f * damageSkill;
        }

        for (int i = 0; i < missiles.Length; i++)
        {
            missiles[i].fireDelay -= .025f * firespeedSkill;
            missiles[i].bonusDamage += 1f * damageSkill;

            missiles[i].ResetLauncher();
        }
    }

    public void OnLevelUp()
    {
        skillpoints++;
    }
}
