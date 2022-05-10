using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputManager : MonoBehaviour
{

    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
    public GameObject attackObject;

    private PlayerMotor motor;
    private PlayerLook look;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;
        motor = GetComponent<PlayerMotor>();
        // playerBite = attackObject.GetComponent<PlayerBite>();

        //assign look component to player position
        look = GetComponent<PlayerLook>();

        //jump functionality - uses callback context to call jump action
        //callback context can use: started, canceled, and performed
        onFoot.Jump.performed += ctx => motor.Jump();

        //Sprint uses callback context when performed as well
        onFoot.Sprint.performed += ctx => motor.Sprint();

        //Crouch uses callback context when performed
        onFoot.Crouch.performed += ctx => motor.Crouch(); 

        //Fly uses callback context when performed
        onFoot.Fly.performed += ctx => motor.Fly(); 

        //Bite uses callback context when performed
        onFoot.Bite.performed += ctx => motor.Bite();

        //Passive uses callback context when performed
        onFoot.Passive.performed += ctx => motor.Passive();
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //tell the playermotor to move using the value from our movement action.
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
    }

    private void LateUpdate()
    {
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());
    }

    private void OnEnable() { onFoot.Enable(); }

    private void OnDisable() { onFoot.Disable(); }
}
