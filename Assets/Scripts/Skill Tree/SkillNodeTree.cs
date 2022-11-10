using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillNodeTree : MonoBehaviour
{
    public List<Skill> skills = new List<Skill>();
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < skills.Count; i++)
        {
            if(!skills[i].isDefault)
                skills[i].unlocked = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
