using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargePanel : MonoBehaviour
{
    public float timeToScale = .1f;

    void Awake()
    {
        this.transform.localScale = new Vector3(0, 0, 0);
    }

    public void Enlarge()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), timeToScale).setIgnoreTimeScale(true);
    }

    public void Shrink()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), .001f).setIgnoreTimeScale(true);
        GetComponentInChildren<FadePanel>().Reset(); 
    }
}
