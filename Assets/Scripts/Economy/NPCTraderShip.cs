using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTraderShip : MonoBehaviour
{
    public CivilianCommanderAI ai;

    public void Awake()
    {
        ai = GetComponent<CivilianCommanderAI>();
    }
}
