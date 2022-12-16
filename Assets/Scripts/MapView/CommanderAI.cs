using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommanderAI : MonoBehaviour
{
    public enum ShipCommanderAIState
    {
        wandering,
        recruiting,
        following,
        pursuing,
        hunting,
    }

    public virtual void Defeat()
    {

    }
}
