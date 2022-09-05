using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthcontroller : MonoBehaviour
{
    [Header("Health")]
    public int currentHealth;
    [SerializeField] private Image[] hearts;
    private Animator anim;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    private void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        updateHealth();
    }

    public void updateHealth()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < currentHealth)
            {
                hearts[i].color = Color.red;
                anim.SetTrigger("hurt");
                StartCoroutine(Invunerability());
            }
            else
            {
                hearts[i].color = Color.black;
                if (currentHealth == 0)
                {
                    anim.SetTrigger("die");
                    GetComponent<PlayerMovement>().enabled = false;
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
