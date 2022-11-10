using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public static HealthUI instance;

    public DamageHandler player;
    public Material healthHolo1;
    public Material healthHolo2;
    public GameObject healthHologram;
    public TMP_Text healthText;
    public TMP_Text shieldText;
    public PlayerInfoObject playerInfoObject;

    Renderer rend;
    public MeshFilter playerMeshFilter;
    MeshFilter healthMeshFilter;

    public CanvasGroup healthHologramUI;
    public GameObject healthItems;

    public GameObject healthRenderCam;
    bool healthVisible = false;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateShipModel();
        healthMeshFilter = healthHologram.GetComponent<MeshFilter>();
        rend = healthHologram.GetComponent<Renderer>();
        healthMeshFilter.mesh = playerMeshFilter.mesh;
        float objectSize = (healthMeshFilter.transform.localScale.z * healthMeshFilter.mesh.bounds.size.z) / 12f;
        healthMeshFilter.transform.localScale /= objectSize;
    }

    public void UpdateHealth()
    {
        healthRenderCam.SetActive(true);

        healthMeshFilter.mesh = playerMeshFilter.mesh;
        float healthPercent = player.health / player.maxHealth;
        //shieldText.text = string.Format("SHLD: " + "{0}" + " / " + "{1}", (int)player.currentShield, player.maxShield);
        healthText.text = string.Format("HP: " + "{0}" + " / " + "{1}", (int)player.health, player.maxHealth);
        rend.material.Lerp(healthHolo2, healthHolo1, healthPercent);
        float objectSize = (healthMeshFilter.transform.localScale.z * healthMeshFilter.mesh.bounds.size.z) / 12f;
        healthMeshFilter.transform.localScale /= objectSize;

        FadeInHealthUI();
    }

    public void UpdateShipModel()
    {
        playerMeshFilter = Player.playerInstance.transform.Find("Ships").Find(playerInfoObject.shipType.ToString()).Find("Model").GetComponent<MeshFilter>();
    }

    // Fades in Health UI hologram and invokes a fade out method in 2 seconds. Checks if already planning to fade out before invoking. Prevents flashing of health UI.
    public void FadeInHealthUI()
    {
        LeanTween.alphaCanvas(healthHologramUI, 1f, .25f).setIgnoreTimeScale(true);

        if (healthVisible != true)
            Invoke("FadeOutHealthUI", 2f);

        healthVisible = true;
    }

    public void FadeOutHealthUI()
    {
        LeanTween.alphaCanvas(healthHologramUI, 0f, .25f).setIgnoreTimeScale(true);

        healthRenderCam.SetActive(false);

        healthVisible = false;
    }
}
