using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CandiceAIforGames.AI;

public class AttackCollision : MonoBehaviour
{
    // Start is called before the first frame update
    private InputManager inputManager;
    private CharacterController controller;

    public GameObject agentObject;
    public EnemyHealth enemy;
    public CandiceAIController agent;

    public float attackDamage = 100.0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        inputManager = GetComponent<InputManager>();

        enemy = GetComponent<EnemyHealth>();
        agent = GetComponent<CandiceAIController>();

        agentObject = GetComponent<GameObject>();
    }

    void Update()
    {
        


    }

    public void OnTriggerEnter(Collider other)
    {

        // if (other.tag == "Player")
        // {
        //     Debug.Log("Damaged player from collision");
        //     other.GetComponent<PlayerHealth>().ReceiveDamage(0.08f);
        // }


    }

    void OnTriggerStay(Collider other)
    {
        Debug.Log("Object within trigger.");
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Object exited the trigger.");
    }
}
