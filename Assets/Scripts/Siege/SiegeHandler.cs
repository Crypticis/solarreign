using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiegeHandler : MonoBehaviour
{
    public GameObject[] shieldCells;
    public GameObject shield;

    public void Update()
    {
        for (int i = 0; i < shieldCells.Length; i++)
        {
            if(shieldCells[i] != null)
            {
                return;
            }
        }

        shield.SetActive(false);
    }
}
