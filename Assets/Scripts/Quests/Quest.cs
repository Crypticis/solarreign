using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public string Name;
    public string Description;

    public KillGoal[] killGoals;
    public MiningGoal[] miningGoals;
    public DeliveryGoal[] deliveryGoals;

    public bool isComplete = false;

    public void CheckCompletion()
    {
        for (int i = 0; i < killGoals.Length; i++)
        {
            if(killGoals[i].isComplete == false)
            {
                return;
            }
        }

        for (int i = 0; i < miningGoals.Length; i++)
        {
            if (miningGoals[i].isComplete == false)
            {
                return;
            }
        }

        for (int i = 0; i < deliveryGoals.Length; i++)
        {
            if (deliveryGoals[i].isComplete == false)
            {
                return;
            }
        }

        isComplete = true;
    }

    public void ResetQuest()
    {
        for (int i = 0; i < killGoals.Length; i++)
        {
            killGoals[i].isComplete = false;
        }

        for (int i = 0; i < miningGoals.Length; i++)
        {
            miningGoals[i].isComplete = false;
        }

        for (int i = 0; i < deliveryGoals.Length; i++)
        {
            deliveryGoals[i].isComplete = false;
        }

        isComplete = false;
    }
}
