using GT2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public TurretAim[] turrets;
    Player player;

    void Start()
    {
        turrets = GetComponentsInChildren<TurretAim>();
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.controlsEnabled)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1000f));

            if (turrets.Length > 0)
            {
                if (player.target)
                {
                    foreach (TurretAim turret in turrets)
                    {
                        turret.AimPosition = player.target.position;
                    }

                }
                else
                {

                    foreach (TurretAim turret in turrets)
                    {
                        turret.AimPosition = worldPosition;
                    }

                }
            }
        }
    }
}
