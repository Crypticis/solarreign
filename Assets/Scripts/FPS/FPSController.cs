using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FPSController : PlayerController
{
    CharacterController controller;
    public float speed = 15f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float sprintMultiplier;

    public GameObject questMenu;

    Vector3 velocity;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    bool isGrounded;

    [Header("Interaction Settings")]

    public GameObject interactText;
    public Interactable interactable;
    public DialogueActivator dialogueActivator;
    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    FPSCameraMovement fpsCameraMovement;

    [Header("Weapon Sway")]
    public Transform weapon;
    public float swayAmount;
    public float maxSwayAmount;
    public float smoothSwayAmount;
    private Vector3 initialPosition;
    public Animator animator;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        fpsCameraMovement = GetComponentInChildren<FPSCameraMovement>();
        initialPosition = weapon.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float movementX = -Input.GetAxis("Mouse X");
        float movementY = -Input.GetAxis("Mouse Y");
        movementX = Mathf.Clamp(movementX, -maxSwayAmount, maxSwayAmount);
        movementY = Mathf.Clamp(movementY, -maxSwayAmount, maxSwayAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        weapon.transform.localPosition = Vector3.Lerp(weapon.transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothSwayAmount);

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if(x != 0 || z != 0)
        {
            animator.SetBool("isMoving", true);
        } else
        {
            animator.SetBool("isMoving", false);
        }

        Vector3 move = transform.right * x + transform.forward * z;

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        if(Input.GetButton("Boost") && isGrounded)
        {
            move *= sprintMultiplier;
        }

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if (interactable && Input.GetButtonDown("Interact"))
        {
            interactable.Interact(this);
            Debug.Log("Interaction logged");
        }

        if (dialogueActivator && Input.GetButtonDown("Interact"))
        {
            dialogueActivator.Interact(this);
            Debug.Log("Dialogue logged");
            CameraMovement();
        }

        if (Input.GetButtonDown("Quest Menu"))
        {
            if (questMenu.activeSelf)
            {
                questMenu.SetActive(false);
                Time.timeScale = 1f;
                Cursor.visible = false;
            }
            else
            {
                questMenu.SetActive(true);
                Time.timeScale = 0f;
                Cursor.visible = true;
            }
        }
    }

    public void CameraMovement()
    {
        if (fpsCameraMovement.enabled)
        {
            fpsCameraMovement.enabled = false;
        } 
        else
        {
            fpsCameraMovement.enabled = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            interactable = other.GetComponent<Interactable>();
            interactText.GetComponent<TMP_Text>().text = string.Format("Press 'F' to interact with " + interactable.name);
            interactText.SetActive(true);
        }

        if (other.GetComponent<DialogueActivator>())
        {
            dialogueActivator = other.GetComponent<DialogueActivator>();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Interactable>())
        {
            interactText.SetActive(false);
            interactable = null;
        }

        if (other.GetComponent<DialogueActivator>())
        {
            dialogueActivator = null;
        }
    }
}
