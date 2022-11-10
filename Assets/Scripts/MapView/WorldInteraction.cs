using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldInteraction : Interactable
{
    public InteractionType interactionType;
    public DialogueActivator dialogueActivator;
    public override void Interact(PlayerController playerController)
    {
        var player = playerController.GetComponent<Player>();
        if(interactionType == InteractionType.location)
        {
            if (GetComponent<SpaceStation>())
            {
                player.ShowLocationUI(GetComponent<SpaceStation>());
                QuestManager.instance.ObjectiveUpdate(GetComponent<SettlementInfo>().Name);
            }
        } 
        else if(interactionType == InteractionType.enemy)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if(interactionType == InteractionType.ally)
        {
            GetComponent<DialogueTrigger>().TriggerDialogue();
        }
        else if(interactionType == InteractionType.mining)
        {
            GameManager.instance.Save();
            SceneManager.LoadScene(5);
        }
    }
}

public enum InteractionType
{
    location,
    enemy,
    ally,
    mining,
    science,
}
