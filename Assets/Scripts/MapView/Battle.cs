using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battle : MonoBehaviour
{
    public List<GameObject> npcs = new List<GameObject>();
    public bool battleIsWon = false;
    public GameObject battleOverUI;
    public GameObject gameOverUI;

    public NPCDatabaseObject database;

    private IEnumerator coroutine;

    void Start()
    {
        Invoke("UpdateNPCList", 5f);

        coroutine = CheckBattle(2.0f);

        //MusicManager.instance.StopAll(MusicManager.instance.sounds);
        MusicManager.instance.PlayRandomBattleMusic();
    }

    void UpdateNPCList()
    {
        var temp = GameObject.FindGameObjectsWithTag("Enemy");
        npcs.AddRange(temp);

        StartCoroutine(coroutine);
    }

    void Update()
    {
        for (int i = npcs.Count - 1; i >= 0; i--)
        {
            if (npcs[i] == null)
            {
                npcs[i] = npcs[npcs.Count - 1];
                npcs.RemoveAt(npcs.Count - 1);
            }
        }

        if (battleIsWon)
        {
            ShowBattleWon();
        }

        if(Player.playerInstance == null)
        {
            gameOverUI.SetActive(true);
            Cursor.visible = true;
        }
    }

    public void LoadMap()
    {
        GameManager.instance.isNeedingLoading = true;
        SceneManager.LoadScene(1);
    }
    
    void ShowBattleWon()
    {
        BattleManager.instance.ConvertFleet();

        if (battleIsWon)
        {
            if (!battleOverUI.activeSelf)
            {
                battleOverUI.SetActive(true);
                Cursor.visible = true;
            }
        }
    }

    private IEnumerator CheckBattle(float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            if (npcs.Count <= 0)
            {
                battleIsWon = true;
            }
        }
    }
}
