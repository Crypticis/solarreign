using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionInWindowOpen : MonoBehaviour
{
    void OnEnable()
    {
        LeanTween.scaleY(this.gameObject, 1, .5f).setIgnoreTimeScale(true).setEaseInBounce();
        LeanTween.scaleX(this.gameObject, 1, .25f).setIgnoreTimeScale(true).setEaseInBounce();
    }

}