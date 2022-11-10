using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skill", menuName = "Skill Tree/Skill")]
public class Skill : ScriptableObject
{
    public string Name;
    public Sprite icon;
    public Skill[] requiredSkills;
    public int requiredSkillPoints;
    public string description;
    public bool unlocked;
    public bool isDefault;
    //public int skillPoints;

    //public bool CanBeUnlocked()
    //{
    //    for (int i = 0; i < requiredSkills.Length; i++)
    //    {
    //        if (requiredSkills[i].unlocked == false)
    //        {
    //            Debug.Log("Skill" + requiredSkills[i].Id.ToString() + "needs to be unlocked");
    //            return false;
    //        }
    //        if (skillPoints < requiredSkillPoints)
    //        {
    //            Debug.Log("Not enough skill points." + " Needs to have at least " + requiredSkillPoints + ".");
    //            return false;
    //        }
    //    }

    //    return true;
    //}

    //public void UnlockSkill()
    //{
    //    if (CanBeUnlocked())
    //    {
    //        unlocked = true;
    //    }
    //}
}
