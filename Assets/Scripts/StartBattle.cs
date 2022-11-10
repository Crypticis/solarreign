using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBattle : MonoBehaviour
{
    public bool hasStarted = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    public void BattleStart()
    {
        Time.timeScale = 1f;
        hasStarted = true;
        this.gameObject.SetActive(false);
    }
}
