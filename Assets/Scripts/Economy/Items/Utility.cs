using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility : MonoBehaviour
{
    public UtilityType utilityType;
    public ModuleSize moduleSize;

    public float range;
}

public enum UtilityType
{
    capacitor,
}
