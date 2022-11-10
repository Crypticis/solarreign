using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScienceMinigameCell : MonoBehaviour
{
    public ScienceMinigameManager manager;

    public bool hasBomb = false;
    public bool isSelected = false;

    public GameObject image;
    public TMP_Text number;

    public int bombsNearby;

    public void Select()
    {
        isSelected = true;

        if(hasBomb == true)
        {
            Reveal();
            manager.Fail();
        }
        else
        {
            Reveal();
            manager.CheckCompletion();
        }
    }

    public void Reveal()
    {
        if (hasBomb)
        {
            image.SetActive(true);
        }
        else
        {
            number.text = bombsNearby.ToString();
        }
    }
}
