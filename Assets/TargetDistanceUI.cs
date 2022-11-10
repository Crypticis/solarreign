using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetDistanceUI : MonoBehaviour
{
    public Transform player;
    public TMP_Text distanceText;
    public TargetingManager targeting;

    public TMP_Text nameText;

    void Update()
    {
        if(player && targeting)
        {
            if (targeting.target != null)
            {
                float distance = (targeting.target.position - player.position).magnitude * 10;
                distanceText.text = distance.ToString("n2") + "m";

                if (nameText.text != targeting.target.GetComponent<HUDElements>().name)
                    nameText.text = targeting.target.GetComponent<HUDElements>().name;
            }
            else
            {
                nameText.text = "";
            }
        }
    }
}
