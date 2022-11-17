using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDamageHandler : DamageHandler
{
    public override void Defeat(GameObject killer)
    {
        if (deathParticle != null)
            Instantiate(deathParticle, this.transform.position, Quaternion.identity);

        if (isPlayer == false && killer == player)
        {
            //StatManager.instance.currentMoney += Random.Range(minMoneyReward, maxMoneyReward);

            Ticker.Ticker.AddItem((string.Format("<color=#0984e3>{0}</color> destroyed <color=#d63031>{1}</color>", (killer.GetComponent<PlayerInfo>().playerInfoObject.Name).ToString(), (GetComponent<HUDElements>().name).ToString())), 5f, Color.white);

            StatManager.instance.level.AddExp(experienceReward);
            StatManager.instance.currentMoney += moneyReward;

            StatManager.instance.GetRelation(GetComponent<FleetFaction>().faction).relation -= 10;
            Ticker.Ticker.AddItem((string.Format("Relation with {0} reduced by 10. Relation is now: {1}", (GetComponent<FleetFaction>().faction.name), StatManager.instance.GetRelation(GetComponent<FleetFaction>().faction).relation)), 5f, Color.white);

            if (experienceReward != 0)
                Ticker.Ticker.AddItem(string.Format("Gained <color=#00b894>{0}</color> experience.", (experienceReward).ToString()), 5f, Color.white);

            QuestManager.instance.ObjectiveUpdate(GetComponent<HUDElements>().name);
        }

        if (killer != player && killer.GetComponent<FleetMember>())
        {
            //if(GetComponent<Targeting>().isAlly)
            //    Ticker.Ticker.AddItem(string.Format(shooter.GetComponent<HUDElements>().name + " defeated " + this.GetComponent<HUDElements>().name + "."), 5f, Color.white);
            killer.GetComponent<FleetMember>().level.AddExp(experienceReward);
        }

        Destroy(gameObject);
    }
}
