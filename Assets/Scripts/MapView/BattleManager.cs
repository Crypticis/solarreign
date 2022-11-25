using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;
    Vector3 worldLocation;

    public FleetShip[] _enemyFleet;
    public FleetShip[] playerFleet;

    public List<GameObject> enemyFleet = new List<GameObject>();
    public List<GameObject> fleet = new List<GameObject>();
    public List<FleetShip> newFleet = new List<FleetShip>();

    public Faction enemyFaction;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Update()
    {
        for (int i = fleet.Count - 1; i >= 0; i--)
        {
            if (fleet[i] == null)
            {
                fleet[i] = fleet[fleet.Count - 1];
                fleet.RemoveAt(fleet.Count - 1);
            }
        }

        for (int i = enemyFleet.Count - 1; i >= 0; i--)
        {
            if (enemyFleet[i] == null)
            {
                enemyFleet[i] = enemyFleet[enemyFleet.Count - 1];
                enemyFleet.RemoveAt(enemyFleet.Count - 1);
            }
        }
    }

    public void LoadBattleScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadFleet(Fleet fleet)
    {
        _enemyFleet = fleet.fleet.ToArray();
        //Debug.Log("3");
    }
    public void LoadSettlementFleet(SettlementFleet fleet)
    {
        _enemyFleet = fleet.fleet.ToArray();
    }

    public void LoadPlayerFleet()
    {
        playerFleet = Player.playerInstance.fleet.fleet.ToArray();
        //Debug.Log("5");
    }

    public void ReloadFleet()
    {
        Player.playerInstance.fleet.fleet = newFleet;
    }

    public void ConvertFleet()
    {
        newFleet = new List<FleetShip>();
        for (int i = 0; i < fleet.Count; i++)
        {
            var temp = new FleetShip();
            //temp.ship = GameManager.instance.database.GetShip[fleet[i].name];
            //temp.attack = fleet[i].GetComponent<FleetMember>().power;
            //temp.health = fleet[i].GetComponent<DamageHandler>().health;
            temp.pilot.level = fleet[i].GetComponent<FleetMember>().level;

            temp.pilot.skillpoints = fleet[i].GetComponent<FleetMember>().skillpoints;

            newFleet.Add(temp);
        }
    }

    public void ClearFleets()
    {
        newFleet.Clear();
    }
}
