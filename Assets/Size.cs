using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    public float objectSize;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        objectSize = GetComponent<MeshFilter>().mesh.bounds.size.z * transform.localScale.z;

    }
}
