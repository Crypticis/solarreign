using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScienceInteraction : WorldInteraction
{
    public ScienceMinigameManager.ScienceMinigame.DifficultyLevel difficulty;
    public Item reward;
    public int rewardAmount;
    public bool completed = false;

    public override void Interact(PlayerController playerController)
    {
        ScienceMinigameManager.instance.ShowMinigame(difficulty, reward, rewardAmount, this.gameObject);
    }
}
