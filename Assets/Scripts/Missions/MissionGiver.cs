using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGiver : Interactable
{
    public Mission[] missions;

    public override void Interact(PlayerController playerController)
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }

    public virtual void AddMission() { }
}
