using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUI : MonoBehaviour
{
    public Transform missionUI;
    public Mission activeMission;
    public GameObject missionSlotPrefab;

    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public TMP_Text objectiveText;
    public TMP_Text objectiveTypeText;
    public TMP_Text objectiveAmountText;

    public GameObject missionOverview;
    // Update is called once per frame

    public void OnEnable()
    {
        missionOverview.SetActive(false);
        CreateUI();
    }

    void CreateUI()
    {
        for (int i = 0; i < QuestManager.instance.missions.Count; i++)
        {
            Mission mission = QuestManager.instance.missions[i];

            GameObject go = Instantiate(missionSlotPrefab, missionUI);
            go.GetComponent<MissionSlot>().mission = mission;
            go.GetComponentInChildren<TMP_Text>().text = mission.name;

            go.GetComponentInChildren<Button>().onClick.AddListener(() => SelectMission(mission));
        }
    }

    public void SelectMission(Mission mission)
    {
        if(activeMission == mission)
        {
            missionOverview.SetActive(false);
            activeMission = null;
            return;
        }

        activeMission = mission;

        missionOverview.SetActive(true);

        nameText.text = mission.name;
        descriptionText.text = mission.description;
        objectiveText.text = mission.objectives[0].objectiveName;
        objectiveTypeText.text = mission.objectives[0].objectiveType.ToString();
        objectiveAmountText.text = mission.objectives[0].currentAmount.ToString() + " / " + mission.objectives[0].neededAmount.ToString();
    }

    public void RemoveMission()
    {
        QuestManager.instance.RemoveMission(activeMission.name);

        missionOverview.SetActive(false);
    }
}
