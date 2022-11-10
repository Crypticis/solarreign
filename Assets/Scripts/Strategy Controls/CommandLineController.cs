using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandLineController : MonoBehaviour
{
    public GameObject commandLinePrefab;
    public Transform commandPlane;

    public List<GameObject> commandLines = new List<GameObject>();

    void Update()
    {
        for (int i = 0; i < commandLines.Count; i++)
        {
            if (commandLines[i] == null)
            {
                commandLines[i] = commandLines[commandLines.Count - 1];
                commandLines.RemoveAt(commandLines.Count - 1);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ally" || other.tag == "Player" || other.tag == "Enemy" || other.tag == "Environment")
        {
            GameObject go = Instantiate(commandLinePrefab, this.transform);
            go.GetComponent<CommandLine>().go = other.transform;
            go.GetComponent<CommandLine>().plane = commandPlane;

            commandLines.Add(go);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < commandLines.Count; i++)
        {
            if (commandLines[i].GetComponent<CommandLine>().go == other.transform)
            {
                Destroy(commandLines[i]);
            }
        }
    }
}
