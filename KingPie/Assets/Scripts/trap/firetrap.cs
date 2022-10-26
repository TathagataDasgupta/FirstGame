using UnityEngine;
using System.Collections;

public class firetrap : MonoBehaviour
{
   

    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;
    [Header("damage")]
    [SerializeField] private int damage;
    [SerializeField] private healthcontroller _healthcontroller;

    [Header("Sound")]
    [SerializeField] private AudioClip firesound;


    private bool triggered; //when the trap gets triggered
    private bool active; //when the trap is active and can hurt the player

    private void Awake()
    {
       anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap());

           else  if (active)
                getDamage();
        }
    }
    private IEnumerator ActivateFiretrap()
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;

        //Wait for delay, activate trap, turn on animation, return color back to normal
        yield return new WaitForSeconds(activationDelay);
        SoundManager.instance.PlaySound(firesound);
        spriteRend.color = Color.white; //turn the sprite back to its initial color
        active = true;
        anim.SetBool("activated", true);

        //Wait until X seconds, deactivate trap and reset all variables and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);
    }
    public void getDamage()
    {
        _healthcontroller.currentHealth = _healthcontroller.currentHealth - damage;
        _healthcontroller.updateHealth();
        // gameObject.SetActive(false);
    }
}
