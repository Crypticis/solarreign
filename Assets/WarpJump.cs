using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FLFlight;
using TMPro;
using UnityEngine.UI;

public class WarpJump : MonoBehaviour
{
    public float WarpToDistance;
    private Quaternion _lookRotation;
    private Vector3 _direction;
    public float rotationSpeed;
    public bool isWarping;
    public Transform rayPoint;

    ShipPhysics physics;
    float fovTimer = 0f;
    public GameObject warpEffect;
    public GameObject warpEffectTunnel;
    Camera cam;
    public float timer = 5f;
    float shakeAmount = 0f;
    public Collider col;
    public TMP_Text warpTime;

    public PlayerInfo playerInfo;

    Rigidbody rb;

    public WarpTargetingManager warpTargetingManager;

    public PlayerInfo.ShipDefaultValues shipDefaultValues;

    public float fTimer;
    public GameObject warpStuff;
    public Transform warpTarget;

    public GameObject progressBar;
    public PlayerFleet fleet;
    public Transform fleetAnchor;

    public Player player;
    public ShipInput shipInput;
    void Start()
    {
        physics = GetComponent<ShipPhysics>();
        playerInfo = GetComponent<PlayerInfo>();
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        warpTargetingManager = GetComponentInChildren<WarpTargetingManager>();
    }

    void Update()
    {
        if (timer <= 0f)
            timer = 0f;

        if(warpTargetingManager.target && Input.GetKey(KeyCode.F) && !isWarping)
        {
            fTimer += Time.deltaTime;

            progressBar.SetActive(true);
            progressBar.GetComponent<Slider>().value = fTimer / 2f;

            Invoke("HideProgressBar", 2f);

            if (Input.GetKey(KeyCode.F) && fTimer > 2f)
            {
                warpTarget = warpTargetingManager.target;
                Warp(warpTarget);

                progressBar.SetActive(false);
                progressBar.GetComponent<Slider>().value = 0f;

                for (int i = 0; i < playerInfo.shipDefaults.Length; i++)
                {
                    if(playerInfo.playerInfoObject.shipType == playerInfo.shipDefaults[i].shipType)
                    {
                        shipDefaultValues = playerInfo.shipDefaults[i];
                    }
                }
            }
        }

        if (isWarping)
        {
            timer -= Time.deltaTime;

            Align(warpTarget);

            warpStuff.transform.Find("Seconds").GetComponent<TMP_Text>().text = timer.ToString("0.0");

            if (timer <= 0f)
            {
                if (Vector3.Distance(transform.position, warpTarget.position) <= 500f + (warpTarget.GetComponentInChildren<Collider>().bounds.size.x / 2))
                {
                    SetFlightMode();

                    for (int i = 0; i < fleet.fleet.Count; i++)
                    {
                        fleet.fleet[i].ship.transform.position = fleetAnchor.position;
                    }
                }
                else
                {
                    SetWarpMode();
                }
            }
        }

        if(Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.F) && isWarping)
        {
            isWarping = false;
            SetFlightMode();
            warpStuff.SetActive(false);
        }
    }

    public void HideProgressBar()
    {
        progressBar.SetActive(false);
    }

    public void Align(Transform target)
    {
        _direction = (target.position - transform.position).normalized;

        _lookRotation = Quaternion.LookRotation(_direction, Vector3.up);

        if(Vector3.Dot(target.position.normalized, transform.position.normalized) < 0f)
        {
            rotationSpeed = 90f;
        }
        else if (Vector3.Dot(target.position.normalized, transform.position.normalized) < 0.5f)
        {
            rotationSpeed = 30f;
        }
        else
        {
            rotationSpeed = 15f;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(_lookRotation.x, _lookRotation.y, _lookRotation.z, _lookRotation.w), Time.deltaTime * rotationSpeed);

        physics.angularForce = Vector3.zero;
    }

    public void Warp(Transform transform)
    {
        Align(warpTargetingManager.target);

        isWarping = true;

        warpStuff.SetActive(true);
    }

    public void SetFlightMode()
    {
        isWarping = false;

        rb.velocity = Vector3.zero;
        Ship.PlayerShip.Input.throttle = 0f;

        physics.linearForce = shipDefaultValues.linearForce;
        physics.angularForce = shipDefaultValues.angularForce;

        warpEffect.SetActive(false);
        warpEffectTunnel.SetActive(false);

        warpStuff.transform.Find("Seconds").GetComponent<TMP_Text>().text = "";

        col.enabled = true;

        fovTimer -= Time.deltaTime;
        cam.fieldOfView = Mathf.Lerp(60, 90, fovTimer);
        shakeAmount -= Time.deltaTime;
        shakeAmount = Mathf.Lerp(0, 1f, fovTimer);
        ScreenShake.instance.TriggerShake(.1f, shakeAmount);

        col.enabled = true;

        EnableControls();

        timer = 5f;
        fTimer = 0f;
    }

    public void SetWarpMode()
    {
        physics.linearForce = new Vector3(shipDefaultValues.linearForce.x * 250, shipDefaultValues.linearForce.y * 250, shipDefaultValues.linearForce.z * 250);
        Ship.PlayerShip.Input.throttle = 1f;

        warpEffect.SetActive(true);
        warpEffectTunnel.SetActive(true);
        warpStuff.SetActive(false);

        col.enabled = false;

        fovTimer += Time.deltaTime;
        cam.fieldOfView = Mathf.Lerp(60, 90, fovTimer);
        shakeAmount += Time.deltaTime;
        shakeAmount = Mathf.Lerp(0, 1f, fovTimer);
        ScreenShake.instance.TriggerShake(.1f, shakeAmount);

        col.enabled = false;

        DisableControls();

        fTimer = 0f;
    }

    private void DisableControls()
    {
        //Disables flight controls

        shipInput.controlsEnabled = false;
        player.controlsEnabled = false;
    }

    private void EnableControls()
    {
        //Enables flight controls

        shipInput.controlsEnabled = true;
        player.controlsEnabled = true;
    }
}
