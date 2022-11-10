using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetDialogueChanger : MonoBehaviour
{
    public Dialogue[] enemyDialogue;
    public Dialogue[] neutralDialogue;
    public Dialogue[] allyDialogue;

    DialogueTrigger dialogueTrigger;

    FleetFaction fleetFaction;


    private void Start()
    {
        dialogueTrigger = GetComponent<DialogueTrigger>();
        fleetFaction = GetComponent<FleetFaction>();

        for (int i = 0; i < fleetFaction.faction.enemies.Count; i++)
        {
            if (fleetFaction.faction.enemies[i].playerInFaction)
            {
                dialogueTrigger.dialogue = enemyDialogue[Random.Range(0, enemyDialogue.Length)];
                return;
            }
        }

        if (fleetFaction.faction.playerInFaction)
        {
            dialogueTrigger.dialogue = allyDialogue[Random.Range(0, allyDialogue.Length)];
            return;
        }

        dialogueTrigger.dialogue = neutralDialogue[Random.Range(0, neutralDialogue.Length)];
        return;
    }

    void Update()
    {
        for (int i = 0; i < fleetFaction.faction.enemies.Count; i++)
        {
            if (fleetFaction.faction.enemies[i].playerInFaction)
            {
                //dialogueActivator.UpdateDialogueObject(enemyDialogue[Random.Range(0, enemyDialogue.Length)]);
                dialogueTrigger.dialogue = enemyDialogue[Random.Range(0, enemyDialogue.Length)];
                return;
            } 
        }

        if (fleetFaction.faction.playerInFaction)
        {
            //dialogueActivator.UpdateDialogueObject(allyDialogue[Random.Range(0, allyDialogue.Length)]);
            dialogueTrigger.dialogue = allyDialogue[Random.Range(0, allyDialogue.Length)];
            return;
        }

        dialogueTrigger.dialogue = neutralDialogue[Random.Range(0, neutralDialogue.Length)];
        //dialogueActivator.UpdateDialogueObject(neutralDialogue[Random.Range(0, neutralDialogue.Length)]);
        return;
    }
}
