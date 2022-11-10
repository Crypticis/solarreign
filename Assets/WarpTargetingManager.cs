using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WarpTargetingManager : TargetingManager
{
    TargetingManager targetingManager;

    public override void Awake()
    {
        targetingManager = transform.parent.GetComponentInChildren<TargetingManager>();
    }

    public void Start()
    {
        RefreshWarpTargets();
    }

    public void RefreshWarpTargets()
    {
        var array1 = GameObject.FindGameObjectsWithTag("Settlement");
        var array2 = GameObject.FindGameObjectsWithTag("Warp Gate");
        var array3 = GameObject.FindGameObjectsWithTag("Warp Target");

        var array4 = array1.Concat(array2).Concat(array3).ToArray();

        potentialTargets = array4.ToList();
    }

    public override void OnTriggerEnter(Collider other)
    {

    }

    public override void OnTriggerExit(Collider other)
    {

    }

    public override void AttemptTarget()
    {
        RefreshWarpTargets();
        UpdateTargets();

        if (optionalTargets.Count <= 0 || warpJump.isWarping)
            return;

        try
        {
            if (target != null)
            {
                if (target == optionalTargets[0].transform)
                {
                    target = null;
                }
                else
                {
                    target = optionalTargets[0].transform;
                    targetingManager.target = null;
                }
            }
            else
            {
                if (targetingManager.target == null)
                {
                    target = optionalTargets[0].transform;
                    targetingManager.target = null;
                }
            }

            targetHologramUI.UpdateTargetUI();
        }
        catch
        {
            target = null;
            targetHologramUI.UpdateTargetUI();
        }
    }
}
