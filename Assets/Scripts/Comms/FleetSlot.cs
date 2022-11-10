using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FleetSlot : MonoBehaviour
{
    public GameObject commsTarget;
    public TMP_Text healthText;

    private void Update()
    {
        if (!commsTarget)
            Destroy(this.gameObject);

        if (commsTarget)
        {
            healthText.text = (commsTarget.GetComponent<DamageHandler>().health.ToString() + "/" + commsTarget.GetComponent<DamageHandler>().maxHealth.ToString());
        }
    }
}
