
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CandiceAIforGames.AI;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour
{
    Animator animator;
    Animator animator2;
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
    public GameObject sword, enemy1, enemy2, enemy3;
    public bool CanAttack = true;
    public bool isAttacking = false; //check if player is attacking
    public bool isPassive = false; //check if player is passive
    public bool inCombat = false;

    public float AttackCooldown = 4.0f;
    public float attackDamage = 100.0f;

    public CandiceAIController agent;

    //SOUND
    public AudioClip swordAttackSound;

    Collider col2; 
    Collider col3;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        col2 = GetComponent<Collider>();
        col3 = GetComponent<Collider>();

        agent = GetComponent<CandiceAIController>();
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

    public void Passive()
    {
        Collider other = GetComponent<Collider>();

        if (other.tag == "Enemy")
        {
            agent.moveSpeed = 0f;
        }
        
        Debug.Log("Passive Click: " + agent.name);
        //Make enemy passive so it is possible to bite them
        //Play passive animation

        // AudioSource ac = GetComponent<AudioSource>();
        // ac.PlayOneShot(swordAttackSound);

        StartCoroutine(ResetPassiveCooldown());

        // if (col2.tag == "Enemy")
        // {
        //     //this.agent.Sleep();

        //     this.gameObject.SendMessage("Sleep");

        // }
        

    }

    public void Bite()
    {
        isAttacking = true;
        CanAttack = false;
        
        

        //candice damage logic
        //agent.SendMessage("ReceiveDamage", attackDamage);

        //end candice damage logic

        Animator anim = sword.GetComponent<Animator>();

        anim.SetTrigger("Attack");

        AudioSource ac = GetComponent<AudioSource>();
        ac.PlayOneShot(swordAttackSound);
        StartCoroutine(ResetAttackCooldown());
    }

    public void OnTriggerEnter(Collider other)
    {
        Collider col2 = GetComponent<Collider>();
        Collider col3 = GetComponent<Collider>();
        
        enemy1 = other.GetComponent<GameObject>();
        enemy2 = other.GetComponent<GameObject>();
        enemy3 = other.GetComponent<GameObject>();
        // Animator enemyAnim2 = sword.GetComponent<Animator>();
        if (other.tag == "Enemy" && isAttacking == true)
        {
            inCombat = true;
            Debug.Log("Damaged enemy");
            Animator enemyAnim = sword.GetComponent<Animator>();
            
            enemyAnim.SetTrigger("Death");
            Destroy(enemy1,2f);
        }

        if (col2.tag == "Enemy2" && isAttacking == true)
        {
            inCombat = true;

            Debug.Log("Damaged enemy2");
            animator = enemy1.GetComponent<Animator>();

            EnemyHealth enemy2 = GetComponent<EnemyHealth>();
            enemy2.ReceiveDamage(100f);

            animator.SetTrigger("Death2");
            Destroy(enemy2,2f);
            //enemyAnim2.SetTrigger("Death");
            //Destroy((this.agent),2f);
        }

        //Passive attack
        if (other.tag == "Enemy" && isPassive == true)
        {

            Animator enemyAnim = sword.GetComponent<Animator>();
            enemyAnim.SetTrigger("Death");
            Debug.Log("Passive Attack Playing: ");
        }

        // if (other.tag == "Enemy3" && isAttacking == true)
        // {
        //     Debug.Log("Damaged enemy3");
        //     Animator enemyAnim3 = sword.GetComponent<Animator>();
        //     enemyAnim3.SetTrigger("Death3");
        //     //Destroy((this.agent),2f);
        // }


           /* || other.tag == "Enemy3" || other.tag == "Enemy4"
            || other.tag == "Enemy5" || other.tag == "Enemy6"
            || other.tag == "Enemy7" */

        

    }

    IEnumerator ResetPassiveCooldown()
    {
        

        yield return new WaitForSeconds(2f);
        StartCoroutine(ResetPassiveBool());
        
    }

    IEnumerator ResetAttackCooldown()
    {
        StartCoroutine(ResetAttackBool());
        StartCoroutine(ResetCombatCooldown());

        yield return new WaitForSeconds(AttackCooldown);
        CanAttack = true;
        this.gameObject.SendMessage("ReceiveDamage", attackDamage);

    }

    IEnumerator ResetAttackBool()
    {
        yield return new WaitForSeconds(1.0f);
        isAttacking = false;
    }

    IEnumerator ResetPassiveBool()
    {
        yield return new WaitForSeconds(1.0f);
        isPassive = false;
    }

    IEnumerator ResetCombatCooldown()
    {
        yield return new WaitForSeconds(5f);
        inCombat = false;
    }

    public void LoadDeath()
    {
        SceneManager.LoadScene(4); //lose scene
    }

}
