using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTiles : MonoBehaviour
{
    public GameObject hexTile;

    public SettlementInfo mainPlanet;  
    void Update()
    {
        if(Vector3.Distance(Camera.main.transform.position, Player.playerInstance.transform.position) >= 1000)
        {
            hexTile.SetActive(true);
            //hexTile.GetComponent<SpriteRenderer>().color = mainPlanet.faction.factionColor;
            var temp = mainPlanet.faction.factionColor;
            //temp.a = .0588f;
            hexTile.GetComponent<SpriteRenderer>().color = temp;

        }
        else
        {
            hexTile.SetActive(false);
        }
    }
}
