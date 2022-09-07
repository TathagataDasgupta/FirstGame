
using UnityEngine;

public class LTRSAW : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private healthcontroller _healthcontroller;
    

   
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            getDamage();
        }
    }

    public void getDamage()
    {
        _healthcontroller.currentHealth = _healthcontroller.currentHealth - damage;
        _healthcontroller.updateHealth();
       // gameObject.SetActive(false);
    }   

}
