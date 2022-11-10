using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScanTarget : MonoBehaviour
{
    public TargetingManager targeting;
    public float scanTimer = 0;

    public GameObject scanLoadingBar;

    void Update()
    {
        if(targeting.target && targeting.target.GetComponent<ScannableHUDElements>() && !targeting.target.GetComponent<ScannableHUDElements>().scanned)
        {
            if (Input.GetKey(KeyCode.F))
            {
                scanTimer += Time.deltaTime;

                scanLoadingBar.SetActive(true);
                scanLoadingBar.GetComponent<Slider>().value = scanTimer / targeting.target.GetComponent<ScannableHUDElements>().requiredTimeToScan;

                if (Input.GetKey(KeyCode.F) && scanTimer > targeting.target.GetComponent<ScannableHUDElements>().requiredTimeToScan)
                {
                    targeting.target.GetComponent<ScannableHUDElements>().scanned = true;

                    scanLoadingBar.SetActive(false);
                    scanLoadingBar.GetComponent<Slider>().value = 0;
                }
            }
            else
            {
                scanTimer = 0;

                scanLoadingBar.SetActive(false);
                scanLoadingBar.GetComponent<Slider>().value = 0;
            }
        }
    }
}
