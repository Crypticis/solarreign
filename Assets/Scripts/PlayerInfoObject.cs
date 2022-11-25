using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Info", menuName = "Player Info Object")]
[System.Serializable]
public class PlayerInfoObject : ScriptableObject
{
    public string Name;

    public ShipType shipType;

    public int shipID;
}

public enum ShipType
{
    corvette,
    destroyer,
    cruiser,
    battlecruiser,
    battleship,
    dreadnought
}
