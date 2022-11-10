using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeRotation : MonoBehaviour
{
    [ContextMenu("Randomize Rotation")]
    void RandomizeRotations()
    {
        this.transform.rotation = Random.rotation;
    }
}
