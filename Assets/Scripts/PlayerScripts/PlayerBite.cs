using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBite : MonoBehaviour
{
    
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
       
        animator = GetComponent<Animator>();
    }



    public void Bite()
    {
       
        animator.SetTrigger("Bite");
        
    }
}
