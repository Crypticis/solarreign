using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnitySteer.Behaviors;

public class GoalLine : MonoBehaviour
{
    public LineRenderer lr;
    //private Transform[] points;

    public Vector3 goalPosition;

    public Targeting targeting;
    public SteerForPoint point;
    public SteerToFollow follow;


    public Gradient targetColor, pointColor, followColor;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        point = GetComponentInParent<SteerForPoint>();
        follow = GetComponentInParent<SteerToFollow>();
        targeting = GetComponentInParent<Targeting>();
    }

    private void Start()
    {
        lr.positionCount = 2;
    }

    void Update()
    {
        lr.SetPosition(0, this.transform.position);

        if (targeting.target)
        {
            goalPosition = targeting.target.transform.position;
            lr.colorGradient = targetColor;
        }
        else if (follow.Target && follow.enabled == true)
        {
            goalPosition = follow.Target.position;
            lr.colorGradient = followColor;
        } 
        else
        {
            goalPosition = point.TargetPoint;
            lr.colorGradient = pointColor;
        }

        lr.SetPosition(1, goalPosition);
    }
}
