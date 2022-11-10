using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class POIManager : MonoBehaviour
{
    public static POIManager instance;

    public List<POI> pois = new List<POI>();

    private void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < pois.Count; i++)
        {
            if(pois[i].sceneName == scene.name)
            {
                GameObject go = Instantiate(pois[i].prefab, pois[i].location, Quaternion.identity);
            }
        }
    }

    public void RemovePOI(string name)
    {
        for (int i = 0; i < pois.Count; i++)
        {
            if (pois[i].name == name)
            {
                pois.Remove(pois[i]);
            }
        }
    }

    public void AddPOI(string Name, GameObject prefab, Vector3 Location, string SceneName, string prefix, int maxNPCs)
    {
        POI poi = new POI
        {
            name = Name,
            prefab = prefab,
            location = Location,
            sceneName = SceneName,

            prefix = prefix,
            maxNPC = maxNPCs,
        };

        pois.Add(poi);
    }

    [System.Serializable]
    public struct POI
    {
        public string name;
        public GameObject prefab;
        public Vector3 location;
        public string sceneName;

        public string prefix;
        public int maxNPC;
    }
}
