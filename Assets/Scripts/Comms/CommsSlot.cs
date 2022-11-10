using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CommsSlot : MonoBehaviour
{
    public GameObject commsTarget;

    private void Update()
    {
        if (!commsTarget)
            Destroy(this.gameObject);
    }
}
