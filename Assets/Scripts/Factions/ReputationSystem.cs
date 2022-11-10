using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReputationSystem : MonoBehaviour
{
    public float relations;
    public Faction faction1;
    public Faction faction2;

    public Relation myRelation;
    public enum Relation
    {
        neutral,
        ally,
        enemy
    }

    private void Update()
    {
        relations = Mathf.Clamp(relations, -100, 100);

        RelationUpdate();

        if(RelationUpdate() == Relation.ally)
        {
            if (!faction1.allies.Contains(faction2))
            {
                faction1.allies.Add(faction2);
            }
            if (!faction2.allies.Contains(faction1))
            {
                faction2.allies.Add(faction1);
            }
        } 
        else
        {
            faction1.allies.Remove(faction2);
            faction2.allies.Remove(faction1);
        }

        if (RelationUpdate() == Relation.enemy)
        {
            if (!faction1.enemies.Contains(faction2))
            {
                faction1.enemies.Add(faction2);
            }
            if (!faction2.enemies.Contains(faction1))
            {
                faction2.enemies.Add(faction1);
            }
        }
        else
        {
            faction1.enemies.Remove(faction2);
            faction2.enemies.Remove(faction1);
        }
    }

    private Relation RelationUpdate()
    {
        if (relations >= 50)
        {
            return Relation.ally;
        }
        else if (relations >= 0)
        {
            return Relation.neutral;
        }
        else
        {
            return Relation.enemy;
        }
    }
}
