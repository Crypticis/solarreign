using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemInfoObject : ScriptableObject
{
    public List<FleetObject> fleets = new List<FleetObject>();
}

[System.Serializable]
public class FleetObject
{
    public GameObject prefab;
    public Vector3 position;

    public Fleet fleet;
}
