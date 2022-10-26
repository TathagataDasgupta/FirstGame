using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class giveDamage : MonoBehaviour
{



    [Header("Health")]
    public int currentHealth;
    [SerializeField] private GameObject[] hearts;
    private Animator anim;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathsound;
    [Header("hurt Sound")]
    [SerializeField] private AudioClip hurtsound;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        //updateHealth();
    }

    public void updateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < currentHealth)
            {
               
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
                SoundManager.instance.PlaySound(hurtsound);
            }
            else
            {
               
                if (currentHealth == 0)
                {
                    anim.SetTrigger("die"); 
                    SoundManager.instance.PlaySound(deathsound);
                    GetComponent<MeleeEnemy>().enabled = false;
                    gameObject.SetActive(false);
                    GetComponent<EnemyPatrol>().enabled = false;
                    gameObject.SetActive(false);
                   
                    

                }


            }
        }
    }

    public int AddHealth(int _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, 4);
        return currentHealth;
    }
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
    }
}

