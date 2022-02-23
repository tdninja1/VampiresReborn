using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMotor : MonoBehaviour
{
    //player controller
    private CharacterController controller;
    //player
    private Vector3 playerVelocity;
    //speed check
    private float speed = 5f;
    //gravity check
    private bool isGrounded;
    public float gravity = -9.8f;

    //sprint check
    private bool isSprinting;
    
    //crouch movement
    private bool isCrouching;
    private bool lerpCrouch = true;
    private float crouchTimer = 0f;

    //jump movement
    public float jumpHeight = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;

        //crouch check
        if (lerpCrouch)
        {
            crouchTimer += Time.deltaTime;
            float p = crouchTimer / 1;
            p *= p;

            if (isCrouching)
                controller.height = Mathf.Lerp(controller.height, 1, p);
            else
                controller.height = Mathf.Lerp(controller.height, 2, p);

            if (p > 1)
            {
                lerpCrouch = false;
                crouchTimer = 0f;
            }
        }
    }

    public void ProcessMove(Vector2 input) 
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // check if player is grounded and if their playerVelocity; is less than 0
        if (isGrounded && playerVelocity.y < 0) 
            playerVelocity.y = -2f; //if so, set player playerVelocity; to -2f
        //gravity is calculated
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        
        //Tests
        // Debug.Log("Player Y Velocity: " + playerVelocity.y);
        // Debug.Log("Player Speed: " + speed);
        Debug.Log("Player Crouch: " + isCrouching);
    
    }

    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
        }
    }

    public void Crouch()
    {
        isCrouching = !isCrouching;
        crouchTimer = 0;
        lerpCrouch = true;
    }

    public void Sprint()
    {
        isSprinting = !isSprinting;
        
        if (isSprinting)
            speed = 8;
        else
            speed = 5;
    }

}
