using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Mission
{
    public string name;
    public string description;

    public Objective[] objectives;

    public float moneyReward;

    public bool completed;

    public void CheckObjectives(string nameOfObjective)
    {
        for (int i = 0; i < objectives.Length; i++)
        {
            objectives[i].AddObjective(nameOfObjective);
        }

        for (int i = 0; i < objectives.Length; i++)
        {
            if (objectives[i].completed == false)
                return;
        }

        CompleteMission();
    }

    public void CompleteMission()
    {
        completed = true;
        StatManager.instance.UpdateMoney(moneyReward);

        Ticker.Ticker.AddItem(name + " mission completed. Awarded " + moneyReward + " .", 5f, Color.white);

        QuestManager.instance.RemoveMission(name);
    }
}

[Serializable]
public class Objective
{
    public ObjectiveType objectiveType;

    public string objectiveName;

    public int neededAmount;
    public int currentAmount;

    public bool completed;

    public void AddObjective(string nameOfObjective)
    {
        if(nameOfObjective == objectiveName)
        {
            currentAmount++;
        }

        if(currentAmount >= neededAmount)
        {
            CompleteObjective();
        }
    }

    public void CompleteObjective()
    {
        completed = true;
    }

    public enum ObjectiveType
    {
        kill,
        collect,
        locate
    }
}
