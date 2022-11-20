using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Info", menuName = "Player Info Object")]
[System.Serializable]
public class PlayerInfoObject : ScriptableObject
{
    public string Name;

    public ShipType shipType;
}

public enum ShipType
{
    fighter,
    bomber,
    cruiser,
    heavyFighter,
    dreadnought
}
