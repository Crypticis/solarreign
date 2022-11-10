using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueActivator : Interactable
{
    public DialogueObject dialogueObject;

    public void UpdateDialogueObject(DialogueObject dialogueObject)
    {
        this.dialogueObject = dialogueObject;
    }

    public override void Interact(PlayerController playerController)
    {
        foreach (DialogueResponseEvents responseEvents in GetComponents<DialogueResponseEvents>())
        {
            if (responseEvents.DialogueObject == dialogueObject)
            {
                playerController.GetComponent<Player>().DialogueUI.AddResponseEvents(responseEvents.Events);
                break;
            }
        }

        playerController.GetComponent<Player>().DialogueUI.ShowDialogue(dialogueObject);
    }
}
