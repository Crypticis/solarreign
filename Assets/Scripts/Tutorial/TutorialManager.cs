using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager instance;
    public GameObject tutorialPanel;
    public bool isIntro = false;
    //public Cinemachine.CinemachineFreeLook cm;
    public int timerMultiplyer;
    float timer = 0;
    public ClickToMove clickToMove;
    bool isIntro2 = false;

    private void Awake()
    {
        instance = this;
    }

    //public void LateUpdate()
    //{
    //    if (isIntro)
    //    {
    //        timer += Time.deltaTime * timerMultiplyer;

    //        if (!isIntro2)
    //        {
    //            cm.m_Orbits[0].m_Height = Mathf.Lerp(1400, 250, timer);
    //            cm.m_Orbits[0].m_Radius = Mathf.Lerp(3900, 600, timer);
    //        }
    //        else
    //        {
    //            cm.m_Orbits[0].m_Height = Mathf.Lerp(250, 500, timer);
    //            cm.m_Orbits[0].m_Radius = Mathf.Lerp(600, 1200, timer);

    //            //cm.m_XAxis.Value = Mathf.Lerp(0, 12f, timer);

    //            if (timer >= 1)
    //            {
    //                isIntro = false;
    //            }
    //        }

    //        if(timer >= 1 && !isIntro2)
    //        {
    //            isIntro2 = true;
    //            timer = 0;
    //        }
    //    }
    //}

    public void OpenTutorial()
    {
        tutorialPanel.SetActive(true);
        //clickToMove.enabled = false;
        //cm.m_Orbits[0].m_Height = 1400;
        //cm.m_Orbits[0].m_Radius = 3900;
        Time.timeScale = 0;
    }

    public void CloseTutorial()
    {
        tutorialPanel.SetActive(false);
        //isIntro = true;
        //clickToMove.enabled = true;
        Time.timeScale = 1;
    }

}
