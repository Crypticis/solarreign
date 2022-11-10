using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicStarter : MonoBehaviour
{
    void Start()
    {
        MusicManager.instance.StopAll(MusicManager.instance.sounds);
        MusicManager.instance.StopAll(MusicManager.instance.battleMusic);
        MusicManager.instance.PlayRandomMusic();
    }
}
