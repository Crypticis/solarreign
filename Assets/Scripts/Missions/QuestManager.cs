using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestManager : MonoBehaviour
{
    public GameObject interactionPrefab;

    public static QuestManager instance;

    public List<Mission> missions = new List<Mission>();

    //public Mission[] missions;

    private void Awake()
    {
        instance = this;
    }

    public void AddNewMission(string missionName, string missionDesc, Objective[] objectives, float moneyReward)
    {
        Mission mission = new Mission
        {
            name = missionName,
            description = missionDesc,
            objectives = objectives,
            moneyReward = moneyReward
        };

        missions.Add(mission);
    }

    public void AddNewMission(string missionName, string missionDesc, Objective[] objectives, float moneyReward, string prefix)
    {
        Mission mission = new Mission
        {
            name = missionName,
            description = missionDesc,
            objectives = objectives,
            moneyReward = moneyReward
        };

        for (int i = 0; i < objectives.Length; i++)
        {
            if(objectives[i].objectiveType == Objective.ObjectiveType.kill)
            {
                SpawnRandomPirateInteraction(prefix, objectives[i].neededAmount);
            }
        }

        missions.Add(mission);
    }

    public void RemoveMission(string missionName)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            if(missions[i].name == missionName)
            {
                missions.RemoveAt(i);
            }
        }
    }

    public void ObjectiveUpdate(string nameOfObjective)
    {
        for (int i = 0; i < missions.Count; i++)
        {
            missions[i].CheckObjectives(nameOfObjective);
        }
    }

    public void SpawnRandomPirateInteraction(string prefix, int amount)
    {
        Vector3 location = new Vector3(0 + Random.insideUnitSphere.x * 30000, 0 + Random.insideUnitSphere.y * 30000, 0 + Random.insideUnitSphere.z * 30000);

        GameObject go = Instantiate(interactionPrefab, location, Quaternion.identity);
        go.GetComponent<HUDElements>().name = prefix + " Pirate Hideout";
        go.GetComponent<PirateHideout>().maxPirates = amount;
        go.GetComponent<PirateHideout>().prefix = prefix;

        //for (int i = 0; i < amount; i++)
        //{
        //    PirateSpawner.instance.SpawnPirateAt(go.transform, prefix);
        //}

        POIManager.instance.AddPOI("Pirate Hideout", interactionPrefab, location, SceneManager.GetActiveScene().name, prefix, amount);
    }
}
