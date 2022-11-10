using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GNB;
using FLFlight;
using TMPro;
using Ticker;
using UnityEngine.UI;
using FLFlight.UI;

public class Player : PlayerController
{
    public static Player playerInstance;

    public bool controlsEnabled = true;

    public GameObject hud;

    private GameObject player;
    private bool isMoving = false;
    private bool isBoosting = false;

    [Header("Audio Settings")]
    public float maxPitch;
    public float minPitch;
    public float maxpitchBoost;
    private AudioSource audiosource;
    public AudioClip[] shipAudio;

    [Header("Energy Settings")]
    public float maxEnergy = 100;
    public float energy = 100;
    public float boostCostMutliplier = 1;
    public float energyRecovery = 1;
    public TMP_Text energyReadout;
    public Slider energyBar;

    Vector3 maxLinearForce;
    Vector3 maxAngularForce;
    ShipPhysics physics;
    ShipInput input;
    Camera cam;
    public Camera backgroundCam;
    public float fovTimer;

    [Header("Warp Settings")]

    public GameObject warpEffect;
    public GameObject warpEffectTunnel;

    TargetingSystem targetingSystem;
    TargetingManager targetingManager;
    public Transform target;

    public TrailRenderer[] wingTails;

    WarpJump warp;
    public Gun[] guns;
    public AAHardpoint[] missiles;
    public Laser[] lasers;
    float shakeAmount = 0f;

    [Header("Weapon Settings")]

    //public bool usingMissiles;
    //public bool usingGuns;
    //public bool usingSeekers;
    //float unmodifiedMissileDelay;
    //float unmodifiedGunDelay;
    //float unmodifiedMaxEnergy;

    [HideInInspector]
    public GameObject shipModel;

    Rigidbody rb;

    [Header("Interaction Settings")]

    public GameObject interactText;
    Interactable interactable;
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;

    public Camera firstPersonCam;
    public Camera thirdPersonCam;

    public CameraRig cameraRig;

    public GameObject thirdPersonVisuals;
    public GameObject firstPersonVisuals;
    public Transform firstPersonFirepoint;
    public Transform thirdPersonFirepoint;
    public GameObject stick;

    public GameObject missileWarning;
    float boostCooldown;
    public BoresightCrosshair boresightCrosshair;
    public ParticleSystem[] flameBoosters;

    public GameObject canvas;

    public StartBattle startBattle;
    public UnitController unitController;

    public ParticleSystemRenderer speedLines;
    //public PlayerInfo playernInfo;

    public PlayerFleet fleet;
    public GameObject locationUI;

    public float boostTimer = 0f;
    float boostTimerReq = 2f;

    bool isBraking = false;

    private void Awake()
    {
        playerInstance = this;
    }

    void Start()
    {
        warp = GetComponent<WarpJump>();
        guns = GetComponentsInChildren<Gun>();
        lasers = GetComponentsInChildren<Laser>();
        targetingSystem = GetComponentInChildren<TargetingSystem>();
        targetingManager = GetComponentInChildren<TargetingManager>();
        player = gameObject;
        audiosource = gameObject.GetComponent<AudioSource>();
        audiosource.volume = .1f;
        audiosource.pitch = .1f;
        energy = maxEnergy;
        missiles = GetComponentsInChildren<AAHardpoint>();
        rb = GetComponent<Rigidbody>();

        physics = GetComponent<ShipPhysics>();
        input = GetComponent<ShipInput>();

        maxAngularForce = GetComponent<ShipPhysics>().angularForce;
        maxLinearForce = GetComponent<ShipPhysics>().linearForce;

        cam = Camera.main;
        fovTimer = 0f;
    }

    void Update()
    {
        if (unitController.inStrategyMode == true)
            return;

        boostCooldown -= Time.deltaTime;

        Vector3 velocity = rb.velocity;

        var color = speedLines.material.GetColor("_TintColor");
        color.a = rb.velocity.magnitude / 80;
        color.a = Mathf.Clamp(color.a, 0, .5f);
        speedLines.material.SetColor("_TintColor", color);

        if (targetingManager.target != null)
        {
            target = targetingManager.target;
        }
        else
        {
            target = null;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (canvas.activeSelf)
            {
                canvas.SetActive(false);
            } 
            else
            {
                canvas.SetActive(true);
            }
        }

        fovTimer = Mathf.Clamp(fovTimer, 0f, 1f);
        //energyBar.value = energy / maxEnergy;
        energyReadout.text = string.Format("CORE: {0}%", (((energy / maxEnergy) * 100f).ToString("000")));

        audiosource.volume = Mathf.Clamp(audiosource.volume, .1f, 1f);
        energy = Mathf.Clamp(energy, 0, maxEnergy);

        wingTails[0].materials[0].SetColor("_TintColor", Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.1f), fovTimer));
        wingTails[1].materials[0].SetColor("_TintColor", Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 0.1f), fovTimer));

        var emmision1 = flameBoosters[0].emission;
        var emission2 = flameBoosters[1].emission;

        emmision1.rateOverTime = Mathf.Lerp(10, 100, fovTimer);
        emission2.rateOverTime = Mathf.Lerp(10, 100, fovTimer);

        if (!warp.isWarping)
        {
            if (Input.GetButton("Boost") && energy > 0f && boostCooldown <= 0f && controlsEnabled)
            {
                boostTimer += Time.deltaTime;

                if (boostTimer >= boostTimerReq)
                {
                    if (isBraking)
                    {
                        physics.linearForce = new Vector3(maxLinearForce.x, maxLinearForce.y, maxLinearForce.z);
                    }
                    else
                    {
                        physics.linearForce = new Vector3(maxLinearForce.x * 3, maxLinearForce.y * 3, maxLinearForce.z * 3);
                    }

                    isBoosting = true;
                    energy -= Time.deltaTime * boostCostMutliplier;
                    fovTimer += Time.deltaTime * 2;
                    shakeAmount = Mathf.Lerp(0, .01f, fovTimer);
                    cam.fieldOfView = Mathf.Lerp(60, 80, fovTimer);
                    backgroundCam.fieldOfView = Mathf.Lerp(60, 80, fovTimer);
                    ScreenShake.instance.TriggerShake(.1f, shakeAmount);
                }
            }
            else
            {
                boostTimer = 0;

                isBoosting = false;
                energy += Time.deltaTime * energyRecovery;
                fovTimer -= Time.deltaTime * 2;
                shakeAmount = Mathf.Lerp(0, .01f, fovTimer);
                ScreenShake.instance.TriggerShake(.1f, shakeAmount);
                cam.fieldOfView = Mathf.Lerp(60, 80, fovTimer);
                backgroundCam.fieldOfView = Mathf.Lerp(60, 80, fovTimer);
            }

            if (Input.GetButtonUp("Boost"))
            {
                physics.linearForce = maxLinearForce;
            }
        }

        // Parking Break

        if (Input.GetButtonDown("Jump"))
        {
            isBraking = true;
            physics.angularForce = new Vector3(maxAngularForce.x * 1.25f, maxAngularForce.y * 1.25f, maxAngularForce.z * 1.25f);
            physics.linearForce = new Vector3(maxLinearForce.x * .25f, maxLinearForce.y * .25f, maxLinearForce.z * .25f);
        }

        if (Input.GetButtonUp("Jump"))
        {
            isBraking = false;
            physics.angularForce = new Vector3(maxAngularForce.x, maxAngularForce.y, maxAngularForce.z);
            physics.linearForce = new Vector3(maxLinearForce.x, maxLinearForce.y, maxLinearForce.z);
        }

        if (energy <= 0)
        {
            boostCooldown = 10f;
        }

        if (Input.GetButton("Fire1") && controlsEnabled && energy > 0)
        {
            for (int i = 0; i < guns.Length; i++)
            {
                guns[i].IsFiring = true;
            }

            energy -= Time.deltaTime * guns.Length * .5f;

          //  ScreenShake.instance.TriggerShake(.1f, .01f);
        }
        else
        {
            for (int i = 0; i < guns.Length; i++)
            {
                guns[i].IsFiring = false;
            }
        }

        if (Input.GetButton("Fire1") && controlsEnabled && energy > 0)
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].isFiring = true;
            }

            energy -= Time.deltaTime * lasers.Length * 1f;

           // ScreenShake.instance.TriggerShake(.1f, .01f);
        }
        else
        {
            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].isFiring = false;
            }
        }

        for (int i = 0; i < missiles.Length; i++)
        {
            if (missiles[i].enabled)
            {
                if (Input.GetButton("Fire1") && controlsEnabled)
                {
                    if (target)
                    {
                        if (target.tag != "Celestial Body" || target.tag != "Station")
                            missiles[i].Launch(target.transform, velocity);
                    }
                    else
                    {
                        missiles[i].Launch(null, velocity);
                    }
                }
            }
        }

        if (Ship.PlayerShip != null)
        {
            if (Ship.PlayerShip.Velocity.magnitude >= 0.1f)
            {
                isMoving = true;
            }
            else
            {
                isMoving = false;
            }
        }

        if (isMoving == true)
        {
            audiosource.volume += Time.deltaTime / 10;
            audiosource.pitch = Ship.PlayerShip.Velocity.magnitude / 2.25f;

            audiosource.pitch = Mathf.Clamp(audiosource.pitch, minPitch, maxPitch);

            if (!audiosource.isPlaying)
            {
                audiosource.Play();
            }
        } else if (isMoving == false)
        {
            if(audiosource.volume != .1f)
                audiosource.volume -= Time.deltaTime / 10;

            if (audiosource.volume == 0)
            {
                //audiosource.Stop();
            }
        }

        if (interactable && Input.GetButtonDown("Interact"))
        {
            interactable.Interact(this);
            Debug.Log("Interaction logged");
        }

        if (Input.GetButtonDown("Perspective") && controlsEnabled)
        {
            if (firstPersonVisuals.activeSelf)
            {
                firstPersonVisuals.SetActive(false);
                thirdPersonVisuals.SetActive(true);
                cameraRig.cam = thirdPersonCam;
                firstPersonCam.gameObject.SetActive(false);
                thirdPersonCam.gameObject.SetActive(true);
                audiosource.clip = shipAudio[0];
            }
            else
            {
                firstPersonVisuals.SetActive(true);
                thirdPersonVisuals.SetActive(false);
                cameraRig.cam = firstPersonCam;
                firstPersonCam.gameObject.SetActive(true);
                thirdPersonCam.gameObject.SetActive(false);
                audiosource.clip = shipAudio[1];
            }
        }

        float pivotX = Input.mousePosition.x / Screen.width;
        float pivotY = Input.mousePosition.y / Screen.height;

        stick.transform.localRotation = Quaternion.Euler(Mathf.Lerp(20, -10, pivotY), 0, Mathf.Lerp(30, -30, pivotX));
        firstPersonCam.gameObject.transform.localRotation = Quaternion.Euler(Mathf.Lerp(10, -30, pivotY), Mathf.Lerp(-70, 70, pivotX), 0);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            interactable = other.GetComponent<Interactable>();
            interactText.GetComponent<TMP_Text>().text = string.Format("Press 'F' to interact with " + interactable.name);
            interactText.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            interactText.SetActive(false);
            interactable = null;
        }
    }

    public void UpdateModel()
    {

    }

    public void ShowLocationUI(SpaceStation spaceStation)
    {
        locationUI.GetComponent<SpaceStationUI>().spaceStation = spaceStation;
        locationUI.SetActive(true);


        locationUI.GetComponent<SpaceStationUI>().camPan.isMovable = false;
        locationUI.GetComponent<SpaceStationUI>().crz.isMovable = false;



        Time.timeScale = 0f;
    }
}
