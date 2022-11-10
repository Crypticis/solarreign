using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FadeIn : MonoBehaviour
{
    public void FadeInUI()
    {
        LeanTween.alphaCanvas(this.GetComponent<CanvasGroup>(), 1f, .25f);
    }

    //public void FadeOutUI()
    //{
    //    LeanTween.alphaCanvas(this.GetComponent<CanvasGroup>(), 0f, 1f);
    //}
}
