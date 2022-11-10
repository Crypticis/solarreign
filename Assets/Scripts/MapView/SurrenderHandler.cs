using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SurrenderHandler : MonoBehaviour
{
    public static SurrenderHandler instance;

    public Transform captor;
    public bool isCaptive = false;

    public float timer;

    private IEnumerator coroutine;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if(isCaptive == true)
        {
            Player.playerInstance.gameObject.SetActive(false);
            Player.playerInstance.gameObject.transform.position = captor.position;
        }
        else
        {
            Player.playerInstance.gameObject.SetActive(true);
        }

        if (timer <= 0)
        {
            timer = 0;
            if (isCaptive == true)
            {
                BreakFree();
            }
        }
    }

    public void Surrender(GameObject _captor)
    {
        timer = Random.Range(15f, 60f);
        isCaptive = true;

        Time.timeScale = GameManager.instance.speedMultiplier;

        captor = _captor.transform;

        Player.playerInstance.GetComponent<ClickToMove>().enabled = true;

        if (_captor.GetComponent<UniqueNPC>())
        {
            _captor.GetComponentInChildren<Targeting>().target = null;
            if (_captor.GetComponent<FleetCommanderAI>())
            {
                _captor.GetComponent<FleetCommanderAI>().target = null;
            }
            else if (_captor.GetComponent<CivilianCommanderAI>())
            {
                _captor.GetComponent<CivilianCommanderAI>().target = null;
            }

            _captor.GetComponent<NavMeshAgent>().isStopped = false;
        }

        var moneyTaken = StatManager.instance.playerStatsObject.currentMoney * Random.Range(.05f, .15f);
        StatManager.instance.playerStatsObject.currentMoney -= moneyTaken;

        Ticker.Ticker.AddItem("$" + moneyTaken + " was taken by your captors.");
    }

    public void BreakFree()
    {
        Debug.Log("Broke Free");

        isCaptive = false;
        Player.playerInstance.gameObject.transform.position = RandomNavSphere(captor.transform.position, Random.Range(100, 150), -1);
        captor = null;

        Ticker.Ticker.AddItem("You have escaped your captors.");

        Time.timeScale = 0f;
    }

    public Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;

        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void StartRespawnTimer(GameObject objectToRespawn, float respawnTimer)
    {
        coroutine = Respawn(objectToRespawn, respawnTimer);
        StartCoroutine(coroutine);
    }

    IEnumerator Respawn(GameObject objectToRespawn, float respawnTimer)
    {
        yield return new WaitForSeconds(respawnTimer);
        objectToRespawn.SetActive(true);
        if (objectToRespawn.GetComponent<NavMeshAgent>())
        {
            objectToRespawn.GetComponent<NavMeshAgent>().isStopped = false;
        }
    }
}
