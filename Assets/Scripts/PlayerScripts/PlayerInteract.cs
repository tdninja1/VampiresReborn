using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;

    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI playerUI;

    private InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;

        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //clear message if no interactable is in front of the player
        playerUI.UpdateText(string.Empty);

        //create a ray to check for collision 
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance);

        RaycastHit hitInfo; //variable to store our collision information
        //means the ray is going to return a value
        if (Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if (hitInfo.collider.GetComponent<Interactable>() != null)
            {
                //store hit info in temp variable
                // Interactable interactable = hitInfo.collider.GetComponent<Interactable>();
                // playerUI.UpdateText(interactable.promptMessage);
                // if (inputManager.OnFoot.Interact.triggered)
                // {
                //     interactable.BaseInteract();
                // }
                
                //debug / test
                Debug.Log(hitInfo.collider.GetComponent<Interactable>().promptMessage);
            }
        }
    }
}
