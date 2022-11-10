using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GoalLineController : MonoBehaviour
{

    public UnitController unitController;
    public List<GameObject> units = new List<GameObject>();

    public void Start()
    {
        Invoke("FindUnits", .5f);
    }

    void Update()
    {
        for (int i = 0; i < units.Count; i++)
        {
            if (units[i] == null)
            {
                units[i] = units[units.Count - 1];
                units.RemoveAt(units.Count - 1);
            }
        }

        if (unitController.inStrategyMode)
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].GetComponentInChildren<LineRenderer>().enabled = true;
                units[i].GetComponentInChildren<GoalLine>().enabled = true;
            }
        } 
        else
        {
            for (int i = 0; i < units.Count; i++)
            {
                units[i].GetComponentInChildren<LineRenderer>().enabled = false;
                units[i].GetComponentInChildren<GoalLine>().enabled = false;
            }
        }
    }

    public void FindUnits()
    {
        units = GameObject.FindGameObjectsWithTag("Ally").ToList();
    }
}
