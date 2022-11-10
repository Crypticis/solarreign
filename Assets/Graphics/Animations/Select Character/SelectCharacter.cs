using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacter : MonoBehaviour
{
    public GameObject[] characters;
    public int activeIndex = 0;
    public CharacterType selectedCharType;

    public void SelectCurrentCharacter()
    {
        selectedCharType = (CharacterType)activeIndex;
        StatManager.instance.playerStatsObject.selectedCharType = selectedCharType;
    }

    public void Next()
    {
        if (activeIndex + 1 > characters.Length - 1) 
        {
            activeIndex = 0;
        }
        else
        {
            activeIndex++;
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if(i != activeIndex)
            {
                characters[i].SetActive(false);
            }
            else
            {
                characters[i].SetActive(true);
            }
        }
    }

    public void Previous()
    {
        if(activeIndex - 1 < 0)
        {
            activeIndex = characters.Length - 1;
        }
        else
        {
            activeIndex--;
        }

        for (int i = 0; i < characters.Length; i++)
        {
            if (i != activeIndex)
            {
                characters[i].SetActive(false);
            }
            else
            {
                characters[i].SetActive(true);
            }
        }
    }

    public enum CharacterType
    {
        human_male,
        human_female,
    }
}
