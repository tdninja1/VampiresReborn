
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
    private float health;
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
    public AudioClip loseSound;

    
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        inputManager = GetComponent<InputManager>();

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
            TakeDamage(Random.Range(5, 10));
        }

        if (inputManager.onFoot.Heal.triggered)
        {
            RestoreHealth(Random.Range(5, 10));
        }

        // if (inputManager.onFoot.Interact.triggered && CollectCube == null) {
        //     UpdateScrollsUI();
            
        // }

        if (agent != null && agent.WithinAttackRange()) {
            // ReceiveDamage(Random.Range(5, 10));
            TakeDamage(3);
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

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;

        if (health <= 0) { 
            SceneManager.LoadScene(3); //lose scene
            // AudioSource ac = GetComponent<AudioSource>();
            // ac.PlayOneShot(winSound);
        }
    }

    // public void ReceiveDamage(float damage)
    // {
    //     health -= damage;
    //     lerpTimer = 0f;

    //     if (health <= 0)
    //     {

    //     }
    // }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;

    }
}
