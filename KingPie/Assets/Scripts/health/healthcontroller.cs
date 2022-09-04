using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class healthcontroller : MonoBehaviour
{
    public int currentHealth;
    [SerializeField] private Image[] hearts;
    private Animator anim;


    private void Start()
    {
       
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
}
