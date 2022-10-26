using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectibleheart : MonoBehaviour
{
    [SerializeField] private int healthValue;
    [SerializeField] private healthcontroller _healthcontroller;

    [Header("Sound")]
    [SerializeField] private AudioClip healthsound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            getDamage();
          //  collision.GetComponent<Health>().AddHealth(healthValue);
           
        }
        void getDamage()
        {
            
            _healthcontroller.currentHealth = _healthcontroller.currentHealth + healthValue;
            _healthcontroller.updateHealth();
            SoundManager.instance.PlaySound(healthsound);
            gameObject.SetActive(false);
        }
    }
}
