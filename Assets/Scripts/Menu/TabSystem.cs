using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSystem : MonoBehaviour
{
    public GameObject tab1;
    public GameObject tab2;

    public Button button1;
    public Button button2;

    public void Tab1()
    {
        Reset();
        button1.interactable = false;
        AudioManager.instance.Play("Click");
        tab1.SetActive(true);
    }
    public void Tab2()
    {
        Reset();
        button2.interactable = false;
        AudioManager.instance.Play("Click");
        tab2.SetActive(true);
    }

    public void Reset()
    {
        if (button1.interactable == false)
        {
            button1.interactable = true;
        }

        if (button2.interactable == false)
        {
            button2.interactable = true;
        }

        if (tab1.activeSelf == true)
        {
            tab1.SetActive(false);
        }

        if (tab2.activeSelf == true)
        {
            tab2.SetActive(false);
        }
    }
}
