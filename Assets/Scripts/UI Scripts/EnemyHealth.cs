
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using CandiceAIforGames.AI;
using CandiceAIforGames.AI.Pathfinding;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    private float lerpTimer; //used to animate the health bar
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    //counter for scrolls
    public TextMeshProUGUI scrollCounterText;

    private InputManager inputManager;

    public CandiceAIController agent;
    private RaycastHit rayHit;

    //SOUND
    public AudioClip loseSound;

    Animator anim;

    CandiceAIController agentController;

    public float cooldown = 1f;
    private float lastAttacked = -9999f;

    private float hitLast = 0;
    private float hitDelay = 0.95f;

    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        inputManager = GetComponent<InputManager>();
        agent = GetComponent<CandiceAIController>();

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); //ensures value never goes above or below max and min
        UpdateHealthUI();

        
        
    }

    void Awake()
    {
        health = maxHealth;
        
        agentController = GetComponent<CandiceAIController>();
    }

    public void UpdateHealthUI()
    {
        Debug.Log(health);

        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float healthFraction = health / maxHealth;

        //damage taken
        if (fillBack > healthFraction)
        {
            frontHealthBar.fillAmount = healthFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, healthFraction, percentComplete);
        }

        //restore health
        if (fillFront < healthFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = healthFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }

    // public void UpdateScrollsUI()
    // {
    //     Debug.Log(counter);
    // }

    // public void TakeDamage(float damage)
    // {
    //     health -= damage;
    //     lerpTimer = 0f;

    //     if (health <= 0) { 
    //         SceneManager.LoadScene(3); //lose scene
    //         // AudioSource ac = GetComponent<AudioSource>();
    //         // ac.PlayOneShot(winSound);
    //     }
    // }
    public PlayerMotor pm;
    public GameObject HitParticle;

    public void OnTriggerEnter(Collider other) {
        Animator enemyAnim = other.GetComponent<Animator>();
        float damage = 100f;

        if (other.tag == "Player" && pm.isAttacking) 
        {
            

            if (Time.time - hitLast < hitDelay) 
            {
                return;
                
            }
            ReceiveDamage(100f);
            hitLast = Time.time;

            
            Debug.Log("Player attacking: " + other.name);
           
            Instantiate(HitParticle, new Vector3(other.transform.position.x, 
                transform.position.y, other.transform.position.z), 
                other.transform.rotation);
        }
    }

    public void ReceiveDamage(float damage)
    {
         health -= damage;
         lerpTimer = 0f;

         if (health <= 0) { 
            agentController.moveSpeed = 0;
            agentController.onAttackComplete();
            agentController.DeathAnim();
            agentController.Disappear();
            
            // AudioSource ac = GetComponent<AudioSource>();
            // ac.PlayOneShot(winSound);
            
          }
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;

    }

    public void SetHealth(float health)
    {
        health = this.health;
    }

    public void SetMaxHealth(float maxHealth)
    {
        maxHealth = this.maxHealth;
    }
}
