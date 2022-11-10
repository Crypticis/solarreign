using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    public static NPCManager instance;
    public List<UniqueNPC> uniqueNPCs = new List<UniqueNPC>();

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        var temp = GameObject.FindGameObjectsWithTag("NPC");
        for (int i = 0; i < temp.Length; i++)
        {
            uniqueNPCs.Add(temp[i].GetComponent<UniqueNPC>());
        }
        for (int i = 0; i < uniqueNPCs.Count; i++)
        {
            uniqueNPCs[i].ID = i;
        }
    }
}
