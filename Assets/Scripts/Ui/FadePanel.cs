using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public float targetAlpha;
    public float rate = .1f;

    public void Reset()
    {
        var imageColor = GetComponent<Image>().color;
        imageColor.a = 0;
        GetComponent<Image>().color = imageColor;
    }

    void Update()
    {
        var imageColor = GetComponent<Image>().color;
        imageColor.a = Mathf.Lerp(imageColor.a, targetAlpha / 255, rate * Time.unscaledDeltaTime);
        GetComponent<Image>().color = imageColor;
    }
}
