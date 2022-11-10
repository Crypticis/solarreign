using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPC", menuName = "NPCs/NPC")]
[System.Serializable]
public class NPC : ScriptableObject
{
    public string Name;
    public int ID;
    public string description;
    public float money;
    public float defaultMoney;

    public LeaderTrait leaderTrait;
    public Rank rank;
    public Personality personality;

    public void ResetMoney()
    {
        money = defaultMoney;
    }

    public enum LeaderTrait
    {
        pacifist,
        warmonger,
        economist,
    }
    public enum Personality
    {
        pragmatist,
        gloryseeker,
        loyalist,
    }
    public enum Rank
    {
        commander,
        captain,
        viceAdmiral,
        admiral,
    }
}
