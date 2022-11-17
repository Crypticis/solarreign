using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserInterfaceManager : MonoBehaviour
{
    public GameObject menu;
    public CanvasGroup flightCanvasGroup;

    public void Update()
    {
        if(Input.GetButtonDown("Pause"))
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                Time.timeScale = 1;
                LeanTween.alphaCanvas(flightCanvasGroup, 1, .1f);
                menu.LeanScale(new Vector3(0, 0, 1), 0f).setIgnoreTimeScale(true);
            }
            else
            {
                menu.SetActive(true);
                Time.timeScale = 0;
                LeanTween.alphaCanvas(flightCanvasGroup, 0, .1f).setIgnoreTimeScale(true);
            }
    }
}
