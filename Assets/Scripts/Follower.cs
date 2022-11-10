using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public Camera ReferenceCamera;
    public Transform TargetTransform;

    void Update()
    {
        Vector3 screenPos = ReferenceCamera.WorldToScreenPoint(TargetTransform.position);
        GetComponent<RectTransform>().anchoredPosition = screenPos;
    }
}
