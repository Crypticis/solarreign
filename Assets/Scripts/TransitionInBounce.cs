using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionInBounce : MonoBehaviour
{
    public float easeTime = .1f;

    void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), easeTime).setIgnoreTimeScale(true).setEaseInBounce();
    }
}
