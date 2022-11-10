using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public static NavigationManager instance;
    public int currentSystemDangerRating = 0;

    public TMP_Text systemText;
    public TMP_Text dangerText;
    public TMP_Text factionText;

    public NPCDatabaseObject database;

    private void Awake()
    {
        instance = this;

        SetSystem(SceneManager.GetActiveScene().name, database.GetSystemDangerRating(SceneManager.GetActiveScene().name), database.GetSystemFaction(SceneManager.GetActiveScene().name));
    }

    public void SetSystem(string systemName, int danger, Faction faction)
    {
        //Debug.Log("Setting Name");
        GrowText();
        Invoke("ShrinkText", .25f);
        systemText.text = systemName;
        dangerText.text = danger.ToString("0.0");
        factionText.text = faction.name;

        currentSystemDangerRating = danger;
    }

    void GrowText()
    {
        LeanTween.scale(systemText.gameObject, new Vector3(2, 2, 2), .25f).setIgnoreTimeScale(true);
        LeanTween.scale(dangerText.gameObject, new Vector3(2, 2, 2), .25f).setIgnoreTimeScale(true);
        LeanTween.scale(factionText.gameObject, new Vector3(2, 2, 2), .25f).setIgnoreTimeScale(true);
    }

    void ShrinkText()
    {
        LeanTween.scale(systemText.gameObject, new Vector3(1, 1, 1), .25f).setIgnoreTimeScale(true);
        LeanTween.scale(dangerText.gameObject, new Vector3(1, 1, 1), .25f).setIgnoreTimeScale(true);
        LeanTween.scale(factionText.gameObject, new Vector3(1, 1, 1), .25f).setIgnoreTimeScale(true);
    }
}
