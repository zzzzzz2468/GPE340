using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //health stats for the enemy so they take damage
    public float curHealth = 100;
    [SerializeField] private GameObject healthBar;
    [HideInInspector] public GameObject spawnPoint;

    //changes healthbar based off of health, divided by 57 to equal the scale of the healthbar
    public void ChangeHealth()
    {
        healthBar.GetComponent<HealthBar>().UpdateSlider(curHealth / 57);
    }

    //takes damage and checks if the object is dead
    public void TakeDamage(float damage)
    {
        curHealth -= damage;
        if (curHealth <= 0)
        {
            spawnPoint.GetComponent<SpawnPoint>().SpawnedObjectDeath();
            Destroy(gameObject);
        }
        else
            ChangeHealth();
    }
}
