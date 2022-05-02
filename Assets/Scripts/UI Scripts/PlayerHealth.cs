
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

using CandiceAIforGames.AI;
using CandiceAIforGames.AI.Pathfinding;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    private float lerpTimer; //used to animate the health bar
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;

    //counter for scrolls
    public int counter = 0;
    public string counterString;
    public GameObject CollectCube;
    public TextMeshProUGUI scrollCounterText;

    private InputManager inputManager;

    public CandiceAIController agent;
    private RaycastHit rayHit;

    //SOUND
    public AudioClip loseSceneSound;

    public float counterDmgMultiplier = 0.0f;

    /*
    * Animator and time of last attack
    */
    Animator anim;

    /*
    * Enemy Delay For Damage. Make sure to use tags: Enemy - Enemy6 to each corresponding enemy.
    */
    private float hitLast = 0;
    private float hitDelay = 1.95f;

    private float hitLast2 = 0;
    private float hitDelay2 = 2.25f;

    private float hitLast3 = 0;
    private float hitDelay3 = 2.1f;

    private float hitLast4 = 0;
    private float hitDelay4 = 2f;

    private float hitLast5 = 0;
    private float hitDelay5 = 1.98f;

    private float hitLast6 = 0;
    private float hitDelay6 = 1.96f;

    private float hitLast7 = 0;
    private float hitDelay7 = 2.12f;

    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        counterDmgMultiplier = 0f;
        inputManager = GetComponent<InputManager>();

        //added animation
        anim = GetComponent<Animator>();
        //end animation

        //added for scrolls
        counter = 0;
        CollectCube = GetComponent<GameObject>();
        scrollCounterText = GetComponent<TextMeshProUGUI>();
     
     
        agent = GetComponent<CandiceAIController>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth); //ensures value never goes above or below max and min
        UpdateHealthUI();
        
        // if (Input.GetKeyDown(KeyCode.G)) - 
        //above is the new input system, use the old input system by using inputManager to handle inputs
        if (inputManager.onFoot.Debug.triggered)
        {
            //TakeDamage(Random.Range(5, 10));
            ReceiveDamage(Random.Range(5, 10));
        }

        if (inputManager.onFoot.Heal.triggered)
        {
            RestoreHealth(Random.Range(5, 10));
        }

        if (inputManager.onFoot.Interact.triggered && CollectCube == null) {
            UpdateScrollsUI();
            
        }

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

    public void UpdateScrollsUI()
    {
        Debug.Log(counter);

    }

    public void LoseAudio()
    {
        AudioSource ac = GetComponent<AudioSource>();

        if (ac.isPlaying)
            {
                return;
            } else {
                ac.PlayOneShot(loseSceneSound);
            }
    }

    public void ReceiveDamage(float damage)
    {
         health -= damage;
         lerpTimer = 0f;

         if (health <= 0) { 
            SceneManager.LoadScene(4); //lose scene


            //
            
            
            

          }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter " + other.gameObject);
        counterDmgMultiplier += 0.5f;
    }

    public void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit " + other.gameObject);
        counterDmgMultiplier -= 0.5f;

    }
    public void OnTriggerStay(Collider other)
    {
        float damage = 3.2f;
        Debug.Log("Counter " + counterDmgMultiplier + " damage*counter: " + damage * counterDmgMultiplier); 
        if (other.gameObject.tag == "Enemy")
        {
            if (Time.time - hitLast < hitDelay) return;

            Debug.Log("Enemy Collided with player");
            ReceiveDamage(damage + counterDmgMultiplier);
            hitLast = Time.time;
                
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
