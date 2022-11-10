using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_FieldOfView : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "NPC" || other.gameObject.tag == "_NPC")
        {
            other.transform.Find("Text").gameObject.SetActive(true);
            LeanTween.scale(other.transform.Find("Visual").gameObject, new Vector3(1, 1, 1), 2f).setEaseOutElastic();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "_NPC")
        {
            other.transform.Find("Text").gameObject.SetActive(false);
            LeanTween.scale(other.transform.Find("Visual").gameObject, new Vector3(0, 0, 0), 2f).setEaseOutElastic();
        }
    }
}
