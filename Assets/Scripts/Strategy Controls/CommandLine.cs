using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLine : MonoBehaviour
{
    public LineRenderer lr;

    public Transform go;
    public Transform plane;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        lr.positionCount = 2;
    }

    void Update()
    {
        if (!go)
        {
            Destroy(gameObject);
        }

        lr.SetPosition(0, go.position);
        lr.SetPosition(1, new Vector3(go.position.x, plane.position.y, go.position.z));
    }
}
