// based on https://www.youtube.com/watch?v=hiXYyn9NkOo&ab_channel=SoloGameDev
// and based on this for jumping: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 
// mouse rotation based on/taken from https://github.com/Srfigie/Unity-3d-TopDownMovement/blob/master/Assets/Scripts/TopDownCharacterMover.cs
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class CharController : MonoBehaviour
{
    private InputHandler _input;
    private CharacterController ChaController;
    public float Speed = 5f;
    public float JumpHeight = 2f; // Height the player will jump
    public float Gravity = -9.81f; // Gravity force
    private float VertVelocity; // Player's current velocity

    [SerializeField] private Camera Camera;

    private void Awake()
    {
        _input = GetComponent<InputHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ChaController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateFromMouseVector();

        // Jumping
        if (ChaController.isGrounded && Input.GetButton("Jump"))
        {
            VertVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity); // Calculate jump velocity
        }

        // Apply gravity
        VertVelocity += Gravity * Time.deltaTime;

        //apply all movement
        Vector3 move = new Vector3(_input.InputVector.x * Speed, VertVelocity, _input.InputVector.y * Speed);
        ChaController.Move(move * Time.deltaTime);
    }

    private void RotateFromMouseVector()
    {
        Ray ray = Camera.ScreenPointToRay(_input.MousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance: 300f))
        {
            var target = hitInfo.point;
            target.y = transform.position.y;
            transform.LookAt(target);
        }
    }
}
