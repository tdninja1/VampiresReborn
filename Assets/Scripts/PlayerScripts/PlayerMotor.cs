using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMotor : MonoBehaviour
{
    Animator animator;
    //player controller
    private CharacterController controller;
    //input manager
    private InputManager inputManager;

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

    //fly movement
    private bool startCooldown;
    public int flyTimer = 5;

    //Bite attack movement
    public GameObject sword;
    public bool CanAttack = true;
    public float AttackCooldown = 4.0f;

    //SOUND
    public AudioClip swordAttackSound;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //can bite?
        if (inputManager.onFoot.Bite.triggered)
        {
            if (CanAttack != true) return;

            else {
                Bite();
            }

        }

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

    public void Fly()
    {
        //set gravity to create a condition
        gravity = 0.2f;

        //player wants to set isGrounded to false since they want to Fly
        if (gravity == 0.2f) {
            //propel the player velocity
            playerVelocity.y = 9f;
            speed = 2f;
        }

        while (speed <= 2f) {
            //create a timer - flyTimer set to 5 seconds ()
            
            //use timer object
            for (int i = 1; i < 500000.0f; i++) {
                // if (flyTimer.CompareTo(5f).equals(5f))
                // {
                    // float temp = 1f;
                    startCooldown = true;
                  

                    //reset values
                    if (startCooldown == true) {
                        gravity = -9.8f;
                        speed = 5f;
                    
                    }
                    else {
                        startCooldown = false;
                    }
                    
                   

                // }
            }
        }
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

    // public void Bite()
    // {
    //     Debug.Log("Player Bite");
    //     animator.SetTrigger("Attack");
        
    // }

    public void Bite()
    {
        CanAttack = false;

        Animator anim = sword.GetComponent<Animator>();
        anim.SetTrigger("Attack");
        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);
        StartCoroutine(ResetAttackCooldown());
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
    }

}
