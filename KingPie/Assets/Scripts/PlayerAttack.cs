using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Animator anim;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint; 
    [Header("sound")]
    [SerializeField]private AudioClip FireballSound;
    [Header("Fireball Array")]
    [SerializeField] private GameObject[] fireballs;
    private float coolDownTimer =Mathf.Infinity;
   
    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement= GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer>attackCoolDown && playerMovement.canAttack())
        {
            attack();
            
        }
        coolDownTimer += Time.deltaTime;
    }
    private void attack()
    {
        SoundManager.instance.PlaySound(FireballSound);
        anim.SetTrigger("attack");
        coolDownTimer = 0;

        //pooling

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<projectile>().setDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
