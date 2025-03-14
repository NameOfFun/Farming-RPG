using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Movement
    private CharacterController controller;
    private Animator animator;
    private float moveSpeed = 3f;

    [Header("Movement System")]
    public float walkSpeed = 3f;
    public float runSpeed = 7f;


    // Interaction components
    PlayerInteractor interaction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Get movement component
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // Get interaction component
        interaction = GetComponentInChildren<PlayerInteractor>();
    }

    // Update is called once per frame
    void Update()
    {
        // Runs the function that handle all movement
        Move();

        // Runs the funstion that handles all interation
        Interaction();
    }

    public void Interaction()
    {
        // Tool interaction
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // Interact 
            Debug.Log("da nhan nut q");
            interaction.Interact();
        }

        // TODO: Set up  item interaction
    }

    private void Move()
    {
        // Get horizontal and vertical input as a number
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Direction in a normalised vector
        Vector3 dir = new Vector3(horizontal, 0, vertical).normalized;
        Vector3 velocity = moveSpeed * Time.deltaTime * dir;

        // If the sprint key pressed down?
        if (Input.GetButton("Sprint"))
        {
            animator.SetBool("Running", true);
            moveSpeed = runSpeed;
        }
        else
        {
            animator.SetBool("Running", false);
            moveSpeed = walkSpeed;
        }

        // Check if there is movement
        if (dir.magnitude >= 0.1f)
        {
            // Look towards that direction
            transform.rotation = Quaternion.LookRotation(dir);

            // Move 
            controller.Move(velocity);
        }

        // Animation speed parameter
        animator.SetFloat("Speed", velocity.magnitude);
    }
}
