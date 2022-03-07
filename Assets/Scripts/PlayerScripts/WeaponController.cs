using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject sword;
    public bool CanAttack = true;
    public float AttackCooldown = 1.0f;

    public AudioClip swordAttackSound;
    private InputManager inputManager;
    

    void Update()
    {
        if (inputManager.onFoot.Bite.triggered)
        {
            if (CanAttack) 
            {
                SwordAttack();
            }

        }
    }

    public void SwordAttack()
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
