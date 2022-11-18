using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCommand : MonoBehaviour
{
    [SerializeField] PlayerFleet playerFleet;
    [SerializeField] TargetingManager targetingManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && targetingManager.target)
        {
            for (int i = 0; i < playerFleet.fleet.Count; i++)
            {
                playerFleet.fleet[i].ship.GetComponent<Targeting>().SetTarget(targetingManager.target.gameObject);
            }
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            for (int i = 0; i < playerFleet.fleet.Count; i++)
            {
                playerFleet.fleet[i].ship.GetComponent<Targeting>().RemoveTarget();
            }
        }
    }
}
