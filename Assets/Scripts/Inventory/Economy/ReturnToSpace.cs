using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToSpace : Interactable
{
    public int buildIndexOfScene;

    public override void Interact(PlayerController playerController)
    {
        GameManager.instance.isNeedingLoading = true;
        SceneManager.LoadScene(buildIndexOfScene);
        base.Interact(playerController);
    }
}
