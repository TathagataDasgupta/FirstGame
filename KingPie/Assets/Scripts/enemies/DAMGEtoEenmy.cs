using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAMGEtoEenmy : MonoBehaviour
{
    [Header("enemyDamage ")]
    [SerializeField] private int _Damage;
    [SerializeField] private giveDamage _healthcontroller;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            getDamage();
        }
    }

    void getDamage()
    {
        _healthcontroller.currentHealth = _healthcontroller.currentHealth - _Damage;
        _healthcontroller.updateHealth();
        // gameObject.SetActive(false);
    }
}
