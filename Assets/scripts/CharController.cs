// based on https://www.youtube.com/watch?v=hiXYyn9NkOo&ab_channel=SoloGameDev
// and based on this for jumping: https://docs.unity3d.com/ScriptReference/CharacterController.Move.html 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{   
    private CharacterController ChaController;

    public float Speed = 5f;
    public float JumpHeight = 2f; // Height the player will jump
    public float Gravity = -9.81f; // Gravity force
    private float VertVelocity; // Player's current velocity

    // Start is called before the first frame update
    void Start()
    {
        ChaController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Jumping
        if (ChaController.isGrounded && Input.GetButton("Jump"))
        {
            VertVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity); // Calculate jump velocity
        }

        // Apply gravity
        VertVelocity += Gravity * Time.deltaTime;

        //apply all movement
        Vector3 move = new Vector3(Input.GetAxis("Horizontal")*Speed, VertVelocity, Input.GetAxis("Vertical")* Speed);
        ChaController.Move(move * Time.deltaTime);
    }
}
