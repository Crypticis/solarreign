using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInfo : MonoBehaviour
{
    public AsteroidType type;
}

public enum AsteroidType
{
    pure,
    rich,
    neutral
}
