using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsMenu;
    public GameObject controlsMenu;
    public GameObject buttonsRoot;

    //Opens settings menu and closes controls if open. Idk why
    public void OpenSettings()
    {
        buttonsRoot.SetActive(false);

        if (controlsMenu.activeSelf)
            controlsMenu.SetActive(true);

        settingsMenu.SetActive(true);
        settingsMenu.GetComponent<EnlargePanel>().Enlarge();
        controlsMenu.GetComponent<EnlargePanel>().Shrink();
    }

    //Opens controls menu and closes settings if open. Idk why
    public void OpenControl()
    {
        buttonsRoot.SetActive(false);

        if (settingsMenu.activeSelf)
            settingsMenu.SetActive(true);

        controlsMenu.SetActive(true);
        controlsMenu.GetComponent<EnlargePanel>().Enlarge();
        settingsMenu.GetComponent<EnlargePanel>().Shrink();
    }

    //Closes settings and controls. Brings back the buttons.
    public void Back()
    {
        if (settingsMenu.activeSelf)
            settingsMenu.SetActive(true);

        if (controlsMenu.activeSelf)
            controlsMenu.SetActive(true);

        buttonsRoot.SetActive(true);

        settingsMenu.GetComponent<EnlargePanel>().Shrink();
        controlsMenu.GetComponent<EnlargePanel>().Shrink();
    }
}