using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ScienceInteraction : WorldInteraction
{
    public ScienceMinigameManager.ScienceMinigame.DifficultyLevel difficulty;
    public LootTable rewards;
    public bool completed = false;

    public override void Interact(PlayerController playerController)
    {
        ScienceMinigameManager.instance.ShowMinigame(difficulty, rewards, this.gameObject);
    }
}
