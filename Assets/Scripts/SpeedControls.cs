using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControls : MonoBehaviour
{

    GameManager gm;

    public Image[] bars;

    public Color activeColor;
    public Color inactiveColor;

    int previousSpeed = 1;

    void Start()
    {
        gm = GameManager.instance;
        UpdateColor();
    }

    // Update is called once per frame
    void Update()
    {
        if (gm == null)
        {
            gm = GameManager.instance;
        }

        if (Input.GetButtonDown("1x Speed"))
        {
            SetSpeed1();
        }
        if (Input.GetButtonDown("2x Speed"))
        {
            SetSpeed2();
        }
        if (Input.GetButtonDown("3x Speed"))
        {
            SetSpeed3();
        }
    }

    public void Pause()
    {
        if(gm.speedMultiplier != 0)
        {
            previousSpeed = gm.speedMultiplier;
            gm.speedMultiplier = 0;
        }
        else
        {
            gm.speedMultiplier = previousSpeed;
        }

        Time.timeScale = gm.speedMultiplier;
        UpdateColor();
    }

    public void SetSpeed1()
    {
        gm.speedMultiplier = 1;

        Time.timeScale = gm.speedMultiplier;
        UpdateColor();
    }

    public void SetSpeed2()
    {
        gm.speedMultiplier = 2;

        Time.timeScale = gm.speedMultiplier;
        UpdateColor();
    }

    public void SetSpeed3()
    {
        gm.speedMultiplier = 3;

        Time.timeScale = gm.speedMultiplier;
        UpdateColor();
    }
    public void SetSpeed4()
    {
        gm.speedMultiplier = 4;

        Time.timeScale = gm.speedMultiplier;
        UpdateColor();
    }
    public void SetSpeed5()
    {
        gm.speedMultiplier = 5;

        Time.timeScale = gm.speedMultiplier;
        UpdateColor();
    }

    public void UpdateColor()
    {
        for (int i = 0; i < bars.Length; i++)
        {
            if (i > gm.speedMultiplier - 1)
            {
                bars[i].color = inactiveColor;
            }
            else
            {
                bars[i].color = activeColor;
            }
        }
    }
}
