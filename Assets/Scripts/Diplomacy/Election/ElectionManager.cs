using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ElectionManager : MonoBehaviour
{
    public float electionTimer = 0;
    public PlayerFaction faction;

    public float influenceCost = 100;
    public float influencePurchaseCost = 1000f;

    //public int votes;

    List<NPCVote> gsfVotesTemp = new List<NPCVote>();
    List<NPCVote> zerogVotesTemp = new List<NPCVote>();

    public NPC[] zerog;
    public NPC[] gsf;

    public NPCVote[] gsfVotes;
    public NPCVote[] zerogVotes;

    public GameObject electionUI;
    public Transform electionPrefabRoot;

    public GameObject electionPrefab;

    public float electionTime = 18000;

    public Button influenceButton;

    public TMP_Text influenceText;
    public TMP_Text voteText;

    public List<GameObject> slots = new List<GameObject>();

    public void Start()
    {
        for (int i = 0; i < zerog.Length; i++)
        {
            var temp = new NPCVote();
            temp.npc = zerog[i];
            temp.votes = 1;
            zerogVotesTemp.Add(temp);
        }

        for (int i = 0; i < gsf.Length; i++)
        {
            var temp = new NPCVote();
            temp.npc = gsf[i];
            temp.votes = 1;
            gsfVotesTemp.Add(temp);
        }

        gsfVotes = gsfVotesTemp.ToArray();
        zerogVotes = zerogVotesTemp.ToArray();
    }

    void Update()
    {
        if(faction.faction.name == "Zero-G Corp.")
        {
            electionTimer += Time.deltaTime;

            if (electionTimer >= electionTime)
            {
                ElectionStart();

                electionTimer = 0f;
            }
        }
    }

    public void ElectionStart()
    {
        electionUI.SetActive(true);

        Time.timeScale = 0f;

        influenceText.text = StatManager.instance.influence.ToString();
        voteText.text = StatManager.instance.votes.ToString();

        for (int i = 0; i < electionPrefabRoot.childCount; i++)
        {
            Destroy(electionPrefabRoot.GetChild(i).gameObject);
        }

        slots.Clear();

        StatManager.instance.votes = 1;

        for (int i = 0; i < 100; i++)
        {
            if (faction.faction == FactionManager.instance.factions[0])
            {
                var index = Random.Range(0, zerogVotes.Length);
                zerogVotes[index].votes++;
            }
            else if (faction.faction == FactionManager.instance.factions[1])
            {
                var index = Random.Range(0, gsfVotes.Length);
                gsfVotes[index].votes++;
            }
        }

        // Zero-G
        
        if (faction.faction == FactionManager.instance.factions[0])
        {
            influenceButton.interactable = true;

            for (int i = 0; i < zerogVotes.Length; i++)
            {
                var go = Instantiate(electionPrefab, electionPrefabRoot);
                go.transform.Find("Name").GetComponent<TMP_Text>().text = zerogVotes[i].npc.Name;
                go.transform.Find("Votes").GetComponent<TMP_Text>().text = zerogVotes[i].votes.ToString();
                go.transform.Find("Trait").GetComponent<TMP_Text>().text = zerogVotes[i].npc.leaderTrait.ToString();
                var temp = i;
                go.GetComponentInChildren<Button>().onClick.AddListener(() => BuyVoteFor(temp));

                slots.Add(go);
            }
        } // GSF
        else if (faction.faction == FactionManager.instance.factions[1])
        {
            influenceButton.interactable = false;

            for (int i = 0; i < gsfVotes.Length; i++)
            {
                var go = Instantiate(electionPrefab, electionPrefabRoot);
                go.transform.Find("Name").GetComponent<TMP_Text>().text = gsfVotes[i].npc.Name;
                go.transform.Find("Votes").GetComponent<TMP_Text>().text = gsfVotes[i].votes.ToString();
                go.transform.Find("Trait").GetComponent<TMP_Text>().text = gsfVotes[i].npc.leaderTrait.ToString();
                var temp = i;
                go.GetComponentInChildren<Button>().onClick.AddListener(() => BuyVoteFor(temp));

                slots.Add(go);
            }
        }
    }

    public void ElectionEnd()
    {
        electionUI.SetActive(false);

        // Zero-G
        if (faction.faction == FactionManager.instance.factions[0])
        {
            var temp = zerogVotes.ToList();

            temp = temp.OrderBy(x => x.votes).ToList();
            temp.Reverse();

            if (StatManager.instance.votes > temp[0].votes)
            {
                FactionManager.instance.factions[0].isPlayerLeader = true;
                FactionManager.instance.factions[0].leader = null;

                Ticker.Ticker.AddItem("You have become the leader of the Zero-G Corp.");
            }
            else
            {
                FactionManager.instance.factions[0].leader = temp[0].npc;
                FactionManager.instance.factions[0].isPlayerLeader = false;

                Ticker.Ticker.AddItem(temp[0].npc.Name + " has become the leader of the Zero-G Corp.");
            }

        } // GSF
        else if (faction.faction == FactionManager.instance.factions[1])
        {
            var temp = gsfVotes.ToList();

            temp = temp.OrderBy(x => x.votes).ToList();
            temp.Reverse();

            if (StatManager.instance.votes > temp[0].votes)
            {
                FactionManager.instance.factions[1].isPlayerLeader = true;
                FactionManager.instance.factions[1].leader = null;

                Ticker.Ticker.AddItem("You have become the leader of the GSF.");
            }
            else
            {
                FactionManager.instance.factions[1].leader = temp[0].npc;
                FactionManager.instance.factions[1].isPlayerLeader = false;

                Ticker.Ticker.AddItem(temp[0].npc.Name + " has become the leader of the GSF.");
            }
        }

        Time.timeScale = GameManager.instance.speedMultiplier;

        StatManager.instance.votes = 1;

        for (int i = 0; i < zerogVotes.Length; i++)
        {
            zerogVotes[i].votes = 1;
        }

        for (int i = 0; i < gsfVotes.Length; i++)
        {
            gsfVotes[i].votes = 1;
        }

        slots.Clear();
    }

    public void BuyVote()
    {
        if(StatManager.instance.influence >= influenceCost)
        {
            StatManager.instance.influence -= influenceCost;

            StatManager.instance.votes++;
        }

        influenceText.text = StatManager.instance.influence.ToString();
        voteText.text = StatManager.instance.votes.ToString();
    }

    public void BuyVoteFor(int index)
    {
        if (faction.faction == FactionManager.instance.factions[0])
        {
            if (StatManager.instance.influence >= influenceCost)
            {
                StatManager.instance.influence -= influenceCost;

                zerogVotes[index].votes++;
            }
        } // GSF
        else if (faction.faction == FactionManager.instance.factions[1])
        {
            if (StatManager.instance.influence >= influenceCost)
            {
                StatManager.instance.influence -= influenceCost;

                gsfVotes[index].votes++;
            }
        }

        UpdateVotes();

        influenceText.text = StatManager.instance.influence.ToString();
        voteText.text = StatManager.instance.votes.ToString();
    }

    public void BuyInfluence()
    {
        if(StatManager.instance.currentMoney >= influencePurchaseCost)
        {
            StatManager.instance.currentMoney -= influencePurchaseCost;

            StatManager.instance.influence += 100;

            influencePurchaseCost *= 1.1f;
        }

        influenceText.text = StatManager.instance.influence.ToString();
        voteText.text = StatManager.instance.votes.ToString();
    }

    public void UpdateVotes()
    {
        if (faction.faction == FactionManager.instance.factions[0])
        {
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].transform.Find("Votes").GetComponent<TMP_Text>().text = zerogVotes[i].votes.ToString();
            }

        } // GSF
        else if (faction.faction == FactionManager.instance.factions[1])
        {

            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].transform.Find("Votes").GetComponent<TMP_Text>().text = gsfVotes[i].votes.ToString();
            }

        }
    }

    [System.Serializable]
    public struct NPCVote
    {
        public NPC npc;
        public int votes;
    }
}
