using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionMember : MonoBehaviour
{
    public ShipType shipType;
    public int factionPower;
}

public enum ShipType
{
    fighter,
    bomber,
    cruiser,
    heavyFighter,
    dreadnought
}
