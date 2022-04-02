using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    private float velocityNew;
    private Vector3 previous;

    public bool playerIsNowMoving;

    Vector3 velocity;
    public bool isGrounded;
    // Update is called once per frame
    void Update()
    {
        checkIfPlayerMoving();
        Jumping();

        //checking if player is grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");


        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }

    private void checkIfPlayerMoving()
    {
        velocityNew = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        if (velocityNew > .1)
        {
            playerIsNowMoving = true;
        }
        if (velocityNew < .1)
        {
            playerIsNowMoving = false;
        }
    }

    private void Jumping()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            //commented out jumping, coz probably wont have jumping in this game
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
}
