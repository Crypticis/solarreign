using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    [SerializeField] private GameObject unitGameObject;
    public GameObject[] NPC;

    private void Awake() 
    {
        SaveSystem.Init();
    }

    private void Save() {
        Vector3 playerPosition = Player.playerInstance.transform.position;
        float money = StatManager.instance.currentMoney;

        SaveObject saveObject = new SaveObject {
            money = money,
            playerPosition = playerPosition
        };

        foreach (GameObject targetGameObject in NPC)
        {
            Fleet fleet = targetGameObject.GetComponent<Fleet>();

            saveObject.npcPositions.Add(fleet.transform.position);
        }

        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);
    }

    private void Load() {
        string saveString = SaveSystem.Load();
        if (saveString != null) {

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            Player.playerInstance.transform.position = saveObject.playerPosition;
            StatManager.instance.currentMoney = saveObject.money;

            for (int i = 0; i < saveObject.npcPositions.Count; i++)
            {
                Fleet fleet = NPC[i].GetComponent<Fleet>();
                fleet.transform.position = saveObject.npcPositions[i];
            }

        } else
        {

        }
    }


    private class SaveObject {
        public List<Vector3> npcPositions = new List<Vector3>();
        public List<int> npcFaction = new List<int>();
        public float money;
        public Vector3 playerPosition;
    }
}