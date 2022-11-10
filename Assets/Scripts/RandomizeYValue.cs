using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeYValue : MonoBehaviour
{
    public int range;

    [ContextMenu("Randomize Y Value")]
    void RandomizeY()
    {
        var value = Random.Range(-range, range);
        transform.position = new Vector3(transform.position.x, value, transform.position.z);
        Debug.Log(value);
    }
}
