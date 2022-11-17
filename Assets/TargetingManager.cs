using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetingManager : MonoBehaviour
{
    public List<GameObject> potentialTargets = new List<GameObject>();

    public List<GameObject> optionalTargets = new List<GameObject>();

    public Transform target;

    public Transform targetIndicator;

    //void UpdateTargets()
    //{
    //    var enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //    potentialTargets.Clear();

    //    for (int i = 0; i < enemies.Length; i++)
    //    {
    //        if (enemies[i].GetComponent<Renderer>().isVisible)
    //        {
    //            potentialTargets.Add(enemies[i]);
    //        }
    //    }

    //    if (potentialTargets.Count <= 0)
    //        return;

    //    potentialTargets = potentialTargets.OrderBy(x => (((Mathf.Abs(Camera.main.WorldToScreenPoint(x.transform.position).x  - Screen.width / 2)) + (Mathf.Abs(Camera.main.WorldToScreenPoint(x.transform.position).y - Screen.height / 2)))) / 2 ).ToList();
    //}

    public PlayerFaction playerFaction;

    // GameObject targetRenderCam;

    public TargetHologramUI targetHologramUI;

    public WarpJump warpJump;

    WarpTargetingManager warpTargeting;

    public virtual void Awake()
    {
        playerFaction = GetComponentInParent<PlayerFaction>();
        warpTargeting = transform.parent.GetComponentInChildren<WarpTargetingManager>();
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FleetFaction>())
        {
            if (playerFaction.faction.IsEnemy(other.gameObject.GetComponent<FleetFaction>().faction) && !potentialTargets.Contains(other.gameObject))
            {
                potentialTargets.Add(other.gameObject);
            }
        }

        if (other.gameObject.GetComponent<ScannableHUDElements>())
        {
            potentialTargets.Add(other.gameObject);
        }

    }

    public virtual void OnTriggerExit(Collider other)
    {
        if (potentialTargets.Contains(other.gameObject))
        {
            potentialTargets.Remove(other.gameObject);
        }
    }

    public void UpdateTargets()
    {
        optionalTargets.Clear();

        for (var i = potentialTargets.Count - 1; i > -1; i--)
        {
            if (potentialTargets[i] == null)
                potentialTargets.RemoveAt(i);
        }

        for (int i = 0; i < potentialTargets.Count; i++)
        {
            if (potentialTargets[i].GetComponentInChildren<Renderer>().isVisible)
            {
                optionalTargets.Add(potentialTargets[i]);
            }
        }

        if (optionalTargets.Count <= 0)
            return;

        optionalTargets = optionalTargets.OrderBy(x => (((Mathf.Abs(Camera.main.WorldToScreenPoint(x.transform.position).x - Screen.width / 2)) + (Mathf.Abs(Camera.main.WorldToScreenPoint(x.transform.position).y - Screen.height / 2)))) / 2).ThenByDescending(x => Vector3.Distance(x.transform.position, Player.playerInstance.transform.position)).ToList();
    }

    public virtual void AttemptTarget()
    {
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
                    warpTargeting.target = null;
                }
            }
            else
            {
                target = optionalTargets[0].transform;
                warpTargeting.target = null;
            }

            targetHologramUI.UpdateTargetUI();
        }
        catch
        {
            target = null;
            targetHologramUI.UpdateTargetUI();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Target"))
        {
            AttemptTarget();
        }

        if (target)
        {
            var heading = target.transform.position - Player.playerInstance.transform.position;

            if (Vector3.Dot(heading, Player.playerInstance.transform.forward) > 0)
            {
                targetIndicator.gameObject.SetActive(true);
                targetIndicator.transform.position = Camera.main.WorldToScreenPoint(target.position);
            }
            else
            {
                targetIndicator.gameObject.SetActive(false);
            }
        }
        else
        {
            targetIndicator.gameObject.SetActive(false);
        }
    }
}
