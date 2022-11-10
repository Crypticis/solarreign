using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeUI : MonoBehaviour
{
    public float time;
    public TMP_Text timeText;

    private void Update()
    {
        time += Time.deltaTime;

        timeText.text = (string.Format("{0}", time.ToString("0")));
    }
}
