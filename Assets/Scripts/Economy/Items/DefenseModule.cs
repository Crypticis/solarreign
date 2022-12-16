using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseModule : MonoBehaviour
{
    public Defense defense;

    [SerializeField] GameObject pointDefenseTurret;

    public void SetPointDefense()
    {
        pointDefenseTurret.gameObject.SetActive(true);
    }

    public void SetNotPointDefense()
    {
        pointDefenseTurret.gameObject.SetActive(false);
    }
}
