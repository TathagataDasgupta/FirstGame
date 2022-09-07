using UnityEngine;

public class enemyDamage : MonoBehaviour
{


    [SerializeField] private int _Damage;
    [SerializeField] private healthcontroller _healthcontroller;

   private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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