using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillNode : MonoBehaviour
{
    public Skill skill;
    Button button;
    TMP_Text text;
    public Image icon;
    Image border;
    private Player player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (skill.isDefault)
            skill.unlocked = true;
        button = GetComponent<Button>();
        text = GetComponentInChildren<TMP_Text>();
        text.text = skill.Name;
        icon.sprite = skill.icon;
        border = GetComponent<Image>();
    }

    public void Update()
    {
        if (CanBeUnlocked())
        {
            button.interactable = true;
        }
        else
        {
            button.interactable = false;
        }

        if (skill.unlocked)
        {
            border.color = new Color(1, 1, 1, 1);
            icon.color = new Color(1, 1, 1, 1);
        }
        else
        {
            border.color = new Color(1, 1, 1, .5f);
            icon.color = new Color(1, 1, 1, .5f);
        }
    }
    public void UnlockSkill()
    {
        if (skill.unlocked)
        {
            Debug.Log("Skill already unlocked.");
            return;
        }

        if (CanBeUnlocked())
        {
            skill.unlocked = true;
            StatManager.instance.playerStatsObject.skillpoints -= skill.requiredSkillPoints;
            Debug.Log("Skill " + skill.Name + " unlocked.");
        }
        else
        {
            for (int i = 0; i < skill.requiredSkills.Length; i++)
            {
                if (skill.requiredSkills[i].unlocked == false)
                {
                    Debug.Log("Skill " + skill.requiredSkills[i].Name + " needs to be unlocked.");
                }
                if (StatManager.instance.playerStatsObject.skillpoints < skill.requiredSkillPoints)
                {
                    Debug.Log("Not enough skill points." + " Needs to have at least " + skill.requiredSkillPoints + ".");
                }
            }
        }
    }

    public bool CanBeUnlocked()
    {
        for (int i = 0; i < skill.requiredSkills.Length; i++)
        {
            if (skill.requiredSkills[i].unlocked == false || (StatManager.instance.playerStatsObject.skillpoints < skill.requiredSkillPoints && !skill.unlocked))
            {
                return false;
            }
        }

        return true;
    }
}
