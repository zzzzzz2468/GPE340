using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    //sets the varaibles
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float curHealth = 100;
    [SerializeField] private GameObject healthBar;

    //allows them to be seen but not changed from other scripts
    public float MaxHealth { get { return maxHealth; } }

    public float CurHealth { get { return curHealth; } }

    //allows different functions to be called
    public UnityEvent onHeal;

    //heals the player and changes the slider
    public void Heal(float amountToHeal)
    {
        curHealth += amountToHeal;
        curHealth = Mathf.Min(curHealth, maxHealth);
        healthBar.GetComponent<PlayerHealthBar>().SetHealth(curHealth);
    }

    public void PlayHealthParticle()
    {

    }

    public void PlayHealthSound()
    {

    }

    //takes damage, checks if dead and sets slider
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        healthBar.GetComponent<PlayerHealthBar>().SetHealth(curHealth);

        if (curHealth <= 0)
            Destroy(gameObject);
    }
}