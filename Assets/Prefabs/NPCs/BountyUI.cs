using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BountyUI : MonoBehaviour
{
    public List<Bounty> availableBounties = new List<Bounty>();
    public string[] prefixes;
    public GameObject bountyPrefab;
    public int maxBounties;

    public void GenerateBounties(int bountiesToCreate)
    {
        for (int i = 0; i < bountiesToCreate; i++)
        {
            string prefix = prefixes[Random.Range(0, prefixes.Length)];

            int amount = Random.Range(1, 2);

            Mission mission = new Mission
            {
                name = "Bounty for " + prefix + " Pirate Leader",
                description = "Hunt down " + amount + " " + prefix + " pirate leader[s] at their hideout.",
                objectives = new Objective[1],
                moneyReward = Random.Range(2000, 5000),
            };

            mission.objectives[0] = new Objective
            {
                objectiveType = Objective.ObjectiveType.kill,
                objectiveName = prefix + " Pirate Leader",
                neededAmount = amount,
                currentAmount = 0,
                completed = false,
            };

            Bounty bounty = new Bounty
            {
                mission = mission,
                prefix = prefix,
            };

            availableBounties.Add(bounty);
        }
    }

    public void CreateUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if(availableBounties.Count < maxBounties)
        {
            GenerateBounties(maxBounties - availableBounties.Count);
        }

        for (int i = 0; i < availableBounties.Count; i++)
        {
            GameObject go = Instantiate(bountyPrefab, transform);
            go.GetComponent<BountySlot>().bountyUI = this;
            go.GetComponent<BountySlot>().bounty = availableBounties[i];
        }
    }

    public void AcceptBounty(Bounty bounty, GameObject bountySlot)
    {
        availableBounties.Remove(bounty);
        Destroy(bountySlot);

        QuestManager.instance.AddNewMission(bounty.mission.name, bounty.mission.description, bounty.mission.objectives, bounty.mission.moneyReward, bounty.prefix);
        Ticker.Ticker.AddItem("Accepted " + bounty.mission.name + ".");
    }

    public struct Bounty
    {
        public Mission mission;
        public string prefix;
    }
}
