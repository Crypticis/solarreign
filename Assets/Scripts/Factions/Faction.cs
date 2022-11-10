using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Faction", menuName = "FactionSystem/Faction")]
public class Faction : ScriptableObject
{
    [Header("Details")]
    public string name;
    [Multiline()]
    public string description;
    public Sprite icon;
    public bool playerInFaction = false;
    public float resources;

    [Header("Colorization")]

    public Color factionColor;
    public Material materialSmooth;
    public Material materialDetail;

    [Header("Leadership")]

    public NPC leader;
    public NPC defaultLeader;
    public bool isPlayerLeader, isPlayerFaction;

    [Header("Diplomacy")]

    public List<Faction> enemies = new List<Faction>();
    public List<Faction> allies = new List<Faction>();

    public GameObject[] shipPrefabs;

    public List<Faction> defaultEnemies = new List<Faction>();
    public List<Faction> defaultAllies = new List<Faction>();

    public void Reset()
    {
        enemies = defaultEnemies;
        allies = defaultAllies;

        if(defaultLeader)
            leader = defaultLeader;
    }

    public bool IsEnemy(Faction faction)
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if(faction == enemies[i])
            {
                return true;
            }
        }

        return false;
    }
}
