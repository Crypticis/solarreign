using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDElements : MonoBehaviour
{
    public new string name;
    public Sprite icon;
    public Sprite iconModel;
    public bool inOverlay = false;
    public bool inFleet = false;
    private Transform player;
    public Transform slot;
    public float recruitmentCost;

    //[ContextMenu("Set Name To Parent Name")]
    public void SetToParentName()
    {
        this.name = transform.parent.transform.name;
    }
}
