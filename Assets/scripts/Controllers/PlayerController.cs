using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    public float sprintFactor = 1.5f;
    public float jumpHeight = 2;
    public float groundedDistance = 0.5f;

    [SerializeField] private UI_Inventory uiInventory;

    private Rigidbody rb;
    private Animator animator;
    private HealthController healthController;
    private WeaponInventory weaponInventory;
    private new Camera camera;
    private Vector2 direction;
    private float jumpVelocity;
    private float sprintMultiplier;
    private Inventory inventory;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        healthController = GetComponent<HealthController>();
        weaponInventory = GetComponent<WeaponInventory>();
        camera = Camera.main;

        // We should only calculate this once for performance
        jumpVelocity = Mathf.Sqrt(2 * jumpHeight * Mathf.Abs(Physics.gravity.y));

        sprintMultiplier = 1;

        inventory = new Inventory();

        // passing inventory object to UI script
        Debug.Log(uiInventory);
        uiInventory.SetInventory(inventory);
    }

    void FixedUpdate()
    {
        MovePlayer();
        RotateTowardsMouse();
    }

    void MovePlayer()
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y).normalized * speed * sprintMultiplier;

        // Direction to animate based on player position
        Vector3 moveDirection = transform.InverseTransformDirection(movement).normalized;

        // Apply jumpy velocity
        movement.y = rb.velocity.y;

        // Apply all movement
        rb.velocity = movement;

        // Set animator parameters
        animator.SetFloat("X Velocity", moveDirection.x);
        animator.SetFloat("Z Velocity", moveDirection.z);
    }

    void RotateTowardsMouse() {
        Ray mouseRay = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(mouseRay, out hit))
        {
            Vector3 direction = hit.point - transform.position;
            direction.y = 0;

            // Perform a smooth rotation towards the direction
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                // transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 4f);
                transform.rotation = targetRotation;
            }
        }
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundedDistance);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        direction = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // Ensure button has been pressed and released and the player is grounded
        if (context.performed && IsGrounded())
        {
            // Apply jump velocity and play jump animation
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
            animator.SetTrigger("Jump");
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        // Check if the attack button was pressed
        if (context.performed)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void OnSprint(InputAction.CallbackContext context) {
        if (context.performed) {
            sprintMultiplier = sprintFactor;
        } else {
            sprintMultiplier = 1;
        }
    }
}
