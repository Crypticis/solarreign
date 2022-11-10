using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TargetHologramUI : MonoBehaviour
{
    public TargetingManager playerTargeting;
    public WarpTargetingManager warpTargeting;
    public MeshFilter holoMeshFilter;
    public TMP_Text nameText;
    public TMP_Text healthText;

    public CanvasGroup targetUI;

    public GameObject targetCam;

    private void Start()
    {
        StartCoroutine("UpdateHealth");
    }

    IEnumerator UpdateHealth()
    {
        while (true)
        {
            if (playerTargeting.target)
            {
                if (playerTargeting.target.GetComponent<ScannableHUDElements>())
                {
                    if (playerTargeting.target.GetComponent<ScannableHUDElements>().scanned)
                    {
                        nameText.text = playerTargeting.target.GetComponent<HUDElements>().name;
                        healthText.text = (playerTargeting.target.GetComponent<DamageHandler>().health.ToString("#") + "/" + playerTargeting.target.GetComponent<DamageHandler>().maxHealth.ToString("#"));
                    }
                    else
                    {
                        nameText.text = "Unknown";
                        healthText.text = "Unknown";
                    }
                }
                else
                {
                    healthText.text = (playerTargeting.target.GetComponent<DamageHandler>().health.ToString("#") + "/" + playerTargeting.target.GetComponent<DamageHandler>().maxHealth.ToString("#"));
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void UpdateTargetUI()
    {
        if (playerTargeting.target)
        {
            holoMeshFilter.mesh = playerTargeting.target.GetComponent<MeshFilter>().mesh;

            float objectSize;

            if(holoMeshFilter.mesh.bounds.size.z > holoMeshFilter.mesh.bounds.size.y)
            {
                objectSize = (holoMeshFilter.transform.localScale.z * holoMeshFilter.mesh.bounds.size.z) / 10f;
            }
            else
            {
                objectSize = (holoMeshFilter.transform.localScale.y * holoMeshFilter.mesh.bounds.size.y) / 10f;
            }

            holoMeshFilter.transform.localScale /= objectSize;

            if (playerTargeting.target.GetComponent<ScannableHUDElements>())
            {
                if (playerTargeting.target.GetComponent<ScannableHUDElements>().scanned)
                {
                    nameText.text = playerTargeting.target.GetComponent<HUDElements>().name;
                    healthText.text = (playerTargeting.target.GetComponent<DamageHandler>().health.ToString("#") + "/" + playerTargeting.target.GetComponent<DamageHandler>().maxHealth.ToString("#"));
                }
                else
                {
                    nameText.text = "Unknown";
                    healthText.text = "Unknown";
                }
            }
            else
            {
                nameText.text = playerTargeting.target.GetComponent<HUDElements>().name;
                healthText.text = (playerTargeting.target.GetComponent<DamageHandler>().health.ToString("#") + "/" + playerTargeting.target.GetComponent<DamageHandler>().maxHealth.ToString("#"));
            }

            targetCam.SetActive(true);

            FadeInTargetUI();
            return;
        }

        if (warpTargeting.target)
        {
            holoMeshFilter.mesh = warpTargeting.target.GetComponentInChildren<MeshFilter>().mesh;
            float objectSize = (holoMeshFilter.transform.localScale.z * holoMeshFilter.mesh.bounds.size.z) / 10f;
            holoMeshFilter.transform.localScale /= objectSize;

            if (warpTargeting.target.GetComponent<HUDElements>())
            {
                nameText.text = warpTargeting.target.GetComponent<HUDElements>().name;
            }
            else if(warpTargeting.target.GetComponent<SettlementInfo>())
            {
                nameText.text = warpTargeting.target.GetComponent<SettlementInfo>().Name;
            }

            healthText.text = "Hold 'F' to Warp.";

            targetCam.SetActive(true);

            FadeInTargetUI();
            return;
        }

        FadeOutTargetUI();
    }

    public void FadeInTargetUI()
    {
        LeanTween.alphaCanvas(targetUI, 1f, .25f).setIgnoreTimeScale(true);
    }

    public void FadeOutTargetUI()
    {
        LeanTween.alphaCanvas(targetUI, 0f, .25f).setIgnoreTimeScale(true);
        targetCam.SetActive(false);
    }
}
